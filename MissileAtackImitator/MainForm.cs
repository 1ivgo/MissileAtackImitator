using System.Drawing;
using System.Windows.Forms;
using MissileAtackImitatorCoreNS;
using MissileAtackImitatorNS.Properties;

namespace MissileAtackImitator
{
    public partial class MainForm : Form
    {
        private Controller controller = null;
        private ScenePoints userPoints = null;
        private Airplane airplane = null;
        private BufferedGraphicsContext bufferedGraphicsContext = null;
        private BufferedGraphics bufferedGraphics = null;
        private Graphics graphics = null;

        public MainForm()
        {
            InitializeComponent();
            controller = new Controller(this);
            timer.Start();
            timer.Interval = 25;
            userPoints = new ScenePoints()
            {
                Size = new Size(5, 5),
                Brush = Brushes.Red,
                IsWithString = true
            };

            graphics = pictureBox.CreateGraphics();
            bufferedGraphicsContext = new BufferedGraphicsContext();
            bufferedGraphics = bufferedGraphicsContext.Allocate(graphics, new Rectangle(0, 0, pictureBox.Width, pictureBox.Height));
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
            airplane?.Draw(bufferedGraphics.Graphics);
            userPoints?.Draw(bufferedGraphics.Graphics);
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
                bufferedGraphics = bufferedGraphicsContext.Allocate(graphics, new Rectangle(0, 0, newWidth, newHeight));
            }
        }

        private void timer_Tick(object sender, System.EventArgs e)
        {
            if (airplane != null)
                airplane.Index++;

            Draw();
        }

        private void TsbtPlay_Click(object sender, System.EventArgs e)
        {
            if (userPoints.Count < 2)
            {
                MessageBox.Show(
                    "Точек должно быть больше двух",
                    "Внимание",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            ScenePoints airplanePoints = controller.GetTrajectory(userPoints);

            if (airplanePoints == null)
                return;

            airplanePoints.Size = new Size(1, 1);
            airplanePoints.Brush = Brushes.Black;
            airplanePoints.IsWithString = false;

            airplane = new Airplane(airplanePoints);
            airplane.Index = 0;

            Draw();
        }

        private void TsBtSettings_Click(object sender, System.EventArgs e)
        {
            controller.ChangePythonScriptFilename();
        }

        private void TsbtClear_Click(object sender, System.EventArgs e)
        {
            airplane = null;
            userPoints.Clear();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Save();
        }

        private void MainForm_Resize(object sender, System.EventArgs e)
        {
            ReshapePictureBox();
        }

        private void pictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            userPoints.Add(new Point(e.X, e.Y));
            Draw();
        }
    }
}
