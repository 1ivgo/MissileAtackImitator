namespace MissileAtackImitatorCoreNS.SceneObjects
{
    using System;
    using System.Drawing;

    public class Missile : MovableSceneObject
    {
        private bool isHit = false;

        public Missile(Bitmap bitmap, ScenePoints points, bool isHit)
            : base(bitmap, points)
        {
            this.isHit = isHit;
        }

        public Action<Missile> Hit { get; set; } = delegate { };

        public override void Move()
        {
            if (Index == MaxIndex)
            {
                if (isHit)
                {
                    Hit(this);
                }

                return;
            }

            Index++;
        }
    }
}
