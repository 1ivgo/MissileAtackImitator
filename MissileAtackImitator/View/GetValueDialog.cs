using MissileAtackImitatorNS.Properties;
using System.Windows.Forms;

namespace MissileAtackImitatorNS.View
{
    public static class GetValueDialog
    {
        public static double Show(string title)
        {
            double result = 0;

            var form = new Form()
            {
                Width = 320,
                Height = 125,
                Text = title,
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
                ValidateDouble(form, tb, ref result);
            };
            tlp.Controls.Add(btOk);

            form.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    ValidateDouble(form, tb, ref result);
            };

            form.ShowDialog();
            return result;
        }

        private static void ValidateDouble(Form form, TextBox tb, ref double result)
        {
            bool parseResult = double.TryParse(tb.Text, out result);

            if(parseResult)
            {
                form.Close();
            }
            else
            {
                DialogResult dr = MessageBox.Show(
                    "Введены неверные данные",
                    "Внимание",
                    MessageBoxButtons.RetryCancel,
                    MessageBoxIcon.Warning);

                if (dr == DialogResult.Cancel)
                    form.Close();
                else
                    tb.Clear();
            }
        }
    }
}
