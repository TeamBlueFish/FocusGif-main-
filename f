using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaintButton
{
    public partial class Form1 : Form
    {
        Color CurrentColor = Color.Black;
        Color LasticColor = Color.White;
        Point CurrentPoint; 
        Point PrevPoint;
        bool isPressed;
        bool ColPen = false;
        Graphics g;
        public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult D = colorDialog1.ShowDialog();
            if (D == System.Windows.Forms.DialogResult.OK)
                CurrentColor = colorDialog1.Color;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Refresh();
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isPressed = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPressed)
            {
                if (!ColPen)
                {
                    PrevPoint = CurrentPoint;
                    CurrentPoint = e.Location;
                    paint();
                }
                else
                {
                    PrevPoint = CurrentPoint;
                    CurrentPoint = e.Location;
                    paintLastic();
                }
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            isPressed = true;
            CurrentPoint = e.Location;
        }
        private void paint()
        {
            Pen p = new Pen(CurrentColor);
            g.DrawLine(p, PrevPoint, CurrentPoint);
        }

        private void paintLastic()
        {
            Pen pe = new Pen(LasticColor);
            g.DrawLine(pe, PrevPoint, CurrentPoint);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ColPen = true;
        }
    }
}
