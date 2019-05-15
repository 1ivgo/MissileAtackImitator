using System.Windows.Forms;
using MissileAtackImitatorNS.Properties;
using MissileAtackImitator;

namespace MissileAtackImitatorNS.View.Forms
{
    internal partial class SettingsForm : Form
    {
        private const string noData = "Нет данных";
        private Controller controller = null;

        public SettingsForm(Controller controller)
        {
            InitializeComponent();
            this.controller = controller;
        }

        internal void Build()
        {
            string pythonScriptFilename = Settings.Default.PythonScriptFilename;
            int stepsCont = Settings.Default.StepsCount;
            int missileVelocityModule = Settings.Default.MissileVelocityModule;

            if (pythonScriptFilename == string.Empty)
                lbPythonScriptFilename.Text = noData;
            else
            {
                int dividerIndex = pythonScriptFilename.Length / 2;
                pythonScriptFilename = pythonScriptFilename.Insert(dividerIndex, "\r\n");
                lbPythonScriptFilename.Text = pythonScriptFilename;
            }

            nudPointsCount.Value = stepsCont;
            nudMissileVelocity.Value = missileVelocityModule;
        }

        private void btApply_Click(object sender, System.EventArgs e)
        {
            string pythonScriptFilename = lbPythonScriptFilename.Text;
            pythonScriptFilename = pythonScriptFilename.Replace("\r\n", string.Empty);
            int stepsCount = (int)nudPointsCount.Value;
            int velocityModule = (int)nudMissileVelocity.Value;

            Settings.Default.PythonScriptFilename = pythonScriptFilename;
            Settings.Default.StepsCount = stepsCount;
            Settings.Default.MissileVelocityModule = velocityModule;

            Close();
        }

        private void btChangePythonScriptFilename_Click(object sender, System.EventArgs e)
        {
            string pythonScriptFilename = controller.ChangePythonScriptFilename();
            int dividerIndex = pythonScriptFilename.Length / 2;
            pythonScriptFilename = pythonScriptFilename.Insert(dividerIndex, "\r\n");
            lbPythonScriptFilename.Text = pythonScriptFilename;
        }
    }
}
