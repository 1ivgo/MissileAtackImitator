using System.Windows.Forms;
using MissileAtackImitatorNS.Properties;

namespace MissileAtackImitatorNS.View.Forms
{
    public partial class SettingsForm : Form
    {
        private const string noData = "Нет данных";

        public SettingsForm()
        {
            InitializeComponent();
        }

        internal void Build()
        {
            string pythonSriptFilename = Settings.Default.PythonScriptFilename;
            int stepsCont = Settings.Default.StepsCount;
            int missileVelocityModule = Settings.Default.MissileVelocityModule;

            if (pythonSriptFilename == string.Empty)
                lbPythonScriptFilename.Text = noData;
            else
                lbPythonScriptFilename.Text = pythonSriptFilename;

            nudPointsCount.Value = stepsCont;
            nudMissileVelocity.Value = missileVelocityModule;
        }

        private void btApply_Click(object sender, System.EventArgs e)
        {
            string pythonScriptFilename = lbPythonScriptFilename.Text;
            int stepsCount = (int)nudPointsCount.Value;
            int velocityModule = (int)nudMissileVelocity.Value;

            Settings.Default.PythonScriptFilename = pythonScriptFilename;
            Settings.Default.StepsCount = stepsCount;
            Settings.Default.MissileVelocityModule = velocityModule;

            Close();
        }
    }
}
