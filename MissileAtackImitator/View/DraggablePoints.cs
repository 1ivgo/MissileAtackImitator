using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MissileAtackImitatorCoreNS.SceneObjects;

namespace MissileAtackImitatorNS.View
{
    class DraggablePoints : List<DraggablePoint>, IDrawable
    {
        private Color color;

        public DraggablePoints(Color color) : base()
        {
            this.color = color;
        }

        public virtual void Draw(Graphics graphics)
        {
            foreach (var point in this)
            {
                point.Draw(graphics);
            }
        }

        internal virtual void Add(
            Control parent,
            string text,
            Point location,
            Size size)

        {
            var draggablePoint = new DraggablePoint(
                color,
                Count,
                parent,
                text,
                location,
                size);

            Add(draggablePoint);
        }

        internal virtual void Clear(bool isFull)
        {
            if (isFull)
            {
                foreach (var point in this)
                {
                    point.Parent.Controls.Remove(point);
                }
            }

            Clear();
        }

        internal List<Point> GetPoints()
        {
            var list = new List<Point>();

            foreach (var point in this)
            {
                list.Add(point.Location);
            }

            return list;
        }
    }
}
