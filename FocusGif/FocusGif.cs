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
        int x1, X1Zaliv, Y1Zaliv;
        int y1;
        int x2;
        int y2;
        int dr;
        string path;
        double SpeedDouble;
        Color CurrentColor = Color.Black;
        Color LasticColor = Color.White;
        Bitmap snapshot;
        Bitmap tempDraw;
        bool MouseD, OffOn = false, selected = false;
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
            //textBox1.Text = "1";
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
                if (!selected && createProjectS == 1)
                {
                    x1 = e.X;
                    y1 = e.Y;
                    tempDraw = (Bitmap)snapshot.Clone();
                    MouseD = true;
                }
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
            if (MouseD || selected)
            {
                snapshot = (Bitmap)tempDraw.Clone();
                bitMapList[nomerKadra] = tempDraw;
                imageList1.Images[nomerKadra] = new Bitmap(tempDraw, imageList1.ImageSize);
                buttonKadr[nomerKadra].Image = imageList1.Images[nomerKadra];
                MouseD = false;
            }
            
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
            Mutton.Size = new Size(60, 35);
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
       

        private void gif()
    {
            AnimatedGifEncoder gif = new AnimatedGifEncoder();
            gif.Start(path);
            gif.SetDelay(dr);   
            gif.SetRepeat(0);
            for (int i = 0, count = nomerKadra2 + 1; i < count; i++)
            {
                gif.AddFrame(bitMapList[i]);
            }
            gif.Finish();
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

        private void zalivka()
        {
            Point[] Pt = new Point[pictureBox1.Width * pictureBox1.Height];
            Color c = tempDraw.GetPixel(X1Zaliv, Y1Zaliv);
            int i = 0;
            int iProhod = i;
            int iS = i;
            Pt[i] = new Point(X1Zaliv, Y1Zaliv);
            tempDraw.SetPixel(X1Zaliv, Y1Zaliv, CurrentColor);
            while (iProhod <= i)
            {
                if ((Pt[iProhod].X + 1) < pictureBox1.Width)
                    if (tempDraw.GetPixel(Pt[iProhod].X + 1, Pt[iProhod].Y) == c)
                    {
                        i++;
                        iS++;
                        Pt[i] = new Point((Pt[iProhod].X + 1), Pt[iProhod].Y);
                        tempDraw.SetPixel((Pt[iProhod].X + 1), Pt[iProhod].Y, CurrentColor);
                    }
                if (Pt[iProhod].X - 1 >= 0)
                    if (tempDraw.GetPixel((Pt[iProhod].X - 1), Pt[iProhod].Y) == c)
                    {
                        i++;
                        iS++;
                        Pt[i] = new Point((Pt[iProhod].X - 1), Pt[iProhod].Y);
                        tempDraw.SetPixel((Pt[iProhod].X - 1), Pt[iProhod].Y, CurrentColor);
                    }
                if (Pt[iProhod].Y + 1 < pictureBox1.Height)
                    if (tempDraw.GetPixel(Pt[iProhod].X, Pt[iProhod].Y + 1) == c)
                    {
                        i++;
                        iS++;
                        Pt[i] = new Point(Pt[iProhod].X, Pt[iProhod].Y + 1);
                        tempDraw.SetPixel(Pt[iProhod].X, Pt[iProhod].Y + 1, CurrentColor);
                    }
                if (Pt[iProhod].Y - 1 >= 0)
                    if (tempDraw.GetPixel(Pt[iProhod].X, Pt[iProhod].Y - 1) == c)
                    {
                        i++;
                        iS++;
                        Pt[i] = new Point(Pt[iProhod].X, Pt[iProhod].Y - 1);
                        tempDraw.SetPixel(Pt[iProhod].X, Pt[iProhod].Y - 1, CurrentColor);
                    }
                iS--;
                iProhod++;

            }
            pictureBox1.Image = tempDraw;
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
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
            selected = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            selectedTool = 0;
            selected = false;
            textBox1.TabStop = false;
        }


        private void label4_Click(object sender, EventArgs e)
        {
            selectedTool = 1;
            selected = false;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            sizePen = 1;   // размер карандаша
        }

        private void label6_Click(object sender, EventArgs e)
        {
            sizePen = 5;   // размер карандаша
        }

        private void label7_Click(object sender, EventArgs e)
        {
            sizePen = 10;   // размер карандаша
        }

        private void label8_Click(object sender, EventArgs e)
        {
            //textBox1.Enabled = true;
            createProject();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            DialogResult dr = open_dialog.ShowDialog();
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
        }

        private void label10_Click(object sender, EventArgs e)
        {
            
        }

        private void label11_Click(object sender, EventArgs e)
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

        private void label13_Click(object sender, EventArgs e)
        {
            createKadr();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            SpeedDouble = Convert.ToDouble(textBox1.Text);
            dr = (int)(1000/ SpeedDouble);
            timer1.Interval = dr;
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

        private void label17_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        bool press = false; Point n = new Point(0, 0);
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            press = true; n = new Point(e.X, e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (press)
            {
                Point P = this.PointToScreen(new Point(e.X, e.Y));
                this.Location = new Point(P.X - n.X, P.Y - n.Y);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            press = false;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
        }

        private void label14_Click(object sender, EventArgs e)
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

        private void label12_Click(object sender, EventArgs e)
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

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (selected == true)
            {
                X1Zaliv = e.X;
                Y1Zaliv = e.Y;
                if (tempDraw.GetPixel(X1Zaliv, Y1Zaliv) != CurrentColor)
                {
                    zalivka();
                }
            }
        }
    }

    
}
