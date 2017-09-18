using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tetris
{
    class Court
    {
        #region GamePicutreSettings
        public static PictureBox picMain;
        public static Bitmap bitmap;
        public static Graphics graphic;
        #endregion

        #region GameParameterSettings
        public static GameItem newGame;
        public static Random rnd = new Random();
        public enum SHAPE { SQUARE = 0, LONG = 1, WIDGET = 2, LEFTHOOK = 3, RIGHTHOOK = 4, LEFTSNAKE = 5, RIGHTSNAKE = 6 }
        LinkedList<BlockItem> blocks = new LinkedList<BlockItem>();
        #endregion

        #region PictureParameterSettings
        public static Image blockImage = Image.FromFile("C:\\Users\\User\\Source\\Repos\\Tetris\\blockBrush.png");
        public static int imagePixel = 32;
        public static int gameWidth = 320;
        public static int gameHeight = 640;
        #endregion
    }
}
