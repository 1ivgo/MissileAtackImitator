using System.Diagnostics;
using System.IO;
using MissileAtackImitatorCoreNS;
using MissileAtackImitatorNS.Properties;

namespace MissileAtackImitator
{
    internal class Controller
    {
        private const string filePathToPython = @"python.exe";
        private const string pythonSuccessMessage = "Done\r\n";
        private MainForm mainForm = null;

        public Controller(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        internal ScenePoints GetTrajectory(ScenePoints userPoints)
        {
            var requestFilename = "ImitationRequest.json";
            var responseFilename = "ImitationResponse.json";
            var pythonScriptFilename = Settings.Default.PythonScriptFilename;

            if (pythonScriptFilename == string.Empty)
                pythonScriptFilename = ChangePythonScriptFilename();

            new ImitationRequest(500, userPoints).DoRequest(requestFilename);

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
                    return null;
                }
            }

            File.Delete(requestFilename);

            return new ImitationResponse().GetResponse(responseFilename);
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
    }
}
