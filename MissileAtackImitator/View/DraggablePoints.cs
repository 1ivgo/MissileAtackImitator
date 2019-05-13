using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MissileAtackImitatorCoreNS.SceneObjects;

namespace MissileAtackImitatorNS.View
{
    class DraggablePoints : List<DraggablePoint>, IDrawable
    {
        internal void Add(
            Control parent,
            string text,
            Point location,
            Size size)

        {
            var draggablePoint = new DraggablePoint(
                Count,
                parent,
                text,
                location,
                size.Width,
                size.Height);

            Add(draggablePoint);
        }

        public void Draw(Graphics graphics)
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
