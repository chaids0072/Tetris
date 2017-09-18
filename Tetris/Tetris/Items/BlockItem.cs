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

        public enum RotateDirections
        {
            North = 1,
            East = 2,
            South = 3,
            West = 4
        };

        public RotateDirections myRotation = RotateDirections.North;

        public BlockItem()
        {
            //this.blockShape = (Court.SHAPE)(Court.rnd.Next(Enum.GetValues(typeof(Court.SHAPE)).Length));
            this.blockShape = (Court.SHAPE)(1);
            this.blockColor = (int)this.blockShape;
        }
    }
}
