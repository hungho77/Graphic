using SharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Graph
{


    public partial class Form1 : Form
    {
        private Point pStart;
        private Point pEnd;
        private Color colorUserColor;
        private int check = -1;
        private int size;
        private bool checkClickDown = false;
        private Bitmap _bm;
        private struct Sgraph
        {
            public Point pStart;
            public Point pEnd;
            public int Shape;
        }
        List<Sgraph> Lgraph = new List<Sgraph>();
        private Sgraph Stemp;
        private List<Point> Ctrl_Ponits = new List<Point>();

        static void Repaint(Point pStart, Point pEnd, int Shape, ref OpenGL gl)
        {
            double Rx, Ry;
            switch (Shape)
            {
                case 0: //vẽ đoạn thẳng   
                    gl.Begin(OpenGL.GL_LINES); // chọn chế độ vẽ đường thẳng
                    gl.Vertex(pStart.X, gl.RenderContextProvider.Height - pStart.Y);
                    gl.Vertex(pEnd.X, gl.RenderContextProvider.Height - pEnd.Y);
                    gl.End();
                    gl.Flush(); // Thực hiện lệnh vẽ ngay lập tức thay vì đợi sau 1 khoảng thời gian 
                    break;

                case 1: //vẽ hình tròn
                    double thetar;
                    Rx = Math.Sqrt(Math.Pow(pStart.X - pEnd.X, 2) + Math.Pow(pStart.Y - pEnd.Y, 2));
                    gl.Begin(OpenGL.GL_LINE_LOOP);
                    for (int i = 0; i < 360; i++)
                    {
                        thetar = i * Math.PI / 180;
                        gl.Vertex(pStart.X + Rx * Math.Cos(thetar), gl.RenderContextProvider.Height - pStart.Y + Rx * Math.Sin(thetar));
                    }
                    gl.End();
                    gl.Flush();// Thực hiện lệnh vẽ ngay lập tức thay vì đợi sau 1 khoảng thời gian
                    break;

                case 2: //vẽ hình chữ nhật
                    gl.Begin(OpenGL.GL_LINE_LOOP);
                    gl.Vertex(pStart.X, gl.RenderContextProvider.Height - pStart.Y);
                    gl.Vertex(pStart.X, gl.RenderContextProvider.Height - pEnd.Y);
                    gl.Vertex(pEnd.X, gl.RenderContextProvider.Height - pEnd.Y);
                    gl.Vertex(pEnd.X, gl.RenderContextProvider.Height - pStart.Y);
                    gl.End();
                    gl.Flush(); // Thực hiện lệnh vẽ ngay lập tức thay vì đợi sau 1 khoảng thời gian
                    break;

                case 3: //vẽ elip
                    Rx = Math.Sqrt(Math.Pow(pStart.X - pEnd.X, 2));
                    Ry = Math.Sqrt(Math.Pow(pStart.Y - pEnd.Y, 2));
                    double x, y;

                    gl.Begin(OpenGL.GL_LINE_LOOP);
                    for (int i = 0; i < 360; i++)
                    {
                        thetar = i * Math.PI / 180;
                        x = pStart.X + Rx * Math.Cos(thetar);
                        y = gl.RenderContextProvider.Height - pStart.Y + Ry * Math.Sin(thetar);
                        gl.Vertex(x, y);


                    }
                    gl.End();
                    gl.Flush();// Thực hiện lệnh vẽ ngay lập tức thay vì đợi sau 1 khoảng thời gian
                    break;

                case 4: // vẽ tam giác
                    Rx = Math.Sqrt(Math.Pow(pStart.X - pEnd.X, 2) + Math.Pow(pStart.Y - pEnd.Y, 2));
                    gl.Begin(OpenGL.GL_LINE_LOOP);
                    for (int i = 0; i < 360; i++)
                    {
                        if (i % 120 == 0)
                        {
                            thetar = i * Math.PI / 180;
                            gl.Vertex(pStart.X + Rx * Math.Cos(thetar + Math.PI / 2), gl.RenderContextProvider.Height - pStart.Y + Rx * Math.Sin(thetar + Math.PI / 2));
                        }
                    }
                    gl.End();
                    gl.Flush(); // Thực hiện lệnh vẽ ngay lập tức thay vì đợi sau 1 khoảng thời gian
                    break;

                case 5: // vẽ ngũ giác
                    Rx = Math.Sqrt(Math.Pow(pStart.X - pEnd.X, 2) + Math.Pow(pStart.Y - pEnd.Y, 2));
                    gl.Begin(OpenGL.GL_LINE_LOOP);
                    for (int i = 0; i < 360; i += 72)
                    {
                        //if (i % 72 == 0)
                        {
                            thetar = i * Math.PI / 180;
                            gl.Vertex(pStart.X + Rx * Math.Cos(thetar + Math.PI / 2), gl.RenderContextProvider.Height - pStart.Y + Rx * Math.Sin(thetar + Math.PI / 2));
                        }
                    }
                    gl.End();
                    gl.Flush(); // Thực hiện lệnh vẽ ngay lập tức thay vì đợi sau 1 khoảng thời gian
                    break;

                case 6: // vẽ lục giác
                    Rx = Math.Sqrt(Math.Pow(pStart.X - pEnd.X, 2) + Math.Pow(pStart.Y - pEnd.Y, 2));
                    gl.Begin(OpenGL.GL_LINE_LOOP);
                    for (int i = 1; i < 360; i++)
                    {
                        if (i % 30 == 0 && i % 60 != 0)
                        {
                            thetar = i * Math.PI / 180;
                            gl.Vertex(pStart.X + Rx * Math.Cos(thetar + Math.PI / 2), gl.RenderContextProvider.Height - pStart.Y + Rx * Math.Sin(thetar + Math.PI / 2));
                        }
                    }
                    gl.End();
                    gl.Flush(); // Thực hiện lệnh vẽ ngay lập tức thay vì đợi sau 1 khoảng thời gian
                    break;
            }
        }

        private void Show_Control_Point(Point pStart, Point pEnd, int Shape, ref OpenGL gl)
        {
            Point pTemp = new Point();
            gl.Color(Color.Yellow.R / 255.0, Color.Yellow.G / 255.0, Color.Yellow.B / 255.0, 0);
            gl.PointSize(5);
            switch (Shape)
            {
                case 0: // đoạn thẳng
                    gl.Begin(OpenGL.GL_POINTS);
                    gl.Vertex(pStart.X, gl.RenderContextProvider.Height - pStart.Y);
                    gl.Vertex(pEnd.X, gl.RenderContextProvider.Height - pEnd.Y);
                    gl.End();
                    gl.Flush(); // Thực hiện lệnh vẽ ngay lập tức thay vì đợi sau 1 khoảng thời gian
                    break;
                case 1: // hình tròn
                    Ctrl_Ponits.Clear();
                    double thetar;
                    double R = Math.Sqrt(Math.Pow(pStart.X - pEnd.X, 2) + Math.Pow(pStart.Y - pEnd.Y, 2));
                    for (int i = 0; i < 360; i += 90)
                    {
                        thetar = i * Math.PI / 180;
                        pTemp.X = (int)(pStart.X + R * Math.Cos(thetar));
                        pTemp.Y = (int)(pStart.Y + R * Math.Sin(thetar));
                        Ctrl_Ponits.Add(pTemp);
                    }
                    pTemp.X = Ctrl_Ponits[0].X;
                    pTemp.Y = Ctrl_Ponits[1].Y;
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.X = Ctrl_Ponits[2].X;
                    pTemp.Y = Ctrl_Ponits[1].Y;
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.X = Ctrl_Ponits[2].X;
                    pTemp.Y = Ctrl_Ponits[3].Y;
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.X = Ctrl_Ponits[0].X;
                    pTemp.Y = Ctrl_Ponits[3].Y;
                    Ctrl_Ponits.Add(pTemp);
                    gl.Begin(OpenGL.GL_POINTS);
                    for (int i = 0; i < Ctrl_Ponits.Count(); i++)
                    {
                        gl.Vertex(Ctrl_Ponits[i].X, gl.RenderContextProvider.Height - Ctrl_Ponits[i].Y);
                    }
                    gl.End();
                    gl.Flush(); // Thực hiện lệnh vẽ ngay lập tức thay vì đợi sau 1 khoảng thời gian
                    break;

                case 2: // hình chữ nhật
                    Ctrl_Ponits.Clear();
                    Ctrl_Ponits.Add(pStart);
                    Ctrl_Ponits.Add(pEnd);
                    pTemp.X = pStart.X;
                    pTemp.Y = pEnd.Y;
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.Y = (pStart.Y + pEnd.Y) / 2;
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.X = pEnd.X;
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.Y = pStart.Y;
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.X = (pStart.X + pEnd.X) / 2;
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.Y = pEnd.Y;
                    Ctrl_Ponits.Add(pTemp);
                    gl.Begin(OpenGL.GL_POINTS);
                    for (int i = 0; i < Ctrl_Ponits.Count(); i++)
                    {
                        gl.Vertex(Ctrl_Ponits[i].X, gl.RenderContextProvider.Height - Ctrl_Ponits[i].Y);
                    }
                    gl.End();
                    gl.Flush(); // Thực hiện lệnh vẽ ngay lập tức thay vì đợi sau 1 khoảng thời gian
                    break;

                case 3:
                    Ctrl_Ponits.Clear();
                    pTemp.X = 2 * pStart.X - pEnd.X;
                    pTemp.Y = 2 * pStart.Y - pEnd.Y;
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.X = pStart.X;
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.X = pEnd.X;
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.Y = pStart.Y;
                    Ctrl_Ponits.Add(pTemp);
                    Ctrl_Ponits.Add(pEnd);
                    pTemp.X = 2 * pStart.X - pTemp.X;
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.Y = pEnd.Y;
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.X = pStart.X;
                    Ctrl_Ponits.Add(pTemp);
                    gl.Begin(OpenGL.GL_POINTS);
                    for (int i = 0; i < Ctrl_Ponits.Count(); i++)
                    {
                        gl.Vertex(Ctrl_Ponits[i].X, gl.RenderContextProvider.Height - Ctrl_Ponits[i].Y);
                    }
                    gl.End();
                    gl.Flush(); // Thực hiện lệnh vẽ ngay lập tức thay vì đợi sau 1 khoảng thời gian
                    break;

                case 4:
                    Ctrl_Ponits.Clear();

                    R = Math.Sqrt(Math.Pow(pStart.X - pEnd.X, 2) + Math.Pow(pStart.Y - pEnd.Y, 2));
                    for (int i = 0; i < 360; i++)
                    {
                        if (i % 120 == 0)
                        {
                            thetar = (i * Math.PI + 189) / 180;
                            pTemp.X = (int)(pStart.X + R * Math.Cos(thetar + Math.PI / 2));
                            pTemp.Y = (int)(pStart.Y + R * Math.Sin(thetar + Math.PI / 2));
                            Ctrl_Ponits.Add(pTemp);
                        }
                    }

                    pTemp.X = Ctrl_Ponits[2].X;
                    pTemp.Y = Ctrl_Ponits[1].Y;
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.X = Ctrl_Ponits[0].X;
                    pTemp.Y = Ctrl_Ponits[3].Y;
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.X = Ctrl_Ponits[1].X;
                    pTemp.Y = Ctrl_Ponits[0].Y;
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.X = Ctrl_Ponits[2].X;
                    pTemp.Y = (Ctrl_Ponits[3].Y + Ctrl_Ponits[2].Y) / 2;
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.X = Ctrl_Ponits[0].X;
                    Ctrl_Ponits.Add(pTemp);
                    gl.Begin(OpenGL.GL_POINTS);
                    for (int i = 0; i < Ctrl_Ponits.Count(); i++)
                    {
                        gl.Vertex(Ctrl_Ponits[i].X, gl.RenderContextProvider.Height - Ctrl_Ponits[i].Y);
                    }
                    gl.End();
                    gl.Flush(); // Thực hiện lệnh vẽ ngay lập tức thay vì đợi sau 1 khoảng thời gian
                    break;

                case 5:
                    Ctrl_Ponits.Clear();
                    R = Math.Sqrt(Math.Pow(pStart.X - pEnd.X, 2) + Math.Pow(pStart.Y - pEnd.Y, 2));       
                    double R1 = R * Math.Cos((36 * Math.PI)/ 180);
                    pTemp.X = pStart.X;
                    pTemp.Y = (int)(pStart.Y - R);
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.X = pStart.X;
                    pTemp.Y = (int)(pStart.Y + R1);
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.X = (int)(pStart.X + R);
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.X = (int)(pStart.X - R);
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.Y = (int)(pStart.Y - R);
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.X = (int)(pStart.X + R);
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.Y = (int)((pTemp.Y + Ctrl_Ponits[2].Y) / 2);
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.X = (int)(pStart.X - R);
                    Ctrl_Ponits.Add(pTemp);
                    gl.Begin(OpenGL.GL_POINTS);
                    for (int i = 0; i < Ctrl_Ponits.Count(); i++)
                    {
                        gl.Vertex(Ctrl_Ponits[i].X, gl.RenderContextProvider.Height - Ctrl_Ponits[i].Y);                  
                    }
                    gl.End();
                    gl.Flush(); // Thực hiện lệnh vẽ ngay lập tức thay vì đợi sau 1 khoảng thời gian
                    break;

                case 6:
                    Ctrl_Ponits.Clear();
                    R = Math.Sqrt(Math.Pow(pStart.X - pEnd.X, 2) + Math.Pow(pStart.Y - pEnd.Y, 2));
                    R1 = R * Math.Cos((30 * Math.PI) / 180);
                    pTemp.X = pStart.X;
                    pTemp.Y = (int)(pStart.Y - R1);
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.Y = (int)(pStart.Y + R1);
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.Y = pStart.Y;
                    pTemp.X = (int)(pStart.X - R);
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.X = (int)(pStart.X + R);
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.X = Ctrl_Ponits[2].X;
                    pTemp.Y = Ctrl_Ponits[0].Y;
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.Y = Ctrl_Ponits[1].Y;
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.X = Ctrl_Ponits[3].X;
                    Ctrl_Ponits.Add(pTemp);
                    pTemp.Y = Ctrl_Ponits[0].Y;
                    Ctrl_Ponits.Add(pTemp);
                    gl.Begin(OpenGL.GL_POINTS);
                    for (int i = 0; i < Ctrl_Ponits.Count(); i++)
                    {
                        gl.Vertex(Ctrl_Ponits[i].X, gl.RenderContextProvider.Height - Ctrl_Ponits[i].Y);
                    }
                    gl.End();
                    gl.Flush(); // Thực hiện lệnh vẽ ngay lập tức thay vì đợi sau 1 khoảng thời gian
                    break;
            }

            



        }

        public Form1()
        {
            InitializeComponent();
            colorUserColor = Color.White;  //  Gán giá trị màu ban đầu
        }

        private void button4_Click(object sender, EventArgs e)
        {
            check = 3;
        }



        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;

            // Set the clear color.
            gl.ClearColor(0, 0, 0, 0);
            // Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);

            gl.RenderMode(OpenGL.GL_SELECT);
            // Load the identity.
            gl.LoadIdentity();


        }

        private void openGLControl_Resized(object sender, EventArgs e)
        {
            // Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            // Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            // Load the identity.
            gl.LoadIdentity();
            // Create a perspective transformation.
            gl.Viewport(0, 0, openGLControl.Width, openGLControl.Height);
            gl.Ortho2D(0, openGLControl.Width, 0, openGLControl.Height);
        }

        private void openGLControl_Load(object sender, EventArgs e)
        {

        }

        private void openGLControl_MouseDown(object sender, MouseEventArgs e)
        {

            pStart = e.Location;
            pEnd = pStart;
            checkClickDown = true;


        }

        private void openGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            pEnd = e.Location;
            checkClickDown = false;
            Stemp.pStart = pStart;
            Stemp.pEnd = pEnd;
            Stemp.Shape = check;
            Lgraph.Add(Stemp);
            pStart = pEnd;


        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            OpenGL gl = openGLControl.OpenGL;

            // Clear the color and depth buffer.
            gl.PushMatrix();
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            // for (int index=0;index<Lgraph.Count();index++)
            gl.Color(colorUserColor.R / 255.0, colorUserColor.G / 255.0, colorUserColor.B / 255.0, 0);
            gl.LineWidth(size);
            for (int index = 0; index < Lgraph.Count(); index++)
            {
                Repaint(Lgraph[index].pStart, Lgraph[index].pEnd, Lgraph[index].Shape, ref gl);
                if (index == Lgraph.Count() - 1)
                {
                    Show_Control_Point(Lgraph[index].pStart, Lgraph[index].pEnd, Lgraph[index].Shape, ref gl);
                }
            }
            gl.Color(colorUserColor.R / 255.0, colorUserColor.G / 255.0, colorUserColor.B / 255.0, 0);
            Repaint(pStart, pEnd, check, ref gl);
        }

        private void btn_Circle(object sender, EventArgs e)
        {
            check = 1;
        }

        private void btn_Line(object sender, EventArgs e)
        {
            check = 0;

        }



        private void btn_BangMau_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                colorUserColor = colorDialog1.Color;
            }
        }

        private void btn_Rectangle_Click(object sender, EventArgs e)
        {
            check = 2;
        }

        private void btn_TamGiacDeu_Click(object sender, EventArgs e)
        {
            check = 4;
        }



        private void num_Size_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cmBox_Size_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = cmBox_Size.SelectedItem.ToString();
            size = Convert.ToInt32(s);
        }

        private void openGLControl_MouseMove(object sender, MouseEventArgs e)
        {

            if (checkClickDown)
            {
                pEnd = e.Location;
            }
        }

        private void btn_NguGiac_Click(object sender, EventArgs e)
        {
            check = 5;
        }

        private void openGLControl_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void Paint_Load(object sender, EventArgs e)
        {

        }

        private void Paint_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            check = 6;
        }
    }
}
