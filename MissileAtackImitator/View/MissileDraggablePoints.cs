using System.Drawing;

namespace MissileAtackImitatorNS.View
{
    class MissileDraggablePoints : DraggablePoints
    {
        private Pen pen = null;

        public MissileDraggablePoints(Color color) : base(color)
        {
            pen = new Pen(color, 5);
            pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
        }

        public override void Draw(Graphics graphics)
        {
            base.Draw(graphics);

            if (Count == 2)
            {
                graphics.DrawLine(pen, this[0].Location, this[1].Location);
            }
        }
    }
}
