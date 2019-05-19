using System;
using System.Drawing;

namespace MissileAtackImitatorCoreNS.SceneObjects
{
    public class Missile : FlyingSceneObject
    {
        private bool isHit = false;
        
        public Missile(Bitmap bitmap, ScenePoints points, bool isHit)
            : base(bitmap, points)
        {
            this.isHit = isHit;
        }

        public Action<Missile> Hit { get; set; } = delegate { };

        public override int Index
        {
            get
            {
                return index;
            }
            set
            {
                if (value > maxIndex)
                {
                    if (isHit)
                        Hit(this);

                    return;
                }

                if (value < 0)
                    return;

                index = value;
            }
        }
    }
}
