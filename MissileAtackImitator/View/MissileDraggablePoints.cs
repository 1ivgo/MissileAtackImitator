using System.Drawing;
using System.Windows.Forms;

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

        internal override void Add(Control parent, string text, Point location, Size size)
        {
            if (Count == 2)
            {
                parent.Controls.Remove(this[0]);
                RemoveAt(0);
            }

            for (int i = 0; i < Count; i++)
            {
                this[i].UpdateIndex(i);
            }

            base.Add(parent, text, location, size);
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
