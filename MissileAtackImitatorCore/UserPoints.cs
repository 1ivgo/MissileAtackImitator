using System.Collections.Generic;
using System.Drawing;

namespace MissileAtackImitatorCoreNS
{
    public class ScenePoints : List<PointF>
    {
        private static Size size = new Size(5, 5);

        public void Draw(Graphics gr)
        {
            for (int i = 0; i < Count; i++)
            {
                gr.FillRectangle(Brushes.Red, new RectangleF(this[i], size));
                gr.DrawString(i.ToString(), new Font("a", 9), Brushes.Black, this[i]);
            }
        }
    }
}
