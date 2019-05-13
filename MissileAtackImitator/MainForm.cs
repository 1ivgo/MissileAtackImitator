using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MissileAtackImitatorCoreNS.SceneObjects;
using MissileAtackImitatorNS.Properties;
using MissileAtackImitatorNS.View;

namespace MissileAtackImitator
{
    public partial class MainForm : Form
    {
        private Controller controller = null;
        private BufferedGraphicsContext bufferedGraphicsContext = null;
        private BufferedGraphics bufferedGraphics = null;
        private Graphics graphics = null;
        private DraggablePoints aircraftPoints = null;
        private DraggablePoints missilePoints = null;
        private Size scenePointsSize = new Size(10, 10);
        private List<IDrawable> sceneObjects;

        public MainForm()
        {
            InitializeComponent();
            controller = new Controller(this);
            timer.Start();
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
            aircraftPoints = new DraggablePoints();
            sceneObjects.Add(aircraftPoints);
            tsbtAddMissile.Checked = false;
            pictureBox.MouseClick += PictureBox_AircraftMouseClick;
            pictureBox.MouseClick -= PictureBox_MissileMouseClick;
        }

        private void SetAddMissileMode()
        {
            missilePoints = new DraggablePoints();
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

        private void timer_Tick(object sender, System.EventArgs e)
        {
            controller.Update();
            Draw();
        }

        private void TsbtPlay_Click(object sender, System.EventArgs e)
        {
            CancellModes();

            if (aircraftPoints.Count < 2)
            {
                MessageBox.Show(
                    "Точек должно быть больше двух",
                    "Внимание",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            controller.DoRequest(
                aircraftPoints.GetPoints(),
                missilePoints.GetPoints(),
                sceneObjects);

            Draw();
        }

        private void TsBtSettings_Click(object sender, System.EventArgs e)
        {
            controller.ChangePythonScriptFilename();
        }

        private void TsbtClear_Click(object sender, System.EventArgs e)
        {
            CancellModes();
            controller.Clear();
            pictureBox.Controls.Clear();
            sceneObjects.Clear();
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
            }
        }

        private void TsbtCancellModes_Click(object sender, System.EventArgs e)
        {
            CancellModes();
        }
    }
}
