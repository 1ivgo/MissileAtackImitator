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

        public virtual void Draw(Graphics graphics)
        {
            foreach (var point in this)
            {
                point.Draw(graphics);
            }
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
