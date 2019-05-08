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
        private BufferedGraphics bufferedGraphics;

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

            Graphics graphics = splitContainer.Panel2.CreateGraphics();
            var bufferedGraphicsContext = new BufferedGraphicsContext();
            bufferedGraphics = bufferedGraphicsContext.Allocate(
                graphics, 
                new Rectangle(0, 0, splitContainer.Panel2.Width, splitContainer.Panel2.Height));
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

        private void Draw()
        {
            bufferedGraphics.Graphics.Clear(Color.White);
            airplane?.Draw(bufferedGraphics.Graphics);
            userPoints?.Draw(bufferedGraphics.Graphics);
            bufferedGraphics.Render();
        }

        private void splitContainer1_Panel2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            userPoints.Add(new Point(e.X, e.Y));
            Draw();
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

            ScenePoints airplanPoints = controller.GetTrajectory(userPoints);
            airplanPoints.Size = new Size(1, 1);
            airplanPoints.Brush = Brushes.Black;
            airplanPoints.IsWithString = false;

            airplane = new Airplane(airplanPoints);
            airplane.Index = 0;

            Draw();
        }

        private void TsBtSettings_Click(object sender, System.EventArgs e)
        {
            var filter = "Python files (*.py)|*.py";
            var title = "Выберите файл скрипта";
            string pythonScriptFilename = ShowOpenFileDialog(filter, title);
            Settings.Default.PythonScriptPath = pythonScriptFilename;
        }

        private void TsbtClear_Click(object sender, System.EventArgs e)
        {
            airplane = null;
            userPoints = null;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Save();
        }
    }
}
