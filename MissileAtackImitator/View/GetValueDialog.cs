namespace MissileAtackImitatorNS.View
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Forms;
    using Properties;

    public static class GetValueDialog<T>
    {
        public static T ShowDialog(MainForm mainForm, string text)
        {
            T result = default(T);

            var form = new Form()
            {
                Text = mainForm.Text,
                AutoSize = true,
                Icon = Resources.Missile,
                KeyPreview = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
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
                Text = text,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                AutoSize = true,
                Font = new Font("MircosoftSansSerif", 12)
            };

            var tb = new TextBox()
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Right
            };

            var btOk = new Button()
            {
                Text = "OK",
                Dock = DockStyle.Fill,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            btOk.Click += (sender, e) =>
            {
                Validate(mainForm, form, tb, ref result);
            };

            form.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Validate(mainForm, form, tb, ref result);
                }
            };

            tlp.Controls.Add(lb);
            tlp.Controls.Add(tb);
            tlp.Controls.Add(btOk);
            form.Controls.Add(tlp);
            form.ShowDialog();

            return result;
        }

        private static void Validate(MainForm mainForm, Form form, TextBox tb, ref T result)
        {
            try
            {
                result = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(tb.Text);
                form.Close();
            }
            catch (System.Exception ex)
            {
                mainForm.ShowError(ex.Message);
                tb.Clear();
            }
        }
    }
}
