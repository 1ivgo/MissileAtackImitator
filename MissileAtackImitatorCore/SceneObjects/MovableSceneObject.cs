namespace MissileAtackImitatorCoreNS.SceneObjects
{
    using System;
    using System.Drawing;

    public abstract class MovableSceneObject : IDrawable, IDisposable
    {
        private bool isExploded = false;
        private bool isDisposed = false;

        public MovableSceneObject(Bitmap bitmap, ScenePoints points)
        {
            Bitmap = bitmap;
            Points = points;
            MaxIndex = points.Count - 1;
        }

        public int TrajectoryLength
        {
            get
            {
                return Points.Count;
            }
        }

        protected int Index { get; set; } = 0;

        protected Bitmap Bitmap { get; set; } = null;

        protected ScenePoints Points { get; set; } = null;

        protected int MaxIndex { get; set; } = 0;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public abstract void Move();

        public void Draw(Graphics gr)
        {
            if (gr == null)
            {
                throw new ArgumentNullException(nameof(gr));
            }

            var point = new PointF(Points[Index].X - Bitmap.Width / 2, Points[Index].Y - Bitmap.Height / 2);
            gr.DrawImage(Bitmap, point);
            Points.Draw(gr);
        }

        public virtual void Explosion(Bitmap bitmap)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            if (isExploded)
            {
                return;
            }

            isExploded = true;
            this.Bitmap = bitmap;
            MaxIndex = Index;
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (!isDisposed)
            {
                if (isDisposing)
                {
                    Bitmap.Dispose();
                }

                Points = null;
                isDisposed = true;
            }
        }
    }
}
