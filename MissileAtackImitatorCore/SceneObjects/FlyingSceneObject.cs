using System;
using System.Drawing;
using MissileAtackImitatorCoreNS.SceneObjects;

namespace MissileAtackImitatorCoreNS
{
    public abstract class FlyingSceneObject : IDrawable, IDisposable
    {
        protected Bitmap bitmap;
        protected ScenePoints points;
        protected int index = 0;
        protected int maxIndex = 0;
        private bool isDisposed = false;

        public FlyingSceneObject(Bitmap bitmap, ScenePoints points)
        {
            this.bitmap = bitmap;
            this.points = points;
            maxIndex = points.Count - 1;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public abstract int Index { get; set; }

        public void Draw(Graphics gr)
        {
            var point = new PointF(points[index].X - bitmap.Width / 2, points[index].Y - bitmap.Height / 2);
            gr.DrawImage(bitmap, point);
            points.Draw(gr);
        }

        public virtual void Explosion(Bitmap bitmap)
        {
            this.bitmap = bitmap;
            maxIndex = Index;
        }


        protected virtual void Dispose(bool isDisposing)
        {
            if (!isDisposed)
            {
                if (isDisposing)
                {
                    bitmap.Dispose();
                }

                points = null;
                isDisposed = true;
            }
        }
    }
}
