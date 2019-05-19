using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MissileAtackImitatorCoreNS.SceneObjects;
using MissileAtackImitatorNS.Properties;
using MissileAtackImitatorNS.View;
using MissileAtackImitatorCoreNS;
using MissileAtackImitatorNS.View.Forms;

namespace MissileAtackImitator.View.Forms
{
    public partial class MainForm : Form
    {
        private Controller controller = null;
        private BufferedGraphicsContext bufferedGraphicsContext = null;
        private BufferedGraphics bufferedGraphics = null;
        private Graphics graphics = null;
        private DraggablePoints aircraftPoints = null;
        private MissileDraggablePoints missilePoints = null;
        private Size scenePointsSize = new Size(10, 10);
        private List<IDrawable> sceneObjects;
        private ImitationRequest imitationRequest;

        public MainForm()
        {
            InitializeComponent();
            controller = new Controller(this);
            timer.Interval = 25;
            timer.Start();
            graphics = pictureBox.CreateGraphics();
            bufferedGraphicsContext = new BufferedGraphicsContext();
            bufferedGraphics = bufferedGraphicsContext.Allocate(
                graphics,
                new Rectangle(0, 0, pictureBox.Width, pictureBox.Height));
            sceneObjects = new List<IDrawable>();
            UpdateRequestFromDefaultSettings();
            BuildDataGridView();
            UpdateDataGridView();
        }

        internal string ShowOpenFileDialog(string filter, string title)
        {
            string filename = string.Empty;
            var ofd = new OpenFileDialog()
            {
                Filter = filter,
                Title = title
            };

            if (ofd.ShowDialog() == DialogResult.OK)
                filename = ofd.FileName;

            return filename;
        }

        internal void ShowError(string message)
        {
            MessageBox.Show(message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        internal void ShowMessage(string message)
        {
            MessageBox.Show(message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Draw()
        {
            bufferedGraphics.Graphics.Clear(Color.White);

            if (sceneObjects == null)
                return;

            foreach (var sceneObject in sceneObjects)
            {
                sceneObject.Draw(bufferedGraphics.Graphics);
            }

            bufferedGraphics.Render();
        }

        private void ReshapePictureBox()
        {
            int newWidth = splitContainer.Panel2.Width - 4;
            int newHeight = splitContainer.Panel2.Height - 4;

            if (newWidth > pictureBox.Width || newHeight > pictureBox.Height)
            {
                pictureBox.Width = newWidth;
                pictureBox.Height = newHeight;
                bufferedGraphics = bufferedGraphicsContext.Allocate(
                    graphics,
                    new Rectangle(0, 0, newWidth, newHeight));
            }
        }

        private void SetAddAircraftPointsMode()
        {
            if (aircraftPoints == null)
            {
                aircraftPoints = new DraggablePoints(Color.Red);
                sceneObjects.Add(aircraftPoints);
            }

            tsbtAddMissile.Checked = false;
            pictureBox.MouseClick += PictureBox_AircraftMouseClick;
            pictureBox.MouseClick -= PictureBox_MissileMouseClick;
            pictureBox.Cursor = Cursors.Hand;
            tsbtClear.Click -= TsbtClear_Click;
            tsbtClear.Click -= TsbtMissileClear_Click;
            tsbtClear.Click += TsbtAirplaneClear_Click;
        }

        private void SetAddMissileMode()
        {
            if (missilePoints != null)
            {
                missilePoints.Clear(true);
                sceneObjects.Remove(missilePoints);
            }

            missilePoints = new MissileDraggablePoints(Color.Black);
            sceneObjects.Add(missilePoints);
            tsbtAddAircraftPoints.Checked = false;
            pictureBox.MouseClick -= PictureBox_AircraftMouseClick;
            pictureBox.MouseClick += PictureBox_MissileMouseClick;
            pictureBox.Cursor = Cursors.Hand;
            tsbtClear.Click -= TsbtClear_Click;
            tsbtClear.Click -= TsbtAirplaneClear_Click;
            tsbtClear.Click += TsbtMissileClear_Click;
        }

        private void CancellModes()
        {
            tsbtAddAircraftPoints.Checked = false;
            tsbtAddMissile.Checked = false;
            pictureBox.MouseClick -= PictureBox_AircraftMouseClick;
            pictureBox.MouseClick -= PictureBox_MissileMouseClick;
            pictureBox.Cursor = Cursors.Default;
            tsbtClear.Click -= TsbtAirplaneClear_Click;
            tsbtClear.Click -= TsbtMissileClear_Click;
            tsbtClear.Click += TsbtClear_Click;
        }

        private void Clear()
        {
            aircraftPoints?.Clear(true);
            missilePoints?.Clear(true);
            controller.Reset(sceneObjects);
            bufferedGraphics.Graphics.Clear(Color.White);
            bufferedGraphics.Render();
        }

        private bool CreateRequest()
        {
            if (aircraftPoints == null)
            {
                ShowMessage("Не заданы точки самолета");
                return false;
            }

            if (aircraftPoints.Count < 2)
            {
                ShowMessage("Точек самолета должно быть больше 1");
                return false;
            }

            imitationRequest.AircraftPoints = aircraftPoints.GetPoints();

            if (missilePoints == null)
            {
                ShowMessage("Не заданы точки ракеты");
                return false;
            }

            if (missilePoints.Count < 2)
            {
                ShowMessage("Точек ракеты должно быть больше 1");
                return false;
            }

            imitationRequest.Missile.LaunchPoint = missilePoints.GetPoints()[0];
            imitationRequest.Missile.Direction = missilePoints.GetPoints()[1];

            if (imitationRequest.Missile.VelocityModule == 0)
            {
                double missileVelocityModule = Settings.Default.MissileVelocityModule;
                if (missileVelocityModule == 0)
                {
                    missileVelocityModule = GetValueDialog<double>.Show(this, "Скорость ракеты");
                    Settings.Default.MissileVelocityModule = missileVelocityModule;
                }

                imitationRequest.Missile.VelocityModule = missileVelocityModule;
            }

            if (imitationRequest.StepsCount == 0)
            {
                int stepsCount = Settings.Default.StepsCount;
                if (stepsCount == 0)
                {
                    stepsCount = GetValueDialog<int>.Show(this, "Количество точек маршрута");
                    Settings.Default.StepsCount = stepsCount;
                }

                imitationRequest.StepsCount = stepsCount;
            }

            return true;
        }

        private void UpdateRequestFromDefaultSettings()
        {
            imitationRequest.Missile.VelocityModule = Settings.Default.MissileVelocityModule;
            imitationRequest.StepsCount = Settings.Default.StepsCount;
        }

        private void BuildDataGridView()
        {
            dataGridView.Rows.Add(4);
            dataGridView.Rows[0].Cells[0].Value = "Количество точек маршрута";
            dataGridView.Rows[1].Cells[0].Value = "Ускорение ракеты";
            dataGridView.Rows[2].Cells[0].Value = "Длина траектории обычной ракеты";
            dataGridView.Rows[2].Cells[0].Style.ForeColor = Color.Red;
            dataGridView.Rows[3].Cells[0].Value = "Длина траектории нечеткой ракеты";
            dataGridView.Rows[3].Cells[0].Style.ForeColor = Color.Blue;
        }

        private void UpdateDataGridView()
        {
            dataGridView.Rows[0].Cells[1].Value = imitationRequest.StepsCount;
            dataGridView.Rows[1].Cells[1].Value = imitationRequest.Missile.VelocityModule;
        }

        private void OnSettingsFormSettingsChanged()
        {
            imitationRequest.StepsCount = Settings.Default.StepsCount;
            imitationRequest.Missile.VelocityModule = Settings.Default.MissileVelocityModule;
            UpdateDataGridView();
        }

        private void timer_Tick(object sender, System.EventArgs e)
        {
            controller.Update();
            Draw();
        }

        private void TsbtPlay_Click(object sender, System.EventArgs e)
        {
            CancellModes();
            controller.Reset(sceneObjects);
            bool isCreated = CreateRequest();

            if (!isCreated)
                return;

            controller.DoRequest(sceneObjects, imitationRequest);
            Draw();
        }

        private void TsBtSettings_Click(object sender, System.EventArgs e)
        {
            var settingsForm = new SettingsForm(controller);
            settingsForm.SettingsChanged += OnSettingsFormSettingsChanged;
            settingsForm.Build();
            settingsForm.ShowDialog();
        }

        private void TsbtClear_Click(object sender, System.EventArgs e)
        {
            Clear();
            controller.Reset(sceneObjects);
        }

        private void TsbtAirplaneClear_Click(object sender, System.EventArgs e)
        {
            aircraftPoints.Clear(true);
        }

        private void TsbtMissileClear_Click(object sender, System.EventArgs e)
        {
            missilePoints.Clear(true);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Save();
        }

        private void MainForm_Resize(object sender, System.EventArgs e)
        {
            ReshapePictureBox();
        }

        private void TsbtAddAircraftPoints_Click(object sender, System.EventArgs e)
        {
            SetAddAircraftPointsMode();
        }

        private void TsbtAddMissile_Click(object sender, System.EventArgs e)
        {
            SetAddMissileMode();
        }

        private void PictureBox_AircraftMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                aircraftPoints.Add(pictureBox, string.Empty, e.Location, scenePointsSize);
                Draw();
            }
        }

        private void PictureBox_MissileMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                missilePoints.Add(pictureBox, string.Empty, e.Location, scenePointsSize);
                if (missilePoints.Count == 2 && imitationRequest.Missile.VelocityModule == 0)
                {
                    double velocityModule = GetValueDialog<double>.Show(this, "Скорость ракеты");
                    Settings.Default.MissileVelocityModule = velocityModule;
                    CancellModes();
                }
            }
        }

        private void TsbtCancellModes_Click(object sender, System.EventArgs e)
        {
            CancellModes();
        }
    }
}
