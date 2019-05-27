namespace MissileAtackImitatorNS.View.Forms
{
    using System;
    using System.Windows.Forms;
    using Properties;

    internal partial class SettingsForm : Form
    {
        private const string noData = "Нет данных";
        private const string defuzzificationRightMax = "Right-Max";
        private const string defuzzificationCentroid = "Centroid";
        private const string inferenceMaxMin = "Max-Min";
        private const string inferenceMaxProd = "Max-Prod";
        private const int maxStringLenght = 80;
        private Controller controller = null;

        public SettingsForm(Controller controller)
        {
            InitializeComponent();
            this.controller = controller;
        }

        public Action SettingsChanged { get; set; } = delegate { };

        internal void Build()
        {
            string pythonScriptFilename = Settings.Default.PythonScriptFilename;
            int stepsCont = Settings.Default.StepsCount;
            double missileVelocityModule = Settings.Default.MissileVelocityModule;
            double propCoeff = Settings.Default.PropCoeff;
            string inference = Settings.Default.Inference;
            string defuzzification = Settings.Default.Defuzzification;

            if (pythonScriptFilename == string.Empty)
                lbPythonScriptFilename.Text = noData;
            else
                SetPythonScriptFilename(pythonScriptFilename);

            if (inference == inferenceMaxProd)
                rbInferenceMaxProd.Checked = true;
            else
                rbInferenceMaxMin.Checked = true;

            if (defuzzification == defuzzificationRightMax)
                rbDefuzzificationRightMax.Checked = true;
            else
                rbDefuzzificationCentroid.Checked = true;

            nudPointsCount.Value = stepsCont;
            nudMissileVelocity.Value = (decimal)missileVelocityModule;
            nudPropCoeff.Value = (decimal)propCoeff;
        }

        private void Apply()
        {
            string pythonScriptFilename = lbPythonScriptFilename.Text;
            int stepsCount = (int)nudPointsCount.Value;
            double velocityModule = (double)nudMissileVelocity.Value;
            double propCoeff = (double)nudPropCoeff.Value;
            string inference = string.Empty;
            string defuzzification = string.Empty;

            if (pythonScriptFilename == noData)
                pythonScriptFilename = string.Empty;
            else
            {
                if (pythonScriptFilename.Contains(Environment.NewLine))
                {
                    pythonScriptFilename = pythonScriptFilename.Replace(Environment.NewLine, string.Empty);
                }
            }

            if (rbInferenceMaxMin.Checked == true)
            {
                inference = inferenceMaxMin;
            }
            else
            {
                inference = inferenceMaxProd;
            }

            if (rbDefuzzificationCentroid.Checked == true)
            {
                defuzzification = defuzzificationCentroid;
            }
            else
            {
                defuzzification = defuzzificationRightMax;                
            }

            Settings.Default.PythonScriptFilename = pythonScriptFilename;
            Settings.Default.StepsCount = stepsCount;
            Settings.Default.MissileVelocityModule = velocityModule;
            Settings.Default.PropCoeff = propCoeff;
            Settings.Default.Inference = inference;
            Settings.Default.Defuzzification = defuzzification;

            SettingsChanged();
            Close();
        }

        private void SetPythonScriptFilename(string pythonScriptFilename)
        {
            if (pythonScriptFilename.Length > maxStringLenght)
            {
                int dividerIndex = pythonScriptFilename.Length / 2;
                pythonScriptFilename = pythonScriptFilename.Insert(dividerIndex, Environment.NewLine);
            }

            lbPythonScriptFilename.Text = pythonScriptFilename;
            Settings.Default.Save();
        }

        private void btApply_Click(object sender, System.EventArgs e)
        {
            Apply();
        }

        private void btChangePythonScriptFilename_Click(object sender, EventArgs e)
        {
            string pythonScriptFilename = controller.ChangePythonScriptFilename();
            SetPythonScriptFilename(pythonScriptFilename);
        }

        private void SettingsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Apply();
            }
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            Settings.Default.Reset();
            Build();
        }
    }
}
