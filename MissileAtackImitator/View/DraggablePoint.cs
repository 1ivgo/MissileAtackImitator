using System;
using System.Drawing;
using System.Windows.Forms;

namespace MissileAtackImitatorNS.View
{
    class DraggablePoint : Control
    {
        private bool isDisposed = false;
        private bool isDragging = false;
        private Point mouseDown = Point.Empty;
        private Font font = new Font(DefaultFont, FontStyle.Regular);
        private Brush brush = Brushes.Black;
        private int index = 0;
        private Point stringLocation = Point.Empty;

        public DraggablePoint(
            int index,
            Control parent,
            string text,
            Point location,
            int width,
            int height)
            : base(
                  parent,
                  text,
                  location.X,
                  location.Y,
                  width,
                  height)
        {
            this.index = index;
            BackColor = Color.Red;
            stringLocation = new Point(Location.X, Location.Y + Size.Height);
        }

        internal void Draw(Graphics graphics)
        {
            graphics.DrawString(index.ToString(), font, brush, stringLocation);
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
