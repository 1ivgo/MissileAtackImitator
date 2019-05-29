namespace MissileAtackImitatorNS.View
{
    using System.Drawing;
    using System.Windows.Forms;
    using Properties;

    internal static class ProgressBarDialog
    {
        static Form form = null;

        internal static bool ShowDialog(Form mainForm)
        {
            bool isFinished = true;

            form = new Form()
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Text = mainForm.Text,
                Icon = Resources.Missile,
                KeyPreview = true,
                StartPosition = FormStartPosition.CenterParent
            };

            var tlp = new TableLayoutPanel()
            {
                RowCount = 3,
                Dock = DockStyle.Fill,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            tlp.RowStyles.Clear();
            tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            var lb = new Label()
            {
                Text = "Подождите, идет выполнение операции",
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Font = new Font("MicrosoftSansSerif", 12)
            };

            var pb = new ProgressBar()
            {
                Dock = DockStyle.Fill,
                Style = ProgressBarStyle.Marquee,
                Width = 100,
                Height = 25
            };

            var btCancell = new Button()
            {
                Dock = DockStyle.Fill,
                Text = "Отмена",
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            btCancell.Click += (sender, e) =>
            {
                Cancel(form, ref isFinished);
            };


            tlp.Controls.Add(lb);
            tlp.Controls.Add(pb);
            tlp.Controls.Add(btCancell);
            form.Controls.Add(tlp);
            form.ShowDialog();

            return isFinished;
        }

        internal static void Close()
        {
            form.Close();
        }

        private static void Cancel(Form form, ref bool isFinished)
        {
            isFinished = false;
            form.Close();
        }
    }
}
