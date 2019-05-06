using System.Drawing;
using System.Windows.Forms;
using MissileAtackImitatorCoreNS;

namespace MissileAtackImitator
{
    public partial class MainForm : Form
    {
        private Controller controller = new Controller();
        private ScenePoints userPoints;
        private Airplane airplane;

        public MainForm()
        {
            InitializeComponent();
            timer.Start();
            timer.Interval = 100;
            userPoints = new ScenePoints()
            {
                Size = new Size(5, 5),
                Brush = Brushes.Red,
                IsWithString = true
            };
        }

        private void splitContainer1_Panel2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            userPoints.Add(new Point(e.X, e.Y));
            splitContainer.Panel2.Invalidate();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
            userPoints?.Draw(e.Graphics);
            airplane?.Draw(e.Graphics);
        }

        private void timer_Tick(object sender, System.EventArgs e)
        {
            if (airplane != null)
                airplane.Index++;

            splitContainer.Panel2.Invalidate();
        }

        private void toolStripButton1_Click(object sender, System.EventArgs e)
        {
            ScenePoints airplanPoints = controller.GetTrajectory(userPoints);
            airplanPoints.Size = new Size(1, 1);
            airplanPoints.Brush = Brushes.Black;
            airplanPoints.IsWithString = false;

            airplane = new Airplane(airplanPoints);
            airplane.Index = 0;

            splitContainer.Panel2.Invalidate();
        }
    }
}
