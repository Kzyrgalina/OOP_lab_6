using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP678
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.MouseWheel += new MouseEventHandler(pictureBox1_MouseWheel);
        }
        Storage storage = new Storage(100); //создание хранилища
        Bitmap bmp = new Bitmap(1800, 800); //создание места для рисования
        Point temp = new Point(0, 0);
        public Bitmap Image { get; internal set; }
        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            Graphics g = Graphics.FromImage(bmp);
            if (e.Delta > 0) //увеличение объекта
            {
                storage.resizeObj(pictureBox1, 5);
                g.Clear(Color.WhiteSmoke);
                Refresh();
            }
            else if (e.Delta < 0) //уменьшение объекта
            {
                storage.resizeObj(pictureBox1, -5);
                g.Clear(Color.WhiteSmoke);
                Refresh();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
          
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Graphics g = Graphics.FromImage(bmp);
            RadioButton rb = rbColorBlack; //выбор цвета создаваемой компоненты
            if (rbColorBlack.Checked == true)
            {
                rb = rbColorBlack;
            }
            else if (rbColorRed.Checked == true)
            {
                rb = rbColorRed;
            }
            else if (rbColorYellow.Checked == true)
            {
                rb = rbColorYellow;
            }
            else if (rbColorGreen.Checked == true)
            {
                rb = rbColorGreen;
            }
            else if (rbColorBlue.Checked == true)
            {
                rb = rbColorBlue;
            }
            else if (rbColorViolet.Checked == true)
            {
                rb = rbColorViolet;
            }
            if (Control.ModifierKeys == Keys.Control) //если зажат Ctrl
            {
                if (storage.checkInfo2(e) == false)
                {
                    if (radioButton1.Checked == true)
                    {
                        storage.addObj(new CCircle(e.X, e.Y, 20, rb.ForeColor));
                        g.Clear(Color.WhiteSmoke);
                        Refresh();
                    }
                    else if (radioButton2.Checked == true)
                    {
                        storage.addObj(new CSquare(e.X, e.Y, 40, rb.ForeColor));
                        g.Clear(Color.WhiteSmoke);
                        Refresh();
                    }
                    else if (radioButton3.Checked == true)
                    {
                        storage.addObj(new CTriangle(e.X, e.Y - 30, e.X + 20, e.Y + 10, e.X - 20, e.Y + 10, rb.ForeColor));
                        g.Clear(Color.WhiteSmoke);
                        Refresh();
                    }
                    else if (radioButton4.Checked == true)
                    {
                        if (temp.X == 0)
                        {
                            temp = new Point(e.X, e.Y);
                        }
                        else
                        {
                            storage.addObj(new Secture(temp.X, temp.Y, e.X, e.Y, rb.ForeColor));
                            g.Clear(Color.WhiteSmoke);
                            temp.X = 0;
                            temp.Y = 0;
                            Refresh();
                        }

                    }
                    lb1.Text = storage.getCount().ToString();
                }
                else
                {
                    lb1.Text = storage.getCount().ToString();
                    g.Clear(Color.WhiteSmoke);
                    Refresh();
                }

            }
            else
            {
                if (storage.checkInfo1(e) == false) //если нажали по форме
                {
                    if (radioButton1.Checked == true)
                    {
                        storage.addObj(new CCircle(e.X, e.Y, 20, rb.ForeColor));
                        g.Clear(Color.WhiteSmoke);
                        Refresh();
                    }
                    else if (radioButton2.Checked == true)
                    {
                        storage.addObj(new CSquare(e.X, e.Y, 40, rb.ForeColor));
                        g.Clear(Color.WhiteSmoke);
                        Refresh();
                    }
                    else if (radioButton3.Checked == true)
                    {
                        storage.addObj(new CTriangle(e.X, e.Y - 30, e.X + 20, e.Y + 10, e.X - 20, e.Y + 10, rb.ForeColor));
                        g.Clear(Color.WhiteSmoke);
                        Refresh();
                    }
                    else if (radioButton4.Checked == true)
                    {
                        if (temp.X == 0)
                        {
                            temp = new Point(e.X, e.Y);
                        }
                        else
                        {
                            storage.addObj(new Secture(temp.X, temp.Y, e.X, e.Y, rb.ForeColor));
                            g.Clear(Color.WhiteSmoke);
                            temp.X = 0;
                            temp.Y = 0;
                            Refresh();
                        }

                    }
                    lb1.Text = storage.getCount().ToString();
                    
                }
                else //если мышью нажали на объект на форме
                {
                    lb1.Text = storage.getCount().ToString();
                    g.Clear(Color.WhiteSmoke);
                    Refresh();
                }
            }
            Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = Graphics.FromImage(bmp);
            for (int i = 0; i < (storage.getCount()); i++)
            {
                storage.drawObj(pictureBox1, i, bmp, g);
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Graphics g = Graphics.FromImage(bmp);
            if (e.KeyData == Keys.A)//движение объекта влево
            {
                storage.moveObj(pictureBox1, -10, 0);
                g.Clear(Color.WhiteSmoke);
                Refresh();
            }
            if (e.KeyData == Keys.D)//движение объекта вправо
            {
                storage.moveObj(pictureBox1, 10, 0);
                g.Clear(Color.WhiteSmoke);
                Refresh();
            }
            if (e.KeyData == Keys.W)//движение объекта вверх
            {
                storage.moveObj(pictureBox1, 0, -10);
                g.Clear(Color.WhiteSmoke);
                Refresh();
            }
            if (e.KeyData == Keys.S)//движение объекта вниз
            {
                storage.moveObj(pictureBox1, 0, 10);
                g.Clear(Color.WhiteSmoke);
                Refresh();
            }
            if (e.KeyData == Keys.Delete)
            {
                storage.deleteWhenDel();
                g.Clear(Color.WhiteSmoke);
               
                Refresh();
            }
            lb1.Text = storage.getCount().ToString();
            Refresh();
        }

        private void rbColorBlack_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            storage.changeObjColor(rb.ForeColor);
        }

        private void rbColorRed_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            storage.changeObjColor(rb.ForeColor);
        }

        private void rbColorYellow_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            storage.changeObjColor(rb.ForeColor);
        }

        private void rbColorGreen_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            storage.changeObjColor(rb.ForeColor);
        }

        private void rbColorBlue_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            storage.changeObjColor(rb.ForeColor);
        }

        private void rbColorViolet_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            storage.changeObjColor(rb.ForeColor);
        }
       

        private void btnClear_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(bmp);
            storage.allObjTrue();
            storage.deleteWhenDel();
            g.Clear(Color.WhiteSmoke);
            Refresh();
        }
      
    }
}
