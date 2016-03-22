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
        int x1;
        int y1;
        int x2;
        int y2;
        Color CurrentColor = Color.Black;
        Color LasticColor = Color.White;
        Bitmap snapshot;
        Bitmap tempDraw;
        bool MouseD;
        Graphics g;
        int sizePen;
        int nomerKadra = 0, nomerKadra2 = 0, createProjectS = 0;
        string selectedTool, selected;
        PictureBox pictureBox2 = new PictureBox();
        PictureBox pictureBox3 = new PictureBox();
        PictureBox pictureBox4 = new PictureBox();
        Bitmap[] bit = new Bitmap[100];
        Button[] buttonKadr = new Button[100];
        List<Bitmap> bitMapList = new List<Bitmap>();

        public Form1()
        {
            InitializeComponent();
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
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(0, new MenuItem("Очистить", new EventHandler(RightMouseButton_Click)));
                m.Show(this, new Point(e.X, e.Y));
            }
            tempDraw = (Bitmap)snapshot.Clone();
            x1 = e.X;
            y1 = e.Y;
            MouseD = true; 
        }

        private void RightMouseButton_Click(object sender, EventArgs e)
        {
            selectedTool = "Clean";
            if (tempDraw != null)
            {
                Graphics g = Graphics.FromImage(tempDraw);
                g.Clear(SystemColors.Window);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            MouseD = false;
            snapshot = (Bitmap)tempDraw.Clone();
            bitMapList[nomerKadra] = tempDraw;
            imageList1.Images[nomerKadra] = new Bitmap(tempDraw, imageList1.ImageSize);
            buttonKadr[nomerKadra].Image = imageList1.Images[nomerKadra];
        }

        private void button7_Click(object sender, EventArgs e)
        {
            selectedTool = "sterka";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            selectedTool = "kist";
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
            MessageBox.Show("(", "Упс", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            switch (selectedTool)
            {
                case "kist":
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
                    break;
                case "sterka":
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
                    break;
                case "Clean":
                    if (tempDraw != null)
                    {
                        Graphics g = Graphics.FromImage(tempDraw);
                        //e.Graphics.Clear(Color.White);
                        for (int x = 0; x < snapshot.Width; x++)
                        {
                            for (int y = 0; y < snapshot.Height; y++)
                            {
                                Color newColor = Color.FromArgb(255, 255, 255);
                                tempDraw.SetPixel(x, y, newColor);
                            }
                        }
                        g.Dispose();
                    }
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
            createProject();
        }

        private void createProject()
        {
            if (createProjectS == 0)
            {
                nomerKadra = 0;
                createProjectS = 1;
                snapshot = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                button8.BackColor = Color.Black;
                for (int x = 0; x < snapshot.Width; x++)
                {
                    for (int y = 0; y < snapshot.Height; y++)
                    {

                        Color newColor = Color.FromArgb(255, 255, 255);
                        snapshot.SetPixel(x, y, newColor);
                    }
                }

                bitMapList.Add(snapshot);
                imageList1.Images.Add(snapshot);
                createButtonForKadr();
            }
            else
            {

            }
        }

        private void createButtonForKadr()
        {
            Button Mutton = new Button();
            Mutton.Size = new Size(100, 80);
            Mutton.Image = imageList1.Images[nomerKadra];
            Mutton.Click += new EventHandler(button_Click);
            Mutton.TabIndex = nomerKadra;
            flowLayoutPanel1.Controls.Add(Mutton);
            buttonKadr[nomerKadra] = Mutton;

        }

        void button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            //kadr = sender;
            bitMapList[nomerKadra] = tempDraw;
            imageList1.Images[nomerKadra] = new Bitmap(tempDraw, imageList1.ImageSize);
            buttonKadr[nomerKadra].Image = imageList1.Images[nomerKadra];
            int inKadra = (button.TabIndex);
            nomerKadra = inKadra;
            tempDraw = (Bitmap)bitMapList[inKadra].Clone();
            snapshot = (Bitmap)bitMapList[inKadra].Clone();
            pictureBox1.Image = bitMapList[inKadra];

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "bmp";
            sfd.Filter = "Image files (*.bmp)|*.bmp|All files (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)

                snapshot.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            DialogResult dr = open_dialog.ShowDialog();
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            createKadr();
        }

        private void createKadr()
        {
            bitMapList[nomerKadra] = tempDraw;
            imageList1.Images[nomerKadra] = new Bitmap(tempDraw, imageList1.ImageSize);
            buttonKadr[nomerKadra].Image = imageList1.Images[nomerKadra];
            nomerKadra2++;
            nomerKadra = nomerKadra2;
            snapshot = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            for (int x = 0; x < snapshot.Width; x++)
            {
                for (int y = 0; y < snapshot.Height; y++)
                {
                    Color newColor = Color.FromArgb(255, 255, 255);
                    snapshot.SetPixel(x, y, newColor);
                }
            }

            bitMapList.Add(snapshot);
            imageList1.Images.Add(snapshot);
            tempDraw = (Bitmap)snapshot.Clone();
            pictureBox1.Image = bitMapList[nomerKadra];
            createButtonForKadr();
        }
    }

    
}
