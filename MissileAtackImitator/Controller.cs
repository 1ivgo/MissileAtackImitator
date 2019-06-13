namespace MissileAtackImitatorNS
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using MissileAtackImitatorCoreNS;
    using MissileAtackImitatorCoreNS.SceneObjects;
    using Properties;
    using View.Forms;

    internal class Controller
    {
        private MainForm mainForm = null;
        private List<MovableSceneObject> sceneObjects = new List<MovableSceneObject>();
        private const string PythonFilename = @"python.exe";
        private const string PythonSuccessMessage = "Done\r\n";
        private const string RequestFilename = "ImitationRequest.json";
        private const string ResponseFilename = "ImitationResponse.json";
        private bool isFirstRun = true;
        private DirectoryInfo tempDirInfo = null;

        public Controller(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        internal bool InitProgram()
        {
            bool isPythonInstalled = CheckPythonInstall();

            if (!isPythonInstalled)
            {
                var message = "Для работы программы необходим Python3" +
                    Environment.NewLine + 
                    "Установите Python3 и/или добавьте его в PATH" +
                    Environment.NewLine +
                    "Установить?" +
                    Environment.NewLine +
                    "После установки потребуется заново запустить приложение."; 

                DialogResult dr = MessageBox.Show(message, mainForm.Text, MessageBoxButtons.OKCancel);

                if (dr == DialogResult.OK)
                {
                    CommandRunner.ExecuteCommand("python-3.7.3-amd64-webinstall.exe", string.Empty);
                }

                Application.Exit();

                return false;
            }

            if (Settings.Default.IsFirstStart)
            {
                string message = "Подождите, идет установка" +
                    Environment.NewLine +
                    "библиотеки bezier";
                var action = new Action(() => CommandRunner.ExecuteCommand("pip", "install -U bezier"));
                mainForm.DoActionWithProgressBar(message, action);

                message = "Подождите, идет установка" +
                    Environment.NewLine +
                    "библиотеки scikit-fuzzy";
                action = new Action(() => CommandRunner.ExecuteCommand("pip", "install -U scikit-fuzzy"));
                mainForm.DoActionWithProgressBar(message, action);

                Settings.Default.IsFirstStart = false;
                Settings.Default.Save();
            }

            return true;
        }

        internal void Clear()
        {
            tempDirInfo?.Delete(true);
        }

        internal IEnumerable<IDrawable> DoRequest(ImitationRequest imitationRequest)
        {
            var pythonScriptFilename = Settings.Default.PythonScriptFilename;
            var tempPath = string.Empty;

            Reset();

            if (pythonScriptFilename == string.Empty)
            {
                pythonScriptFilename = ChangePythonScriptFilename();
            }

            if (isFirstRun)
            {
                tempPath = CreateTempDir();
            }

            string requestFilename = Path.Combine(tempPath, RequestFilename);
            string responseFilename = Path.Combine(tempPath, ResponseFilename);

            JsonSaverLoader.Save(imitationRequest, requestFilename);

            string args = pythonScriptFilename + " " + requestFilename + " " + responseFilename;
            string[] result = CommandRunner.ExecuteCommand(PythonFilename, args);

            if (result[0] != PythonSuccessMessage)
            {
                mainForm.ShowError(result[1]);
                return null;
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

        private string CreateTempDir()
        {
            string tempPath = Path.GetTempPath();
            tempPath = Path.Combine(tempPath, mainForm.Text);
            tempDirInfo = Directory.CreateDirectory(tempPath);
            return tempDirInfo.FullName;
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

        private bool CheckPythonInstall()
        {
            var enviromentVariables = Environment.GetEnvironmentVariable("PATH");
            int isFindPython = enviromentVariables.ToUpper().IndexOf("PYTHON");

            if (isFindPython == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
