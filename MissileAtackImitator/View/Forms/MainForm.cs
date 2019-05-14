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
            graphics = pictureBox.CreateGraphics();
            bufferedGraphicsContext = new BufferedGraphicsContext();
            bufferedGraphics = bufferedGraphicsContext.Allocate(
                graphics,
                new Rectangle(0, 0, pictureBox.Width, pictureBox.Height));
            sceneObjects = new List<IDrawable>();
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

        internal void ShowError(string message, string tittle)
        {
            MessageBox.Show(message, tittle, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            aircraftPoints = new DraggablePoints(Color.Red);
            sceneObjects.Add(aircraftPoints);
            tsbtAddMissile.Checked = false;
            pictureBox.MouseClick += PictureBox_AircraftMouseClick;
            pictureBox.MouseClick -= PictureBox_MissileMouseClick;
        }

        private void SetAddMissileMode()
        {
            missilePoints = new MissileDraggablePoints(Color.Black);
            sceneObjects.Add(missilePoints);
            tsbtAddAircraftPoints.Checked = false;
            pictureBox.MouseClick -= PictureBox_AircraftMouseClick;
            pictureBox.MouseClick += PictureBox_MissileMouseClick;
        }

        private void CancellModes()
        {
            tsbtAddAircraftPoints.Checked = false;
            tsbtAddMissile.Checked = false;
            pictureBox.MouseClick -= PictureBox_AircraftMouseClick;
            pictureBox.MouseClick -= PictureBox_MissileMouseClick;
        }

        private void Clear()
        {
            pictureBox.Controls.Clear();
            sceneObjects.Clear();
            bufferedGraphics.Graphics.Clear(Color.White);
            bufferedGraphics.Render();
        }

        private void CreateRequest()
        {
            imitationRequest.AircraftPoints = aircraftPoints.GetPoints();
            imitationRequest.Missile.LaunchPoint = missilePoints.GetPoints()[0];
            imitationRequest.Missile.Direction = missilePoints.GetPoints()[1];

            int missileVelocityModule = Settings.Default.MissileVelocityModule;
            if (missileVelocityModule == 0)
            {

            }

            int stepsCount = Settings.Default.StepsCount;
            if (stepsCount == 0)
            {

            }

            imitationRequest.StepsCount = stepsCount;
            imitationRequest.Missile.VelocityModule = missileVelocityModule;
        }

        private void timer_Tick(object sender, System.EventArgs e)
        {
            controller.Update();
            Draw();
        }

        private void TsbtPlay_Click(object sender, System.EventArgs e)
        {
            CancellModes();
            controller.Clear(sceneObjects);

            if (aircraftPoints.Count < 2)
            {
                MessageBox.Show(
                    "Точек должно быть больше двух",
                    "Внимание",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            imitationRequest.AircraftPoints = aircraftPoints.GetPoints();
            imitationRequest.Missile.LaunchPoint = missilePoints.GetPoints()[0];
            imitationRequest.Missile.Direction = missilePoints.GetPoints()[1];
            imitationRequest.StepsCount = 500;

            controller.DoRequest(sceneObjects, imitationRequest);

            Draw();
            timer.Start();
        }

        private void TsBtSettings_Click(object sender, System.EventArgs e)
        {
            var settingsForm = new SettingsForm();
            settingsForm.Build();
            settingsForm.ShowDialog();
            //controller.ChangePythonScriptFilename();
        }

        private void TsbtClear_Click(object sender, System.EventArgs e)
        {
            CancellModes();
            Clear();
            controller.Clear(sceneObjects);
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
                if (missilePoints.Count == 2)
                {
                    double velocityModule = GetValueDialog.Show("Скорость ракеты");
                    imitationRequest.Missile.VelocityModule = velocityModule;
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
