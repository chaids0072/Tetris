using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    class BlockItem
    {
        public Court.SHAPE blockShape;
        public List<Point> locations = new List<Point>(4);
        public int blockColor;
        public int status = 1;
        public RotateDirections myRotation = RotateDirections.North;

        public enum RotateDirections
        {
            North = 1,
            East = 2,
            South = 3,
            West = 4
        };


        public BlockItem()
        {
            this.blockShape = (Court.SHAPE)(Court.rnd.Next(Enum.GetValues(typeof(Court.SHAPE)).Length));
            this.blockColor = (int)this.blockShape;
        }

        public void Erase(Point var)
        {
            if (this.locations.Contains(var)) {
                this.locations.Insert(this.locations.IndexOf(var), new Point(-1,-1));
            }
        }
    }
}
