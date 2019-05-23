using MissileAtackImitatorNS.Properties;
using MissileAtackImitatorNS.View.Forms;
using System.ComponentModel;
using System.Windows.Forms;

namespace MissileAtackImitatorNS.View
{
    public static class GetValueDialog<T>
    {
        public static T Show(MainForm mainForm, string title)
        {
            T result = default(T);

            var form = new Form()
            {
                Width = 320,
                Height = 125,
                Text = mainForm.Text,
                AutoSize = true,
                Icon = Resources.Missile,
                KeyPreview = true
            };

            var tlp = new TableLayoutPanel()
            {
                RowCount = 4,
                Dock = DockStyle.Fill,
            };
            tlp.RowStyles.Clear();
            tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            form.Controls.Add(tlp);

            var lb = new Label()
            {
                Text = title,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                AutoSize = true
            };
            tlp.Controls.Add(lb);

            var tb = new TextBox()
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Right
            };
            tlp.Controls.Add(tb);

            var btOk = new Button()
            {
                Text = "OK",
                Dock = DockStyle.Fill
            };
            btOk.Click += (sender, e) =>
            {
                Validate(mainForm, form, tb, ref result);
            };
            tlp.Controls.Add(btOk);

            form.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    Validate(mainForm, form, tb, ref result);
            };

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
