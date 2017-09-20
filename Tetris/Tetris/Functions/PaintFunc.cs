using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Tetris.Functions
{
    class PaintFunc
    {
        public static void paint()
        {
            Court.graphic.Clear(Color.White);
            Court.graphicSecondary.Clear(Color.FloralWhite);
            //paintGrid();
            paintBlocks();
            Court.picMain.Image = Court.bitmap;
            Court.picSecondary.Image = Court.bitmapSecondary;
        }

        private static void paintBlocks()
        {
            //paint current block
            for (int i = 0; i < Court.newGame.currentBlocks.locations.Count; i++)
            {
                Point tempPoint = Court.newGame.currentBlocks.locations[i];
                Court.graphic.DrawImage(Court.blockImage, new Rectangle(Court.imagePixel * tempPoint.X, Court.imagePixel * tempPoint.Y, 32, 32), new Rectangle(Court.imagePixel * Court.newGame.currentBlocks.blockColor, 0, 32, 32), GraphicsUnit.Pixel);
            }

            //paint next block
            for (int j = 0; j < Court.newGame.nextBlock.locations.Count; j++)
            {
                Point tempPoint = Court.newGame.nextBlock.locations[j];
                Court.graphicSecondary.DrawImage(Court.blockImage, new Rectangle(Court.imagePixel * tempPoint.X , Court.imagePixel * tempPoint.Y, 32, 32), new Rectangle(Court.imagePixel * Court.newGame.nextBlock.blockColor, 0, 32, 32), GraphicsUnit.Pixel);
            }

            //Paint stable blocks
            for (int i = 0; i < Court.newGame.blocks.Count; i++)
            {
                for (int j = 0; j < Court.newGame.blocks[i].locations.Count; j++)
                {
                    Point tempPoint = Court.newGame.blocks[i].locations[j];
                    Court.graphic.DrawImage(Court.blockImage, new Rectangle(Court.imagePixel * tempPoint.X, Court.imagePixel * tempPoint.Y, 32, 32), new Rectangle(Court.imagePixel * Court.newGame.blocks[i].blockColor, 0, 32, 32), GraphicsUnit.Pixel);
                }
            }
        }

        public static void paintGrid()
        {
            Pen p = new Pen(Color.Blue);

            for (int y = 0; y <= GameItem.heightInSquares; ++y)
            {
                Court.graphic.DrawLine(p, 0, y * (Court.gameHeight / GameItem.heightInSquares), GameItem.widthInSquares * (Court.gameWidth / GameItem.widthInSquares), y * (Court.gameHeight / GameItem.heightInSquares));
            }

            for (int x = 0; x <= GameItem.widthInSquares; ++x)
            {
                Court.graphic.DrawLine(p, x * (Court.gameWidth / GameItem.widthInSquares), 0, x * (Court.gameWidth / GameItem.widthInSquares), GameItem.heightInSquares * (Court.gameHeight / GameItem.heightInSquares));
            }
        }
    }
}
