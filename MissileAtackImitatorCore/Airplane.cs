using System.Drawing;
using MissileAtackImitatorCoreNS.Properties;

namespace MissileAtackImitatorCoreNS
{
    public class Airplane
    {
        private Bitmap bitmap;
        private ScenePoints points;
        private int index = -1;

        public Airplane(ScenePoints points)
        {
            this.points = points;
            bitmap = new Bitmap(Resources.FighterJet, 25, 25);
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
