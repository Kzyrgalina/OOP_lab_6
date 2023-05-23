using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace OOP678
{
    class Shape //базовый класс с виртуальными методами
    {
        protected Color clr;

        virtual public void draw(PictureBox sender, Bitmap bmp, Graphics g) { } //метод для риосвания объекта

        virtual public bool isChecked(MouseEventArgs e) { return false; }//проверка на выделенность

        virtual public void move(PictureBox sender, int _x, int _y ){ }//движение объекта

        virtual public void resize(PictureBox sender, int _d) { }//изменение размера

        virtual public void changeColor(Color _clr) { clr = _clr; }//изменение цвета

        virtual public bool getF() { return false; } //методы для выделения

        virtual public void slect1() { }//метод1 для установки значения

        virtual public void slect2() { }//метод1 для установки значения

    }
    class Secture : Shape //класс отрезок, унаследованный от базового класса 
    {
        private Point a, b; //точки
        private bool f;
        public float alfa; //коэф наклона
        public Secture(int _x1, int _y1, int _x2, int _y2, Color _clr) 
        {
            a.X = _x1;
            a.Y = _y1;
            b.X = _x2;
            b.Y = _y2;
            f = true;
            clr = _clr;
            if (b.X - a.X != 0)
            {
                alfa = (b.Y - a.Y) / (b.X - a.X); //коэфф наклона
            }
            else
            {
                alfa = 1000000;
            }

        }
        
   
        override public void draw(PictureBox sender, Bitmap bmp, Graphics g) //метод для рисования на pictureBox
        {

            Pen pen = new Pen(clr);
            if (f == true) //проверка на то, "выделен" ли объект или нет
            {
                pen.Width = 2; //выделение объекта толтсой линией
            }
            g.DrawLine(pen, a, b);
            sender.Image = bmp;
        }
        private bool locationCheck(PictureBox sender) //проверка на нахождение внутри области рисования
        {
            if ((a.X >= sender.Location.X + sender.Size.Width) || a.X <= sender.Location.X)
            {
                return false;
            }
            else if ((a.Y >= sender.Location.Y + sender.Size.Height) || a.Y <= sender.Location.Y)
            {
                return false;
            }
            else if ((b.X >= sender.Location.X + sender.Size.Width) || b.X <= sender.Location.X)
            {
                return false;
            }
            else if ((b.Y >= sender.Location.Y + sender.Size.Height) || b.Y <= sender.Location.Y)
            {
                return false;
            }
            return true;
        }
        public override void move(PictureBox sender, int _x, int _y) //метод для передвижения
        {
            a.X = a.X + _x;
            a.Y = a.Y + _y;
            b.X = b.X + _x;
            b.Y = b.Y + _y;
            if (locationCheck(sender) == false) // область рисования
            {
                a.X = a.X - _x;
                a.Y = a.Y - _y;
                b.X = b.X - _x;
                b.Y = b.Y - _y;
            }
        }
        public override void resize(PictureBox sender, int _d)//метод для изменения размера объекта
        {
            if (alfa != 0)
            {
                a.X = a.X + (int)alfa * _d;
                a.Y = a.Y - (int)alfa * _d;
                b.X = b.X - (int)alfa * _d;
                b.Y = b.Y + (int)alfa * _d;
                if (locationCheck(sender) == false)
                {
                    a.X = a.X - (int)alfa * _d;
                    a.Y = a.Y + (int)alfa * _d;
                    b.X = b.X + (int)alfa * _d;
                    b.Y = b.Y - (int)alfa * _d;
                }
            }
        }

        override public bool isChecked(MouseEventArgs e) //проверка на то, нажат ли объект мышкой
        {

            if (a.X > b.X)
            {
                if (a.Y < b.Y)
                {
                    if (e.X < a.X && e.X > b.X && e.Y > a.Y && e.Y < b.Y)
                    {
                        return true;
                    }
                }
                else if (a.Y > b.Y)
                {
                    if (e.X < a.X && e.X > b.X && e.Y < a.Y && e.Y > b.Y)
                    {
                        return true;
                    }
                }
            }
            else if (a.X < b.X)
            {
                if (a.Y < b.Y)
                {
                    if (e.X > a.X && e.X < b.X && e.Y > a.Y && e.Y < b.Y)
                    {
                        return true;
                    }
                }
                else if (a.Y > b.Y)
                {
                    if (e.X > a.X && e.X < b.X && e.Y < a.Y && e.Y > b.Y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        override public bool getF() //получение значения " выделенный/не выделенный" у объекта
        {
            return f;
        }
        override public void slect1() //изменение значения f на true
        {
            f = true;
        }
        override public void slect2() //изменение значения f на false
        {
            f = false;
        }

    }
    class CSquare : Shape //класс квадрат, унаследованный от базового класса
    {
        private int x, y, l;
        private bool f;

        public CSquare(int _x, int _y, int _l, Color _clr)
        {
            x = _x;
            y = _y;
            l = _l;
            f = true;
            clr = _clr;
        }

        public int getX() { return x; }
        public int getY() { return y; }
        public int getL() { return l; }
        
        override public void draw(PictureBox sender, Bitmap bmp, Graphics g) //метод для рисования на pictureBox
        {
            Rectangle rect = new Rectangle(x - l / 2, y - l / 2, l, l);
            Pen pen = new Pen(clr);
            if (f == true) //проверка на то, "выделен" ли объект или нет
            {
                pen.Width = 2; //выделение объекта толтсой линией
            }
            g.DrawRectangle(pen, rect);
            sender.Image = bmp;
        }
        private bool locationCheck(PictureBox sender)
        {
            if ((x + l / 2 >= sender.Location.X + sender.Size.Width) || x - l / 2 <= sender.Location.X)
            {
                return false;
            }
            else if ((y + l / 2 >= sender.Location.Y + sender.Size.Height) || y - l / 2 <= sender.Location.Y)
            {
                return false;
            }
            return true;
        }
        public override void move(PictureBox sender, int _x, int _y)//движение объекта
        {
            x = x + _x;
            y = y + _y;
            if (locationCheck(sender) == false)
            {
                x = x - _x;
                y = y - _y;
            }
        }
        public override void resize(PictureBox sender, int _d)//изменение размера объекта
        {
            l = l + _d / 2;
            if (locationCheck(sender) == false)
            {
                l = l - _d / 2;
            }
        }


        override public bool isChecked(MouseEventArgs e) //проверка на то, нажат ли объект мышкой
        {
            if (e.X >= x - l / 2 && e.Y >= y - l / 2 && e.X <= x + l / 2 && e.Y <= y + l / 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        override public bool getF() //получение значения " выделенный/не выделенный" у объекта
        {
            return f;
        }
        override public void slect1() //изменение значения f на true
        {
            f = true;
        }
        override public void slect2() //изменение значения f на false
        {
            f = false;
        }
    }
    class CTriangle : Shape // Теугольники
    {
        private Point a, b, c;
        private bool f;

        public CTriangle(int x1, int x2, int y1, int y2, int z1, int z2, Color _clr)
        {
            a = new Point(x1, x2);
            b = new Point(y1, y2);
            c = new Point(z1, z2);
            f = true;
            clr = _clr;
        }
        

        override public void draw(PictureBox sender, Bitmap bmp, Graphics g) //метод для рисования на pictureBox
        {
            Pen pen = new Pen(clr);
            if (f == true) //проверка на то, "выделен" ли объект или нет
            {
                pen.Width = 2; //выделение объекта толтсой линией
            }
            Point[] CurvePoints = { a, b, c };
            g.DrawPolygon(pen, CurvePoints);
            sender.Image = bmp;
        }
        private bool locationCheck(PictureBox sender) //чтобы не вышло за границы
        {
            if ((b.X >= sender.Location.X + sender.Size.Width) || c.X <= sender.Location.X)
            {
                return false;
            }
            else if ((b.Y >= sender.Location.Y + sender.Size.Height) || a.Y <= sender.Location.Y)
            {
                return false;
            }
            return true;
        }
        public override void move(PictureBox sender, int _x, int _y)//движение объекта
        {
            a.X += _x;
            a.Y += _y;
            b.X += _x;
            b.Y += _y;
            c.X += _x;
            c.Y += _y;
            if (locationCheck(sender) == false)
            {
                a.X -= _x;
                a.Y -= _y;
                b.X -= _x;
                b.Y -= _y;
                c.X -= _x;
                c.Y -= _y;
            }
        }
        public override void resize(PictureBox sender, int _d)//изменение размера объекта
        {
            a.Y = a.Y - _d / 2;
            b.X = b.X + _d / 2;
            b.Y = b.Y + _d / 2;
            c.X = c.X - _d / 2;
            c.Y = c.Y + _d / 2;
            if (locationCheck(sender) == false)
            {
                a.Y = a.Y + _d / 2;
                b.X = b.X - _d / 2;
                b.Y = b.Y - _d / 2;
                c.X = c.X + _d / 2;
                c.Y = c.Y - _d / 2;
            }
        }
        override public bool isChecked(MouseEventArgs e) //проверка на то, нажат ли объект мышкой
        {
            int p1 = (a.X - e.X) * (b.Y - a.Y) - (b.X - a.X) * (a.Y - e.Y);
            int p2 = (b.X - e.X) * (c.Y - b.Y) - (c.X - b.X) * (b.Y - e.Y);
            int p3 = (c.X - e.X) * (a.Y - c.Y) - (a.X - c.X) * (c.Y - e.Y);
            if ((p1 >= 0 && p2 >= 0 && p3 >= 0) || (p1 <= 0 && p2 <= 0 && p3 <= 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        override public bool getF() //получение значения " выделенный/не выделенный" у объекта
        {
            return f;
        }
        override public void slect1() //изменение значения f на true
        {
            f = true;
        }
        override public void slect2() //изменение значения f на false
        {
            f = false;
        }
    }
    class CCircle : Shape //класс окружности, унаследованный от базового класса
    {
        private int x, y, r; //координаты и радиус
        private bool f; //булевая переменная, показывающая, "выделен" объект или нет
        public CCircle(int _x, int _y, int _r, Color _clr) //конструктор с параметрами
        {
            x = _x;
            y = _y;
            r = _r;
            f = true;
            clr = _clr;
        }
        
        public int getX() { return x; }
        public int getY() { return y; }
        public int getR() { return r; }
       
        override public void draw(PictureBox sender, Bitmap bmp, Graphics g) //метод для рисования на pictureBox
        {
            Rectangle rect = new Rectangle(x - r, y - r, r * 2, r * 2);
            Pen pen = new Pen(clr);
            if (f == true) //проверка на то, "выделен" ли объект или нет
            {
                pen.Width = 2; //выделение объекта толтсой линией
            }
            g.DrawEllipse(pen, rect);
            sender.Image = bmp;
        }
        private bool locationCheck(PictureBox sender)//проверка объекта на нахождение в области риосования
        {
            if ((x + r >= sender.Location.X + sender.Size.Width) || x - r <= sender.Location.X)
            {
                return false;
            }
            else if ((y + r >= sender.Location.Y + sender.Size.Height) || y - r <= sender.Location.Y)
            {
                return false;
            }
            return true;
        }
        public override void move(PictureBox sender, int _x, int _y)//движение объекта
        {
            x = x + _x;
            y = y + _y;
            if (locationCheck(sender) == false)
            {
                x = x - _x;
                y = y - _y;
            }
        }
        public override void resize(PictureBox sender, int _d)//изменение размера объекта
        {
            r = r + _d / 2;
            if (locationCheck(sender) == false)
            {
                r = r - _d / 2;
            }
        }
        override public bool isChecked(MouseEventArgs e) //проверка на то, нажат ли объект мышкой
        {
            if (((e.X - x) * (e.X - x) + (e.Y - y) * (e.Y - y)) <= (r * r))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        override public bool getF() //получение значения " выделенный/не выделенный" у объекта
        {
            return f;
        }
        override public void slect1() //изменение значения f на true
        {
            f = true;
        }
        override public void slect2() //изменение значения f на false
        {
            f = false;
        }
    }
    
    class Storage //контейнер
    {
        private int _maxcount;
        private int _count;
        public Shape[] _storage;
        public System.EventHandler observers;
        public Storage(int maxcount)//конструктор с параметрами
        {
            _maxcount = maxcount; _count = 0;
            _storage = new Shape[_maxcount];
            for (int i = 0; i < _maxcount; i++)
                _storage[i] = null;
        }

      
        
        public void addObj(Shape obj) //добавление объекта
        {
            if (_count >= _maxcount)
            {
                Array.Resize(ref _storage, _count + 1);
                _storage[_count] = obj;
                _storage[_count].slect1();
                for (int i = 0; i < _count - 1; i++)
                {
                    _storage[i].slect2();
                }
                _count++;
                _maxcount++;
            }
            else if (_count == 0)
            {
                _count++;
                _storage[_count - 1] = obj;
                _storage[_count - 1].slect1();
            }
            else
            {
                _count++;
                _storage[_count - 1] = obj;
                for (int i = 0; i < _count - 1; i++)
                {
                    _storage[i].slect2();
                }
            }
        }
        public void deleteObj(int index)//удаление объекта
        {
            _storage[index] = null;
            for (int i = index + 1; i < _count; i++)//идем с места удаления объекта к концу массива и перемещаем объекты 
            {
                _storage[i - 1] = _storage[i];
            }
            _storage[_count - 1] = null;
            _count--;
        }
        public void drawObj(PictureBox sender, int index, Bitmap bmp, Graphics g) //вызов draw у объекта по индексу
        {
            if (_storage[index] != null)
            {
                _storage[index].draw(sender, bmp, g);
            }
        }
        public void moveObj(PictureBox sender, int _x, int _y)//движение объектов
        {
            for (int i = _count - 1; i >= 0; i--)
            {
                if (_storage[i] != null) 
                {
                    if (_storage[i].getF() == true)
                    {
                        _storage[i].move(sender, _x, _y);
                    }

                }
            }
        }
   
        public void resizeObj(PictureBox sender, int _d)//изменение размера
        {
            for (int i = _count - 1; i >= 0; i--)
            {
                if (_storage[i] != null)
                {
                    if (_storage[i].getF() == true)
                    {
                        _storage[i].resize(sender, _d);
                    }
                }
            }
        }
        public void changeObjColor(Color _clr)//изменение цвета
        {
            for (int i = _count - 1; i >= 0; i--)
            {
                if (_storage[i] != null)
                {
                    if (_storage[i].getF() == true)
                    {
                        _storage[i].changeColor(_clr);
                    }
                }
            }
        }

        public int getCount() //получение количества объектов в хранилище (на форме)
        {
            return _count;
        }
        public void allObjFalse()  //все объекты перестают быть выделенными
        {
            for (int i = _count - 1; i >= 0; i--)
            {
                if (_storage[i] != null)
                {
                    if (_storage[i].getF() == true)
                    {
                        _storage[i].slect2();
                    }
                }

            }
        }
        public void allObjTrue()  //все объекты перестают быть выделенными
        {
            for (int i = _count - 1; i >= 0; i--)
            {
                if (_storage[i] != null)
                {
                    if (_storage[i].getF() == false)
                    {
                        _storage[i].slect1();
                    }
                }

            }
        }
        public void deleteWhenDel() //удаление выделенных объектов при нажатии кнопки Delete
        {
            for (int i = _count - 1; i >= 0; i--)
            {
                if (_storage[i] != null)
                {
                    if (_storage[i].getF() == true)
                    {
                        deleteObj(i);
                    }
                }
            }
        }
        public bool checkInfo1(MouseEventArgs e) //проверка того, нажат ли объект на форме, если клавиша Ctrl не нажата
        {
            for (int i = 0; i < getCount(); i++)
            {
                if (_storage[i] != null)
                {
                    if (_storage[i].isChecked(e) == true)
                    {
                        allObjFalse();
                        _storage[i].slect1();
                        return true;
                    }
                }
            }
            return false;
        }
        public bool checkInfo2(MouseEventArgs e) //проверка того, нажат ли объект на форме, если клавиша Ctrl нажата
        {
            for (int i = 0; i < getCount(); i++)
            {
                if (_storage[i] != null)
                {
                    if (_storage[i].isChecked(e) == true)
                    {
                        _storage[i].slect1();
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
