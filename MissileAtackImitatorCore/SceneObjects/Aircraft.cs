namespace MissileAtackImitatorCoreNS.SceneObjects
{
    using System.Drawing;

    public class Aircraft : MovableSceneObject
    {
        public Aircraft(Bitmap bitmap, ScenePoints points) : base(bitmap, points)
        {
        }

        public override void Move()
        {
            if (Index == MaxIndex)
            {
                return;
            }
            else
            {
                Index++;
            }
        }
    }
}
