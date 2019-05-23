namespace MissileAtackImitatorCoreNS.SceneObjects
{
    using System.Collections.Generic;
    using System.Drawing;

    public class ScenePoints : List<RectangleF>, IDrawable
    {
        private Brush brush = null;

        public ScenePoints(List<PointF> points, Size size, Brush brush)
        {
            this.brush = brush;

            foreach (var point in points)
            {
                Add(new RectangleF(point, size));
            }
        }

        public void Draw(Graphics gr)
        {
            gr.FillRectangles(brush, this.ToArray());
        }
    }
}
