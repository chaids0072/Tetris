using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tetris.Functions;

namespace Tetris
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Court.picMain = this.pictureBox1;

            Court.bitmap = new Bitmap(Court.picMain.Width, Court.picMain.Height);

            Court.graphic = Graphics.FromImage(Court.bitmap);

            Court.newGame = new GameItem();
        }

        //private void Form1_Resize_1(object sender, EventArgs e)
        //{
        //    Court.bitmap = new Bitmap(Court.picMain.Width, Court.picMain.Height);
        //    Court.graphic = Graphics.FromImage(Court.bitmap);
        //}

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateFunc.updateGameField();
            PaintFunc.paint();
        }

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            UpdateFunc.OnKeyDown(e.KeyCode);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            UpdateFunc.updateBlockDown();
        }
    }
}
