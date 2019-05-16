using System.Drawing;
using System.Windows.Forms;
using MissileAtackImitatorCoreNS.SceneObjects;

namespace MissileAtackImitatorNS.View
{
    class DraggablePoint : Control, IDrawable
    {
        private bool isDisposed = false;
        private bool isDragging = false;
        private Point mouseDown = Point.Empty;
        private Font font = new Font(DefaultFont, FontStyle.Regular);
        private Brush brush = Brushes.Black;
        private int index = 0;
        private Point stringLocation = Point.Empty;

        public DraggablePoint(
            Color color,
            int index,
            Control parent,
            string text,
            Point location,
            Size size)
            : base(
                  parent,
                  text,
                  location.X,
                  location.Y,
                  size.Width,
                  size.Height)
        {
            this.index = index;
            BackColor = color;
            stringLocation = new Point(Location.X, Location.Y + Size.Height);
        }

        public virtual void Draw(Graphics graphics)
        {
            graphics.DrawString(index.ToString(), font, brush, stringLocation);
        }

        internal void UpdateIndex(int i)
        {
            index = i;
        }

        protected override void Dispose(bool disposing)
        {
            if (isDisposed)
                return;

            if (disposing)
            {
                font.Dispose();
                brush.Dispose();
            }

            isDisposed = true;
            base.Dispose(disposing);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                Cursor = Cursors.Hand;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (isDragging)
                {
                    Left += e.X - mouseDown.X;
                    Top += e.Y - mouseDown.Y;
                    stringLocation = new Point(Location.X, Location.Y + Size.Height);
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
                Cursor = Cursors.Default;
            }
        }
    }
}
