using System.Diagnostics;
using System.IO;
using MissileAtackImitatorCoreNS;
using MissileAtackImitatorNS.Properties;

namespace MissileAtackImitator
{
    internal class Controller
    {
        private const string filePathToPython = @"python.exe";
        private MainForm mainForm = null;

        public Controller(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        internal ScenePoints GetTrajectory(ScenePoints userPoints)
        {
            var requestFilename = "ImitationRequest.json";
            var responseFilename = "ImitationResponse.json";
            var pythonScriptFilename = Settings.Default.PythonScriptPath;

            if (pythonScriptFilename == string.Empty)
            {
                var filter = "Python files (*.py)|*.py";
                var title = "Выберите файл скрипта";
                pythonScriptFilename = mainForm.ShowOpenFileDialog(filter, title);
                Settings.Default.PythonScriptPath = pythonScriptFilename;
            }

            new ImitationRequest(500, userPoints).DoRequest(requestFilename);

            var processInfoStart = new ProcessStartInfo()
            {
                FileName = filePathToPython,
                Arguments = string.Format("{0} {1} {2}", pythonScriptFilename, requestFilename, responseFilename),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            using (var process = Process.Start(processInfoStart))
            {
                using (var sr = process.StandardOutput)
                {
                    string result = sr.ReadToEnd();
                }
            }

            File.Delete(requestFilename);

            return new ImitationResponse().GetResponse(responseFilename);
        }
    }
}
