﻿namespace MissileAtackImitatorNS
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using MissileAtackImitatorCoreNS;
    using MissileAtackImitatorCoreNS.SceneObjects;
    using Properties;
    using View.Forms;

    internal class Controller
    {
        private const string PythonFilename = @"python.exe";
        private const string PythonSuccessMessage = "Done\r\n";
        private MainForm mainForm = null;
        private List<MovableSceneObject> sceneObjects = new List<MovableSceneObject>();

        public Controller(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        internal IEnumerable<IDrawable> DoRequest(ImitationRequest imitationRequest)
        {
            Reset();

            var requestFilename = "ImitationRequest.json";
            var responseFilename = "ImitationResponse.json";
            var pythonScriptFilename = Settings.Default.PythonScriptFilename;

            if (pythonScriptFilename == string.Empty)
            {
                pythonScriptFilename = ChangePythonScriptFilename();
            }

            JsonSaverLoader.Save(imitationRequest, requestFilename);

            var processInfoStart = new ProcessStartInfo()
            {
                FileName = PythonFilename,
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

                if (result != PythonSuccessMessage)
                {
                    using (var sr = process.StandardError)
                    {
                        result = sr.ReadToEnd();
                    }

                    mainForm.ShowError(result);
                    return null;
                }
            }

#if (!DEBUG)
            File.Delete(requestFilename);
#endif

            GetResponse(responseFilename, sceneObjects);
            return sceneObjects as IEnumerable<IDrawable>;
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
            foreach (var sceneObject in sceneObjects)
            {
                sceneObject.Move();
            }
        }

        internal void Reset()
        {
            foreach (var sceneObject in sceneObjects)
            {
                sceneObject.Dispose();
            }

            sceneObjects.Clear();
        }

        private void GetResponse(string responseFilename, List<MovableSceneObject> sceneObjects)
        {
            var pointSize = new Size(2, 2);
            var response = JsonSaverLoader.Load<ImitationResponse>(responseFilename);
            Aircraft aircraft = null;
            var missileBitmap = new Bitmap(Resources.Missile1, 10, 10);
            var explosionBitmap = new Bitmap(Resources.Explosion, 25, 25);

            if (response.AircraftTrajectory.Count > 0)
            {
                List<PointF> aircraftTrajectory = response.AircraftTrajectory;
                var aircraftBitmap = new Bitmap(Resources.FighterJet, 25, 25);
                var aircraftScenePoints = new ScenePoints(aircraftTrajectory, pointSize, Brushes.Black);
                aircraft = new Aircraft(aircraftBitmap, aircraftScenePoints);
                sceneObjects.Add(aircraft);
            }
            else
            {
                mainForm.ShowMessage("Нет траектории самолета");
            }

            if (response.UsualMissile.Trajectory.Count > 0)
            {
                List<PointF> missileTrajectory = response.UsualMissile.Trajectory;
                var missileScenePoints = new ScenePoints(missileTrajectory, pointSize, Brushes.Red);
                var usualMissile = new Missile(missileBitmap, missileScenePoints, response.UsualMissile.IsHit);
                usualMissile.Hit += (bitmap) => usualMissile.Explosion(explosionBitmap);
                mainForm.AddDGVData("Длина траектории четкой ракеты", usualMissile.TrajectoryLength);

                if (aircraft != null)
                {
                    usualMissile.Hit += (bitmap) => aircraft.Explosion(explosionBitmap);
                }

                sceneObjects.Add(usualMissile);
            }
            else
            {
                mainForm.ShowMessage("Нет траектории четкой ракеты");
            }

            if (response.FuzzyMissile.Trajectory.Count > 0)
            {
                List<PointF> fuzzyMissileTrajectory = response.FuzzyMissile.Trajectory;
                var fuzzyMissileScenePoints = new ScenePoints(fuzzyMissileTrajectory, pointSize, Brushes.Blue);
                var fuzzyMissile = new Missile(missileBitmap, fuzzyMissileScenePoints, response.FuzzyMissile.IsHit);
                fuzzyMissile.Hit += (bitmap) => fuzzyMissile.Explosion(explosionBitmap);
                mainForm.AddDGVData("Длина траектории нечеткой ракеты", fuzzyMissile.TrajectoryLength);
                
                if (aircraft != null)
                {
                    fuzzyMissile.Hit += (bitmap) => aircraft.Explosion(explosionBitmap);
                }

                sceneObjects.Add(fuzzyMissile);
            }
            else
            {
                mainForm.ShowMessage("Нет траектории нечеткой ракеты");
            }

#if (!DEBUG)
            File.Delete(responseFilename);
#endif
        }
    }
}
