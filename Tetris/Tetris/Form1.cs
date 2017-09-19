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
            Court.picSecondary = this.pictureBox2;
            Court.bitmap = new Bitmap(Court.picMain.Width, Court.picMain.Height);
            Court.bitmapSecondary = new Bitmap(Court.picSecondary.Width, Court.picSecondary.Height);
            Court.graphic = Graphics.FromImage(Court.bitmap);
            Court.graphicSecondary = Graphics.FromImage(Court.bitmapSecondary);
            Court.newGame = new GameItem();
            Court.speedTimer = timer2;

            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateFunc.updateGameField();
            PaintFunc.paint();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            UpdateFunc.updateBlockDown();
        }

        private void Form1_PreviewKeyDown_1(object sender, PreviewKeyDownEventArgs e)
        {
            UpdateFunc.OnKeyDown(e.KeyCode);
        }

    }
}
