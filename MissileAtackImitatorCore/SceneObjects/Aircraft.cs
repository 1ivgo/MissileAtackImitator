using System.Drawing;

namespace MissileAtackImitatorCoreNS.SceneObjects
{
    public class Aircraft : FlyingSceneObject
    {
        public Aircraft(Bitmap bitmap, ScenePoints points)
            : base(bitmap, points)
        {

        }

        public override int Index
        {
            get
            {
                return index;
            }

            set
            {
                if (value > maxIndex || value < 0)
                {
                    return;
                }

                index = value;
            }
        }
    }
}
