using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FocusGif
{
    public partial class Form1 : Form
    {
        Color CurrentColor = Color.Black;
        Color LasticColor = Color.White;
        Point CurrentPoint;
        Point PrevPoint;
        bool isPressed;
        bool colorPen = false;
        Graphics g;
        int sizePen;
        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPressed)
            {
                if (!colorPen)
                {
                    PrevPoint = CurrentPoint;
                    CurrentPoint = e.Location;
                    Paint();
                }
                else
                {
                    PrevPoint = CurrentPoint;
                    CurrentPoint = e.Location;
                    PaintLastic();
                }
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isPressed = true;
            CurrentPoint = e.Location;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(0, new MenuItem("Очистить", new EventHandler(RightMouseButton_Click)));
                m.Show(this, new Point(e.X, e.Y));
            }
        }

        private void RightMouseButton_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
        isPressed = false;
        }

        private void Paint()
        {
            Pen p = new Pen(CurrentColor);
            switch (sizePen)
            {
                case 1:
                    p = new Pen(CurrentColor, 1);
                    break;
                case 5:
                    p = new Pen(CurrentColor, 5);
                    break;
                case 10:
                    p = new Pen(CurrentColor, 10);
                    break;
            }

            g.DrawLine(p, PrevPoint, CurrentPoint);
        }

        private void PaintLastic()
        {
            Pen pe = new Pen(LasticColor);
            switch (sizePen)
            {
                case 1:
                    pe = new Pen(LasticColor, 1);
                    break;
                case 5:
                    pe = new Pen(LasticColor, 5);
                    break;
                case 10:
                    pe = new Pen(LasticColor, 10);
                    break;
            }
            g.DrawLine(pe, PrevPoint, CurrentPoint);
        }

        private void Стерка_Click(object sender, EventArgs e)
        {
            colorPen = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            colorPen = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult D = colorDialog1.ShowDialog();
            if (D == System.Windows.Forms.DialogResult.OK)
                CurrentColor = colorDialog1.Color;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            sizePen = 1;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            sizePen = 5;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            sizePen = 10;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Многого хочешь", "Упс", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    
}
