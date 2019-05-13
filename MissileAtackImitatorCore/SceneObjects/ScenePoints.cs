using System.Collections.Generic;
using System.Drawing;
using MissileAtackImitatorCoreNS.SceneObjects;

namespace MissileAtackImitatorCoreNS
{
    public class ScenePoints : List<PointF>, IDrawable
    {
        public Size Size { get; set; } = new Size(1, 1);

        public Brush Brush { get; set; } = Brushes.Black;

        public void Draw(Graphics gr)
        {
            for (int i = 0; i < Count; i++)
            {
                gr.FillRectangle(Brush, new RectangleF(this[i], Size));
            }
        }
    }
}
