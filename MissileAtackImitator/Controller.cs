using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using MissileAtackImitator.View.Forms;
using MissileAtackImitatorCoreNS;
using MissileAtackImitatorCoreNS.SceneObjects;
using MissileAtackImitatorNS.Properties;

namespace MissileAtackImitator
{
    internal class Controller
    {
        private const string filePathToPython = @"python.exe";
        private const string pythonSuccessMessage = "Done\r\n";
        private MainForm mainForm = null;
        private Aircraft aircraft = null;
        private Missile usualMissile = null;
        private Missile fuzzyMissile = null;

        public Controller(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        internal void DoRequest(List<IDrawable> sceneObjects, ImitationRequest imitationRequest)
        {
            var requestFilename = "ImitationRequest.json";
            var responseFilename = "ImitationResponse.json";
            var pythonScriptFilename = Settings.Default.PythonScriptFilename;

            if (pythonScriptFilename == string.Empty)
                pythonScriptFilename = ChangePythonScriptFilename();

            JsonSaverLoader.Save(imitationRequest, requestFilename);

            var processInfoStart = new ProcessStartInfo()
            {
                FileName = filePathToPython,
                Arguments = string.Format("{0} {1} {2}", pythonScriptFilename, requestFilename, responseFilename),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            string result = string.Empty;
            using (var process = Process.Start(processInfoStart))
            {
                using (var sr = process.StandardOutput)
                {
                    result = sr.ReadToEnd();
                }

                if (result != pythonSuccessMessage)
                {
                    using (var sr = process.StandardError)
                    {
                        result = sr.ReadToEnd();
                    }

                    mainForm.ShowError(result);
                    return;
                }
            }

            File.Delete(requestFilename);
            GetResponse(responseFilename, sceneObjects);
        }

        internal string ChangePythonScriptFilename()
        {
            string pythonScriptFilename = string.Empty;
            var filter = "Python files (*.py)|*.py";
            var title = "Выберите файл скрипта";
            pythonScriptFilename = mainForm.ShowOpenFileDialog(filter, title);
            pythonScriptFilename = pythonScriptFilename.Insert(0, "\"");
            pythonScriptFilename = pythonScriptFilename.Insert(pythonScriptFilename.Length, "\"");
            Settings.Default.PythonScriptFilename = pythonScriptFilename;

            return pythonScriptFilename;
        }

        internal void Update()
        {
            if (aircraft != null)
                aircraft.Index++;

            if (usualMissile != null)
                usualMissile.Index++;

            if (fuzzyMissile != null)
                fuzzyMissile.Index++;
        }

        internal void Clear(List<IDrawable> sceneObjects)
        {
            foreach (var sceneObject in sceneObjects)
            {
                if (sceneObject is FlyingSceneObject)
                    ((FlyingSceneObject)sceneObject).Dispose();
            }

            sceneObjects.Clear();
        }

        internal void Reset(List<IDrawable> sceneObjects)
        {
            for (int i = 0; i < sceneObjects.Count; i++)
            {
                if (sceneObjects[i] is FlyingSceneObject)
                {
                    ((FlyingSceneObject)sceneObjects[i]).Dispose();
                    sceneObjects.Remove(sceneObjects[i]);
                    i--;
                }

            }
        }

        private void GetResponse(string responseFilename, List<IDrawable> sceneObjects)
        {
            var pointSize = new Size(2, 2);
            var response = JsonSaverLoader.Load<ImitationResponse>(responseFilename);

            if (response.AircraftTrajectory.Count > 0)
            {
                List<PointF> aircraftTrajectory = response.AircraftTrajectory;
                var aircraftBitmap = new Bitmap(Resources.FighterJet, 25, 25);
                var aircraftScenePoints = new ScenePoints(aircraftTrajectory, pointSize, Brushes.Black);
                aircraft = new Aircraft(aircraftBitmap, aircraftScenePoints);
                sceneObjects.Add(aircraft);
            }
            else
                mainForm.ShowMessage("Нет траектории самолета");

            var missileBitmap = new Bitmap(Resources.Missile1, 10, 10);

            if (response.UsualMissile.Trajectory.Count > 0)
            {
                List<PointF> missileTrajectory = response.UsualMissile.Trajectory;
                var missileScenePoints = new ScenePoints(missileTrajectory, pointSize, Brushes.Red);
                usualMissile = new Missile(missileBitmap, missileScenePoints, response.UsualMissile.IsHit);
                usualMissile.Hit += OnMissileHit;
                sceneObjects.Add(usualMissile);
            }
            else
                mainForm.ShowMessage("Нет траектории четкой ракеты");

            if (response.FuzzyMissile.Trajectory.Count > 0)
            {
                List<PointF> fuzzyMissileTrajectory = response.FuzzyMissile.Trajectory;
                var fuzzyMissileScenePoints = new ScenePoints(fuzzyMissileTrajectory, pointSize, Brushes.Blue);
                fuzzyMissile = new Missile(missileBitmap, fuzzyMissileScenePoints, response.FuzzyMissile.IsHit);
                fuzzyMissile.Hit += OnMissileHit;
                sceneObjects.Add(fuzzyMissile);
            }
            else
                mainForm.ShowMessage("Нет траектории нечеткой ракеты");
        }

        private void OnMissileHit(Missile missile)
        {
            var explosionBitmap = new Bitmap(Resources.Explosion, 25, 25);
            missile.Explosion(explosionBitmap);
            aircraft?.Explosion(explosionBitmap);
        }
    }
}
