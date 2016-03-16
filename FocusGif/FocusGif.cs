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
    public partial class FocusGif : Form
    {
        int x1;
        int y1;
        int x2;
        int y2;
        bool selectedTool;
        Color CurrentColor = Color.Black;
        Color LasticColor = Color.White;
        Bitmap snapshot;
        Bitmap tempDraw;
        bool MouseD;
        Graphics g;
        int sizePen;
        public FocusGif()
        {
            InitializeComponent();
            snapshot = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            button8.BackColor = Color.Black;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseD)
            {
                x2 = e.X;
                y2 = e.Y;
                pictureBox1.Invalidate();
                pictureBox1.Update();
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            MouseD = true;
            x1 = e.X;
            y1 = e.Y;
            tempDraw = (Bitmap)snapshot.Clone(); 
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            MouseD = false;
            snapshot = (Bitmap)tempDraw.Clone();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            selectedTool = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            selectedTool = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult D = colorDialog1.ShowDialog();
            if (D == System.Windows.Forms.DialogResult.OK)
                CurrentColor = colorDialog1.Color;
            button8.BackColor = colorDialog1.Color;

        }

        private void button9_Click(object sender, EventArgs e)
        {
            sizePen = 1;   // размер карандаша
        }

        private void button10_Click(object sender, EventArgs e)
        {
            sizePen = 5;   // размер карандаша
        }

        private void button11_Click(object sender, EventArgs e)
        {
            sizePen = 10;   // размер карандаша
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Многого хочешь", "Упс", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (selectedTool)
            {
                if (tempDraw != null)
                {
                    Graphics g = Graphics.FromImage(tempDraw);
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

                    g.DrawLine(p, x1, y1, x2, y2);
                    p.Dispose();
                    e.Graphics.DrawImageUnscaled(tempDraw, 0, 0);
                    g.Dispose();
                    x1 = x2;
                    y1 = y2;
                }
            }
            else
            {
                if (tempDraw != null)
                {
                    Graphics g = Graphics.FromImage(tempDraw);
                    Pen p2 = new Pen(LasticColor);
                    switch (sizePen)
                    {
                        case 1:
                            p2 = new Pen(LasticColor, 1);
                            break;
                        case 5:
                            p2 = new Pen(LasticColor, 5);
                            break;
                        case 10:
                            p2 = new Pen(LasticColor, 10);
                            break;
                    }

                    g.DrawLine(p2, x1, y1, x2, y2);
                    p2.Dispose();
                    e.Graphics.DrawImageUnscaled(tempDraw, 0, 0);
                    g.Dispose();
                    x1 = x2;
                    y1 = y2;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;
            button10.Enabled = true;
            button11.Enabled = true;
        }
    }

    
}
