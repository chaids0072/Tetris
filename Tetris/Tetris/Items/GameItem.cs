using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Tetris
{
    class GameItem
    {
        public const int widthInSquares = 10;           
        public const int heightInSquares = 20;
        public int score = 0;

        public BlockItem currentBlocks = new BlockItem();
        public BlockItem nextBlock = new BlockItem();
        public Boolean currentFallingBlock = false;
        public Boolean nextFallingBlock = true;
        public List<BlockItem> blocks = new List<BlockItem>();

        public GameItem()
        {
        }
    }
}
