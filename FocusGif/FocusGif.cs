using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gif.Components;
using System.Drawing.Drawing2D;

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
        bool MouseD, OffOn = false;
        int sizePen, selectedTool;
        object kadr;
        int nomerKadra = 0, nomerKadra2 = 0, createProjectS = 0;
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
            else
            {
                x1 = e.X;
                y1 = e.Y;
                tempDraw = (Bitmap)snapshot.Clone();
                MouseD = true;
            }
            if (Control.ModifierKeys == Keys.Alt)
            {
                Color c = (tempDraw).GetPixel(e.X, e.Y);
                if (e.Button == MouseButtons.Left)
                    label1.BackColor = c;
            }
        }

        private void RightMouseButton_Click(object sender, EventArgs e)
        {
            if (tempDraw != null)
            {
                Graphics g = Graphics.FromImage(tempDraw);
                g.Clear(SystemColors.Window);
                Graphics g2 = Graphics.FromImage(snapshot);
                g2.Clear(SystemColors.Window);
                pictureBox1.Invalidate();
                pictureBox1.Update();
                buttonKadr[nomerKadra].Image = tempDraw;
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
            selectedTool = 1;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            selectedTool = 0;
        }

        private void button8_Click(object sender, EventArgs e)
        {

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
                case 0:
                    if (tempDraw != null)
                    {
                        Graphics g = Graphics.FromImage(tempDraw);
                        Pen p = new Pen(CurrentColor);
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
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
                        p.StartCap = p.EndCap = LineCap.Round;
                        p.Alignment = PenAlignment.Inset;
                        g.DrawLine(p, x1, y1, x2, y2);
                        p.Dispose();
                        e.Graphics.DrawImageUnscaled(tempDraw, 0, 0);
                        g.Dispose();
                        x1 = x2;
                        y1 = y2;
                    }
                    break;
                case 1:
                    if (tempDraw != null)
                    {
                        Graphics g = Graphics.FromImage(tempDraw);
                        Pen p2 = new Pen(LasticColor);
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
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
                        p2.StartCap = p2.EndCap = LineCap.Round;
                        p2.Alignment = PenAlignment.Inset;
                        g.DrawLine(p2, x1, y1, x2, y2);
                        p2.Dispose();
                        e.Graphics.DrawImageUnscaled(tempDraw, 0, 0);
                        g.Dispose();
                        x1 = x2;
                        y1 = y2;
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
            button9.Enabled = true;
            button10.Enabled = true;
            button11.Enabled = true;
            button12.Enabled = true;
            button17.Enabled = true;
            button18.Enabled = true;
            createProject();
        }

        private void createProject()
        {
            if (createProjectS == 0)
            {
                nomerKadra = 0;
                createProjectS = 1;
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
                createButtonForKadr();
                pictureBox1.Image = snapshot;
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
            Mutton.Text = (nomerKadra+1).ToString();
            Mutton.TextAlign = ContentAlignment.TopRight;
            flowLayoutPanel1.Controls.Add(Mutton);
            buttonKadr[nomerKadra] = Mutton;

        }

        void button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            kadr = sender;
            bitMapList[nomerKadra] = tempDraw;
            imageList1.Images[nomerKadra] = new Bitmap(tempDraw, imageList1.ImageSize);
            buttonKadr[nomerKadra].Image = imageList1.Images[nomerKadra];
            int inKadra = (button.TabIndex);
            nomerKadra = inKadra;
            tempDraw = (Bitmap)bitMapList[inKadra].Clone();
            snapshot = (Bitmap)bitMapList[inKadra].Clone();
            pictureBox1.Image = bitMapList[inKadra];

        }
        string path;
        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "gif";
            sfd.Filter = "Image Files(*.gif)|*.GIF|All files (*.*)|*.*"; 
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                path = sfd.FileName;
                gif();
            }
        }

        private void gif()
    {
            AnimatedGifEncoder gif = new AnimatedGifEncoder();
            gif.Start(path);
            gif.SetDelay(500);   
            gif.SetRepeat(0);
            for (int i = 0, count = nomerKadra2 + 1; i < count; i++)
            {
                gif.AddFrame(bitMapList[i]);
            }
            gif.Finish();
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

        int currentImage = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox2.Image = bitMapList[currentImage];
            currentImage++;
            if (currentImage > nomerKadra2)
            {
                currentImage = 0;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            timer1.Interval = trackBar1.Value;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (OffOn == false)
            {
                timer1.Enabled = true;
                OffOn = true;
                pictureBox2.Width = pictureBox1.Width;
                pictureBox2.Height = pictureBox1.Height;
                pictureBox2.Left = pictureBox1.Left;
                pictureBox2.BorderStyle = pictureBox1.BorderStyle;
                pictureBox2.Top = pictureBox1.Top;
                pictureBox2.Visible = true;
                pictureBox1.Visible = false;
                //Добавляем элемент на форму.
                this.Controls.Add(pictureBox2);
            }
            else
            {
                timer1.Enabled = false;
                OffOn = false;
                pictureBox2.Visible = false;
                pictureBox1.Visible = true;
                currentImage = 0;

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (createProjectS == 1)
            {
                if (MessageBox.Show("Проект не сохранен! Выйти без сохранения?", "Упс",
                   MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Button btn = (Button)kadr;
            if (nomerKadra2 != 0)
            {
                Bitmap kas = bitMapList[nomerKadra];
                if (flowLayoutPanel1.Controls.Contains(btn))
                {
                    btn.Click -= new EventHandler(button_Click);
                    flowLayoutPanel1.Controls.Remove(btn);
                    btn.Dispose();
                    imageList1.Images.RemoveAt(btn.TabIndex);
                    bitMapList.Remove(kas);


                    if (nomerKadra != 0)
                    {
                        nomerKadra2--;
                        izmenenie_poryadka();
                        nomerKadra = nomerKadra - 1;
                        tempDraw = (Bitmap)bitMapList[nomerKadra].Clone();
                        snapshot = (Bitmap)bitMapList[nomerKadra].Clone();
                        pictureBox1.Image = tempDraw;
                    }
                    else
                    {
                        if (nomerKadra == nomerKadra2)
                        {
                            nomerKadra2--;
                            nomerKadra = nomerKadra - 1;
                            tempDraw = (Bitmap)bitMapList[nomerKadra].Clone();
                            snapshot = (Bitmap)bitMapList[nomerKadra].Clone();
                            pictureBox1.Image = tempDraw;
                        }
                        else
                        {
                            nomerKadra2--;
                            izmenenie_poryadka();
                            tempDraw = (Bitmap)bitMapList[nomerKadra].Clone();
                            snapshot = (Bitmap)bitMapList[nomerKadra].Clone();
                            pictureBox1.Image = tempDraw;
                        }

                    }

                }
            }
        }

        void izmenenie_poryadka()
        {
            for (int j = nomerKadra; j < (nomerKadra2 + 2); j++)
            {
                buttonKadr[j] = buttonKadr[j + 1];
            }

            for (int i = nomerKadra; i < (nomerKadra2 + 1); i++)
            {
                Button but = (Button)buttonKadr[i];
                int ser = but.TabIndex;
                but.TabIndex = (ser - 1);
                but.Text = (ser).ToString();
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите очистить полосу кадров?", "Упс",
                   MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                for (int i = nomerKadra2; i > -1; i--)
                {
                    nomerKadra = i;
                    Button btn = (Button)buttonKadr[i];
                    Bitmap kas = bitMapList[nomerKadra];
                    if (flowLayoutPanel1.Controls.Contains(btn))
                    {
                        btn.Click -= new EventHandler(button_Click);
                        flowLayoutPanel1.Controls.Remove(btn);
                        btn.Dispose();
                        imageList1.Images.RemoveAt(btn.TabIndex);
                        bitMapList.Remove(kas);
                        nomerKadra2--;
                    }
                }


                izmenenie_poryadka();
                createProjectS = 0;
                nomerKadra2 = 0;
                createProject();
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            colorDialog1.Color = lb.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                lb.BackColor = colorDialog1.Color;
        }

        private void label1_BackColorChanged(object sender, EventArgs e)
        {
            CurrentColor = label1.BackColor;
            label1.Invalidate();
        }
    }

    
}
