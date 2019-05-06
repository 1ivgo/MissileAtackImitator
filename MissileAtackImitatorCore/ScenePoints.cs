using System.Collections.Generic;
using System.Drawing;

namespace MissileAtackImitatorCoreNS
{
    public class ScenePoints : List<PointF>
    {
        public Size Size { get; set; } = new Size(1, 1);

        public Brush Brush { get; set; } = Brushes.Black;

        public bool IsWithString { get; set; } = false;

        public void Draw(Graphics gr)
        {
            for (int i = 0; i < Count; i++)
            {
                gr.FillRectangle(Brush, new RectangleF(this[i], Size));
                if (IsWithString)
                    gr.DrawString(i.ToString(), new Font("a", 9), Brushes.Black, this[i]);
            }
        }
    }
}
