namespace MissileAtackImitatorNS.View.Forms
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Data;
    using MissileAtackImitatorCoreNS;
    using MissileAtackImitatorCoreNS.SceneObjects;
    using Properties;

    public partial class MainForm : Form
    {
        private Controller controller = null;
        private BufferedGraphicsContext bufferedGraphicsContext = null;
        private BufferedGraphics bufferedGraphics = null;
        private Graphics graphics = null;
        private DraggablePoints aircraftPoints = null;
        private MissileDraggablePoints missilePoints = null;
        private Size scenePointsSize = new Size(10, 10);
        private IEnumerable<IDrawable> sceneObjects = null;
        private DGVData dgvData = null;

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
            if (string.IsNullOrEmpty(message))
                return;

            MessageBox.Show(message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        internal void ShowMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
                return;

            MessageBox.Show(message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Draw()
        {
            bufferedGraphics.Graphics.Clear(Color.White);

            if (sceneObjects != null)
            {
                foreach (var sceneObject in sceneObjects)
                {
                    sceneObject.Draw(bufferedGraphics.Graphics);
                }
            }

            aircraftPoints?.Draw(bufferedGraphics.Graphics);
            missilePoints?.Draw(bufferedGraphics.Graphics);
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
                aircraftPoints = new DraggablePoints(Color.Red);

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
                missilePoints.Clear(true);

            missilePoints = new MissileDraggablePoints(Color.Black);
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
            controller.Reset();
            bufferedGraphics.Graphics.Clear(Color.White);
            bufferedGraphics.Render();
        }

        private bool CreateRequest(ref ImitationRequest imitationRequest)
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

            imitationRequest.Missiles.LaunchPoint = missilePoints.GetPoints()[0];
            imitationRequest.Missiles.Direction = missilePoints.GetPoints()[1];

            double missileVelocityModule = Settings.Default.MissileVelocityModule;
            if (missileVelocityModule == 0)
            {
                missileVelocityModule = GetValueDialog<double>.Show(this, "Скорость ракеты");
                Settings.Default.MissileVelocityModule = missileVelocityModule;
            }

            imitationRequest.Missiles.VelocityModule = missileVelocityModule;

            int stepsCount = Settings.Default.StepsCount;
            if (stepsCount == 0)
            {
                stepsCount = GetValueDialog<int>.Show(this, "Количество точек маршрута");
                Settings.Default.StepsCount = stepsCount;
            }

            imitationRequest.StepsCount = stepsCount;
            imitationRequest.Missiles.PropCoeff = Settings.Default.PropCoeff;
            imitationRequest.Missiles.Inference = Settings.Default.Inference;
            imitationRequest.Missiles.Defuzzification = Settings.Default.Defuzzification;

            return true;
        }

        private void BuildDataGridView()
        {
            dgvData = new DGVData();
            dataGridView.DataSource = dgvData;
            dataGridView.Columns["Name"].HeaderText = "Параметер";
            dataGridView.Columns["Value"].HeaderText = "Значение";
            OnSettingsFormSettingsChanged();
        }

        private void UpdateDataGridView()
        {
            dgvData.ResetBindings();
        }

        private void OnSettingsFormSettingsChanged()
        {
            dgvData.Set("Количество точек маршрута", Settings.Default.StepsCount);
            dgvData.Set("Скорость ракеты", Settings.Default.MissileVelocityModule);
            dgvData.Set("Метод вывода", Settings.Default.Inference);
            dgvData.Set("Метод дефазификации", Settings.Default.Defuzzification);
            dgvData.Set("Коэффициент пропорциональности обычной ракеты", Settings.Default.PropCoeff);
            UpdateDataGridView();
        }

        private void StartImitation()
        {
            CancellModes();

            var imitationRequest = new ImitationRequest();
            bool isCreated = CreateRequest(ref imitationRequest);

            if (!isCreated)
                return;

            sceneObjects = controller.DoRequest(imitationRequest, dgvData);
            Draw();
        }

        private void timer_Tick(object sender, System.EventArgs e)
        {
            controller.Update();
            Draw();
        }

        private void TsbtPlay_Click(object sender, System.EventArgs e)
        {
            StartImitation();
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
            if (tsbtAddAircraftPoints.Checked != true)
            {
                tsbtAddAircraftPoints.Checked = true;
                return;
            }

            SetAddAircraftPointsMode();
        }

        private void TsbtAddMissile_Click(object sender, System.EventArgs e)
        {
            if (tsbtAddMissile.Checked != true)
            {
                tsbtAddMissile.Checked = true;
                return;
            }


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
                if (missilePoints.Count == 2 && Settings.Default.MissileVelocityModule == 0)
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

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            BuildDataGridView();
            UpdateDataGridView();
            Draw();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                StartImitation();
            }

            if (e.KeyCode == Keys.Escape)
            {
                CancellModes();
            }
        }

        private void dataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow row = dataGridView.Rows[index];

            if (row.Cells["Name"].Value.ToString() == "Длина траектории четкой ракеты")
            {
                row.DefaultCellStyle.ForeColor = Color.Red;
            }

            if (row.Cells["Name"].Value.ToString() == "Длина траектории нечеткой ракеты")
            {
                row.DefaultCellStyle.ForeColor = Color.Blue;
            }
        }
    }
}
