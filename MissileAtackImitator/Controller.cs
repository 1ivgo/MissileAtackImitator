using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Resources;
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

        public Controller(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        internal void DoRequest(
            List<Point> aircraftPoints,
            List<Point> missilePoints,
            List<IDrawable> sceneObjects)
        {
            var requestFilename = "ImitationRequest.json";
            var responseFilename = "ImitationResponse.json";
            var pythonScriptFilename = Settings.Default.PythonScriptFilename;

            if (pythonScriptFilename == string.Empty)
                pythonScriptFilename = ChangePythonScriptFilename();

            new ImitationRequest(500, aircraftPoints).DoRequest(requestFilename);

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

            var response = new ImitationResponse();
            response.GetResponse(responseFilename);
            ScenePoints aircraftTrajectory = response.AircraftTrajectory;
            var aircraftBitmap = new Bitmap(Resources.FighterJet);
            aircraft = new FlyingSceneObject(aircraftTrajectory, aircraftBitmap);
            ScenePoints missileTrajectory = response.MissileTrajectory;
            var missileBitmap = new Bitmap(Resources.Missile1);
            missile = new FlyingSceneObject(missileTrajectory, missileBitmap);
            sceneObjects.Add(aircraft);
            sceneObjects.Add(missile);
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
        }

        internal void Clear()
        {
            aircraft = null;
        }
    }
}
