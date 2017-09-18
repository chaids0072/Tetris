using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tetris.Functions
{
    class UpdateFunc
    {
        public static Boolean checkBlocks(BlockItem replaceBlock)
        {
            Boolean canMove = true;

            for (int k = 0; k < replaceBlock.locations.Count; k++)
            {
                Point tempReplacePoint = replaceBlock.locations[k];
                if (tempReplacePoint.X < 0 || tempReplacePoint.X >= 10
                    || tempReplacePoint.Y >= 20 || tempReplacePoint.Y < 0)
                {
                    return canMove = false;
                }
            }

            for (int i = 0; i < Court.newGame.blocks.Count; i++)
            {
                for (int j = 0; j < Court.newGame.blocks[i].locations.Count; j++)
                {
                    Point tempPoint = Court.newGame.blocks[i].locations[j];
                    for (int k = 0; k < replaceBlock.locations.Count; k++)
                    {
                        Point tempReplacePoint = replaceBlock.locations[k];
                        if (tempPoint.X == tempReplacePoint.X && tempPoint.Y == tempReplacePoint.Y)
                        {
                            return canMove = false;
                        }
                    }
                }
            }

            return canMove;
        }

        public static void updateBlockIndex(Keys argKey)
        {
            BlockItem replaceBlock = new BlockItem();

            for (int j = 0; j < Court.newGame.currentBlocks.locations.Count; j++)
            {
                Point tempPoint = Court.newGame.currentBlocks.locations[j];
                switch (argKey)
                {
                    case Keys.Left:
                        tempPoint.X -= 1;
                        break;
                    case Keys.Right:
                        tempPoint.X += 1;
                        break;
                    case Keys.Up:
                        break;
                    case Keys.Down:
                        break;
                    default:
                        break;
                }
                replaceBlock.locations.Add(tempPoint);
            }

            replaceBlock.blockColor = Court.newGame.currentBlocks.blockColor;
            replaceBlock.myRotation = Court.newGame.currentBlocks.myRotation;
            replaceBlock.blockShape = Court.newGame.currentBlocks.blockShape;

            if (checkBlocks(replaceBlock))
            {
                Court.newGame.currentBlocks = replaceBlock;
            }
        }

        public static void updateBlockDown()
        {
            BlockItem replaceBlock = new BlockItem();

            for (int j = 0; j < Court.newGame.currentBlocks.locations.Count; j++)
            {
                Point tempPoint = Court.newGame.currentBlocks.locations[j];
                tempPoint.Y += 1;
                replaceBlock.locations.Add(tempPoint);
            }

            replaceBlock.blockColor = Court.newGame.currentBlocks.blockColor;
            replaceBlock.myRotation = Court.newGame.currentBlocks.myRotation;
            replaceBlock.blockShape = Court.newGame.currentBlocks.blockShape;

            if (checkBlocks(replaceBlock))
            {
                Court.newGame.currentBlocks = replaceBlock;
            }
            else
            {
                Court.newGame.blocks.Add(Court.newGame.currentBlocks);
                Court.newGame.currentBlocks = new BlockItem();
                Court.newGame.currentFallingBlock = false;
            }
        }


        public static void updateGameField()
        {
            if (!Court.newGame.currentFallingBlock)
            {
                switch (Court.newGame.currentBlocks.blockShape)
                {
                    case Court.SHAPE.SQUARE:
                        Court.newGame.currentBlocks.locations.Add(new Point(4, 0));
                        Court.newGame.currentBlocks.locations.Add(new Point(5, 0));
                        Court.newGame.currentBlocks.locations.Add(new Point(4, 1));
                        Court.newGame.currentBlocks.locations.Add(new Point(5, 1));
                        break;
                    case Court.SHAPE.LONG:
                        Court.newGame.currentBlocks.locations.Add(new Point(3, 0));
                        Court.newGame.currentBlocks.locations.Add(new Point(4, 0));
                        Court.newGame.currentBlocks.locations.Add(new Point(5, 0));
                        Court.newGame.currentBlocks.locations.Add(new Point(6, 0));
                        break;
                    case Court.SHAPE.WIDGET:
                        Court.newGame.currentBlocks.locations.Add(new Point(5, 0));
                        Court.newGame.currentBlocks.locations.Add(new Point(4, 1));
                        Court.newGame.currentBlocks.locations.Add(new Point(5, 1));
                        Court.newGame.currentBlocks.locations.Add(new Point(6, 1));
                        break;
                    case Court.SHAPE.LEFTHOOK:
                        Court.newGame.currentBlocks.locations.Add(new Point(7, 0));
                        Court.newGame.currentBlocks.locations.Add(new Point(5, 1));
                        Court.newGame.currentBlocks.locations.Add(new Point(6, 1));
                        Court.newGame.currentBlocks.locations.Add(new Point(7, 1));
                        break;
                    case Court.SHAPE.RIGHTHOOK:
                        Court.newGame.currentBlocks.locations.Add(new Point(5, 0));
                        Court.newGame.currentBlocks.locations.Add(new Point(5, 1));
                        Court.newGame.currentBlocks.locations.Add(new Point(6, 1));
                        Court.newGame.currentBlocks.locations.Add(new Point(7, 1));
                        break;
                    case Court.SHAPE.LEFTSNAKE:
                        Court.newGame.currentBlocks.locations.Add(new Point(6, 0));
                        Court.newGame.currentBlocks.locations.Add(new Point(7, 0));
                        Court.newGame.currentBlocks.locations.Add(new Point(5, 1));
                        Court.newGame.currentBlocks.locations.Add(new Point(6, 1));
                        break;
                    case Court.SHAPE.RIGHTSNAKE:
                        Court.newGame.currentBlocks.locations.Add(new Point(5, 0));
                        Court.newGame.currentBlocks.locations.Add(new Point(6, 0));
                        Court.newGame.currentBlocks.locations.Add(new Point(6, 1));
                        Court.newGame.currentBlocks.locations.Add(new Point(7, 1));
                        break;
                    default:
                        break;
                }
                if (!checkBlocks(Court.newGame.currentBlocks)) {
                    Court.newGame = new GameItem();
                } else {
                    Court.newGame.currentFallingBlock = true;
                }
            }
        }

        public static void OnKeyDown(Keys argKey)
        {
            switch (argKey)
            {
                case Keys.Left:
                    updateBlockIndex(Keys.Left);
                    break;
                case Keys.Right:
                    updateBlockIndex(Keys.Right);
                    break;
                case Keys.Up:
                    changeBlockDirection();
                    break;
                case Keys.Down:
                    Court.speedTimer.Interval = 150;
                    updateBlockDown();
                    break;
                default:
                    break;
            }
            Court.speedTimer.Interval = 500; ;
        }

        public static void changeBlockDirection()
        {

            BlockItem replaceBlock = new BlockItem();

            for (int j = 0; j < Court.newGame.currentBlocks.locations.Count; j++)
            {
                Point tempPoint = Court.newGame.currentBlocks.locations[j];
                replaceBlock.locations.Add(tempPoint);
            }

            replaceBlock.blockColor = Court.newGame.currentBlocks.blockColor;
            replaceBlock.blockShape = Court.newGame.currentBlocks.blockShape;

            Point oldPosition1 = Court.newGame.currentBlocks.locations[0];
            Point oldPosition2 = Court.newGame.currentBlocks.locations[1];
            Point oldPosition3 = Court.newGame.currentBlocks.locations[2];
            Point oldPosition4 = Court.newGame.currentBlocks.locations[3];

            switch (Court.newGame.currentBlocks.blockShape)
            {
                case Court.SHAPE.SQUARE:
                    break;
                case Court.SHAPE.LONG:
                    switch (Court.newGame.currentBlocks.myRotation)
                    {
                        case BlockItem.RotateDirections.North:
                            replaceBlock.myRotation = BlockItem.RotateDirections.East;
                            replaceBlock.locations[0] = new Point(oldPosition3.X, oldPosition3.Y - 2);
                            replaceBlock.locations[1] = new Point(oldPosition3.X, oldPosition3.Y - 1);
                            replaceBlock.locations[3] = new Point(oldPosition3.X, oldPosition3.Y + 1);
                            break;
                        case BlockItem.RotateDirections.East:
                            replaceBlock.myRotation = BlockItem.RotateDirections.North;
                            replaceBlock.locations[0] = new Point(oldPosition3.X - 2, oldPosition3.Y);
                            replaceBlock.locations[1] = new Point(oldPosition3.X - 1, oldPosition3.Y);
                            replaceBlock.locations[3] = new Point(oldPosition3.X + 1, oldPosition3.Y);
                            break;
                    }
                    break;
                case Court.SHAPE.WIDGET:
                    switch (Court.newGame.currentBlocks.myRotation)
                    {
                        case BlockItem.RotateDirections.North:
                            replaceBlock.myRotation = BlockItem.RotateDirections.East;
                            replaceBlock.locations[0] = new Point(oldPosition3.X + 1, oldPosition3.Y);
                            replaceBlock.locations[1] = new Point(oldPosition3.X, oldPosition3.Y - 1);
                            replaceBlock.locations[2] = new Point(oldPosition3.X, oldPosition3.Y);
                            replaceBlock.locations[3] = new Point(oldPosition3.X, oldPosition3.Y + 1);
                            break;
                        case BlockItem.RotateDirections.East:
                            replaceBlock.myRotation = BlockItem.RotateDirections.South;
                            replaceBlock.locations[0] = new Point(oldPosition3.X, oldPosition3.Y + 1);
                            replaceBlock.locations[1] = new Point(oldPosition3.X + 1, oldPosition3.Y);
                            replaceBlock.locations[2] = new Point(oldPosition3.X, oldPosition3.Y);
                            replaceBlock.locations[3] = new Point(oldPosition3.X - 1, oldPosition3.Y);
                            break;
                        case BlockItem.RotateDirections.South:
                            replaceBlock.myRotation = BlockItem.RotateDirections.West;
                            replaceBlock.locations[0] = new Point(oldPosition3.X - 1, oldPosition3.Y);
                            replaceBlock.locations[1] = new Point(oldPosition3.X, oldPosition3.Y + 1);
                            replaceBlock.locations[2] = new Point(oldPosition3.X, oldPosition3.Y);
                            replaceBlock.locations[3] = new Point(oldPosition3.X, oldPosition3.Y - 1);
                            break;
                        case BlockItem.RotateDirections.West:
                            replaceBlock.myRotation = BlockItem.RotateDirections.North;
                            replaceBlock.locations[0] = new Point(oldPosition3.X, oldPosition3.Y - 1);
                            replaceBlock.locations[1] = new Point(oldPosition3.X - 1, oldPosition3.Y);
                            replaceBlock.locations[2] = new Point(oldPosition3.X, oldPosition3.Y);
                            replaceBlock.locations[3] = new Point(oldPosition3.X + 1, oldPosition3.Y);
                            break;
                    }
                    break;
                case Court.SHAPE.LEFTHOOK:
                    switch (Court.newGame.currentBlocks.myRotation)
                    {
                        case BlockItem.RotateDirections.North:
                            replaceBlock.myRotation = BlockItem.RotateDirections.East;
                            replaceBlock.locations[0] = new Point(oldPosition3.X + 1, oldPosition3.Y + 1);
                            replaceBlock.locations[1] = new Point(oldPosition3.X, oldPosition3.Y - 1);
                            replaceBlock.locations[2] = new Point(oldPosition3.X, oldPosition3.Y);
                            replaceBlock.locations[3] = new Point(oldPosition3.X, oldPosition3.Y + 1);
                            break;
                        case BlockItem.RotateDirections.East:
                            replaceBlock.myRotation = BlockItem.RotateDirections.South;
                            replaceBlock.locations[0] = new Point(oldPosition3.X - 1, oldPosition3.Y + 1);
                            replaceBlock.locations[1] = new Point(oldPosition3.X + 1, oldPosition3.Y);
                            replaceBlock.locations[2] = new Point(oldPosition3.X, oldPosition3.Y);
                            replaceBlock.locations[3] = new Point(oldPosition3.X - 1, oldPosition3.Y);
                            break;
                        case BlockItem.RotateDirections.South:
                            replaceBlock.myRotation = BlockItem.RotateDirections.West;
                            replaceBlock.locations[0] = new Point(oldPosition3.X - 1, oldPosition3.Y - 1);
                            replaceBlock.locations[1] = new Point(oldPosition3.X, oldPosition3.Y + 1);
                            replaceBlock.locations[2] = new Point(oldPosition3.X, oldPosition3.Y);
                            replaceBlock.locations[3] = new Point(oldPosition3.X, oldPosition3.Y - 1);
                            break;
                        case BlockItem.RotateDirections.West:
                            replaceBlock.myRotation = BlockItem.RotateDirections.North;
                            replaceBlock.locations[0] = new Point(oldPosition3.X + 1, oldPosition3.Y - 1);
                            replaceBlock.locations[1] = new Point(oldPosition3.X - 1, oldPosition3.Y);
                            replaceBlock.locations[2] = new Point(oldPosition3.X, oldPosition3.Y);
                            replaceBlock.locations[3] = new Point(oldPosition3.X + 1, oldPosition3.Y);
                            break;
                    }
                    break;
                case Court.SHAPE.RIGHTHOOK:
                    switch (Court.newGame.currentBlocks.myRotation)
                    {
                        case BlockItem.RotateDirections.North:
                            replaceBlock.myRotation = BlockItem.RotateDirections.East;
                            replaceBlock.locations[0] = new Point(oldPosition3.X + 1, oldPosition3.Y - 1);
                            replaceBlock.locations[1] = new Point(oldPosition3.X, oldPosition3.Y - 1);
                            replaceBlock.locations[2] = new Point(oldPosition3.X, oldPosition3.Y);
                            replaceBlock.locations[3] = new Point(oldPosition3.X, oldPosition3.Y + 1);
                            break;
                        case BlockItem.RotateDirections.East:
                            replaceBlock.myRotation = BlockItem.RotateDirections.South;
                            replaceBlock.locations[0] = new Point(oldPosition3.X + 1, oldPosition3.Y + 1);
                            replaceBlock.locations[1] = new Point(oldPosition3.X + 1, oldPosition3.Y);
                            replaceBlock.locations[2] = new Point(oldPosition3.X, oldPosition3.Y);
                            replaceBlock.locations[3] = new Point(oldPosition3.X - 1, oldPosition3.Y);
                            break;
                        case BlockItem.RotateDirections.South:
                            replaceBlock.myRotation = BlockItem.RotateDirections.West;
                            replaceBlock.locations[0] = new Point(oldPosition3.X - 1, oldPosition3.Y + 1);
                            replaceBlock.locations[1] = new Point(oldPosition3.X, oldPosition3.Y + 1);
                            replaceBlock.locations[2] = new Point(oldPosition3.X, oldPosition3.Y);
                            replaceBlock.locations[3] = new Point(oldPosition3.X, oldPosition3.Y - 1);
                            break;
                        case BlockItem.RotateDirections.West:
                            replaceBlock.myRotation = BlockItem.RotateDirections.North;
                            replaceBlock.locations[0] = new Point(oldPosition3.X - 1, oldPosition3.Y - 1);
                            replaceBlock.locations[1] = new Point(oldPosition3.X - 1, oldPosition3.Y);
                            replaceBlock.locations[2] = new Point(oldPosition3.X, oldPosition3.Y);
                            replaceBlock.locations[3] = new Point(oldPosition3.X + 1, oldPosition3.Y);
                            break;
                    }
                    break;
                case Court.SHAPE.LEFTSNAKE:
                    switch (Court.newGame.currentBlocks.myRotation)
                    {
                        case BlockItem.RotateDirections.North:
                            replaceBlock.myRotation = BlockItem.RotateDirections.East;
                            replaceBlock.locations[0] = new Point(oldPosition1.X, oldPosition1.Y);
                            replaceBlock.locations[1] = new Point(oldPosition1.X, oldPosition1.Y + 1);
                            replaceBlock.locations[2] = new Point(oldPosition1.X - 1, oldPosition1.Y - 1);
                            replaceBlock.locations[3] = new Point(oldPosition1.X - 1, oldPosition1.Y);
                            break;
                        case BlockItem.RotateDirections.East:
                            replaceBlock.myRotation = BlockItem.RotateDirections.South;
                            replaceBlock.locations[0] = new Point(oldPosition1.X, oldPosition1.Y);
                            replaceBlock.locations[1] = new Point(oldPosition1.X - 1, oldPosition1.Y);
                            replaceBlock.locations[2] = new Point(oldPosition1.X + 1, oldPosition1.Y - 1);
                            replaceBlock.locations[3] = new Point(oldPosition1.X, oldPosition1.Y - 1);
                            break;
                        case BlockItem.RotateDirections.South:
                            replaceBlock.myRotation = BlockItem.RotateDirections.West;
                            replaceBlock.locations[0] = new Point(oldPosition1.X, oldPosition1.Y);
                            replaceBlock.locations[1] = new Point(oldPosition1.X, oldPosition1.Y - 1);
                            replaceBlock.locations[2] = new Point(oldPosition1.X + 1, oldPosition1.Y + 1);
                            replaceBlock.locations[3] = new Point(oldPosition1.X + 1, oldPosition1.Y);
                            break;
                        case BlockItem.RotateDirections.West:
                            replaceBlock.myRotation = BlockItem.RotateDirections.North;
                            replaceBlock.locations[0] = new Point(oldPosition1.X, oldPosition1.Y);
                            replaceBlock.locations[1] = new Point(oldPosition1.X + 1, oldPosition1.Y);
                            replaceBlock.locations[2] = new Point(oldPosition1.X, oldPosition1.Y + 1);
                            replaceBlock.locations[3] = new Point(oldPosition1.X - 1, oldPosition1.Y + 1);
                            break;
                    }
                    break;
                case Court.SHAPE.RIGHTSNAKE:
                    switch (Court.newGame.currentBlocks.myRotation)
                    {
                        case BlockItem.RotateDirections.North:
                            replaceBlock.myRotation = BlockItem.RotateDirections.East;
                            replaceBlock.locations[0] = new Point(oldPosition3.X + 1, oldPosition3.Y - 1);
                            replaceBlock.locations[1] = new Point(oldPosition3.X + 1, oldPosition3.Y);
                            replaceBlock.locations[2] = new Point(oldPosition3.X, oldPosition3.Y);
                            replaceBlock.locations[3] = new Point(oldPosition3.X, oldPosition3.Y + 1);
                            break;
                        case BlockItem.RotateDirections.East:
                            replaceBlock.myRotation = BlockItem.RotateDirections.South;
                            replaceBlock.locations[0] = new Point(oldPosition3.X + 1, oldPosition3.Y + 1);
                            replaceBlock.locations[1] = new Point(oldPosition3.X, oldPosition3.Y + 1);
                            replaceBlock.locations[2] = new Point(oldPosition3.X, oldPosition3.Y);
                            replaceBlock.locations[3] = new Point(oldPosition3.X - 1, oldPosition3.Y);
                            break;
                        case BlockItem.RotateDirections.South:
                            replaceBlock.myRotation = BlockItem.RotateDirections.West;
                            replaceBlock.locations[0] = new Point(oldPosition3.X - 1, oldPosition3.Y + 1);
                            replaceBlock.locations[1] = new Point(oldPosition3.X - 1, oldPosition3.Y);
                            replaceBlock.locations[2] = new Point(oldPosition3.X, oldPosition3.Y);
                            replaceBlock.locations[3] = new Point(oldPosition3.X, oldPosition3.Y - 1);
                            break;
                        case BlockItem.RotateDirections.West:
                            replaceBlock.myRotation = BlockItem.RotateDirections.North;
                            replaceBlock.locations[0] = new Point(oldPosition3.X - 1, oldPosition3.Y - 1);
                            replaceBlock.locations[1] = new Point(oldPosition3.X, oldPosition3.Y - 1);
                            replaceBlock.locations[2] = new Point(oldPosition3.X, oldPosition3.Y);
                            replaceBlock.locations[3] = new Point(oldPosition3.X + 1, oldPosition3.Y);
                            break;
                    }
                    break;
                default:
                    break;
            }

            if (checkBlocks(replaceBlock))
            {
                Court.newGame.currentBlocks = replaceBlock;
            }
        }
    }
}
