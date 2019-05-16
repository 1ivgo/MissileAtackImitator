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
        private FlyingSceneObject aircraft = null;
        private FlyingSceneObject missile = null;
        private FlyingSceneObject fuzzyMissile = null;

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

                    mainForm.ShowError(result, "Ошибка при выполнении скрипта");
                    return;
                }
            }

            File.Delete(requestFilename);

            var response = JsonSaverLoader.Load<ImitationResponse>(responseFilename);


            var pointSize = new Size(2, 2);

            List<PointF> aircraftTrajectory = response.AircraftTrajectory;
            var aircraftBitmap = new Bitmap(Resources.FighterJet, 25, 25);
            var aircraftScenePoints = new ScenePoints(aircraftTrajectory, pointSize, Brushes.Black);
            aircraft = new FlyingSceneObject(aircraftBitmap, aircraftScenePoints);
            sceneObjects.Add(aircraft);

            List<PointF> missileTrajectory = response.UsualMissile.Trajectory;
            var missileBitmap = new Bitmap(Resources.Missile1, 10, 10);
            var missileScenePoints = new ScenePoints(missileTrajectory, pointSize, Brushes.Red);
            missile = new FlyingSceneObject(missileBitmap, missileScenePoints);
            sceneObjects.Add(missile);

            List<PointF> fuzzyMissileTrajectory = response.FuzzyMissile.Trajectory;
            var fuzzyMissileScenePoints = new ScenePoints(fuzzyMissileTrajectory, pointSize, Brushes.Blue);
            fuzzyMissile = new FlyingSceneObject(missileBitmap, fuzzyMissileScenePoints);
            sceneObjects.Add(fuzzyMissile);
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

            if (missile != null)
                missile.Index++;

            if (missile != null)
                fuzzyMissile.Index++;
        }

        internal void Clear(List<IDrawable> sceneObjects)
        {
            sceneObjects.Remove(aircraft);
            aircraft = null;
            sceneObjects.Remove(missile);
            missile = null;
        }
    }
}
