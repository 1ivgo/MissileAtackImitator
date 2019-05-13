using System.Drawing;
using MissileAtackImitatorCoreNS.Properties;
using MissileAtackImitatorCoreNS.SceneObjects;

namespace MissileAtackImitatorCoreNS
{
    public class FlyingSceneObject : IDrawable
    {
        private Bitmap bitmap;
        private ScenePoints points;
        private int index = 0;

        public FlyingSceneObject(ScenePoints points, Bitmap bitmap)
        {
            this.points = points;
            this.bitmap = bitmap;
        }

        public int Index
        {
            get
            {
                return index;
            }
            set
            {
                if (value > points.Count - 1 || value < 0)
                    return;

                index = value;
            }
        }

        public void Draw(Graphics gr)
        {
            var point = new PointF(points[index].X - bitmap.Width / 2, points[index].Y - bitmap.Height / 2);
            gr.DrawImage(bitmap, point);
            points.Draw(gr);
        }
    }
}
