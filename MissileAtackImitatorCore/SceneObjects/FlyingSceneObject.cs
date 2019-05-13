using System.Drawing;
using MissileAtackImitatorCoreNS.SceneObjects;

namespace MissileAtackImitatorCoreNS
{
    public class FlyingSceneObject : IDrawable
    {
        private ScenePoints points;
        private int index = 0;
        private Bitmap bitmap;

        public FlyingSceneObject(Bitmap bitmap, ScenePoints points)
        {
            this.bitmap = bitmap;
            this.points = points;
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
