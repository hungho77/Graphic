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
using System.Collections;



namespace Graph
{
   
    
    public partial class Form1 : Form
    {
        private const int EPCILON = 1;
        private const int EPCILON_ELIPSE = 3;
        private Point pStart;
        private Point pEnd;
        private int check=-1;
        private int size;
        private bool checkClickDown = false;
        private bool checkClickShowControlPoint = false;//kiểm tra đã chon chế độ click để hiển thị show control point
        private int indexResetCtrlPoint=-1;// vị trí hình đã được chọn trong list để hiển thị control point


        private struct Scolor
        {
            public double R;
            public double G;
            public double B;
        };
        private struct Sgraph
        {
            public Point pStart;
            public Point pEnd;
            public int Shape;
            public int Size;
            public Scolor Color;
            public bool Check_Show_Ctrl;//kiểm tra xem có nên show point control ko trong list graph
            public List<Point> List_Graph_Point;
            public List<Point> List_pControl;
             
        };
        private List<Sgraph> Lgraph=new List<Sgraph>();
       
        private Sgraph S_graph_temp;
        private Scolor S_color_temp;

        static double Calc_distance_two_point(Point pStart, Point pEnd)
        {
            return Math.Sqrt(Math.Pow(pStart.X - pEnd.X, 2) + Math.Pow(pStart.Y - pEnd.Y, 2));
        }

        static void Reset_Check_Show_Control(ref List<Sgraph> Lgraph,ref int indexResetCtrlPoint)//khi khong chon che do di chuyen thi huy bỏ kich hoat control
        {
            Sgraph temp;
            
                temp = Lgraph[indexResetCtrlPoint];
                temp.Check_Show_Ctrl = false;
                Lgraph.RemoveAt(indexResetCtrlPoint);
                Lgraph.Insert(indexResetCtrlPoint, temp);
            indexResetCtrlPoint = -1;


        }
        static void Turn_Check_Show_Control(ref List<Sgraph> Lgraph, ref int index_turn_on)//khi khong chon che do di chuyen thi huy bỏ kich hoat control
        {
            Sgraph temp;

            temp = Lgraph[index_turn_on];
            temp.Check_Show_Ctrl = true;
            Lgraph.RemoveAt(index_turn_on);
            Lgraph.Insert(index_turn_on, temp);
            


        }


        //tạo điểm control point
        static void Create_Control_Point(Point pStart, Point pEnd, int Shape,ref List<Point> List_pControl)
        {
            double Rx, thetar;
            Point pTemp = new Point();
            switch (Shape)
            { 
                case 0:
                    pTemp = pStart;
                    List_pControl.Add(pTemp);
                    pTemp = pEnd;
                    List_pControl.Add(pTemp);
                 break;

                case 1:
                    Rx = Math.Sqrt(Math.Pow(pStart.X - pEnd.X, 2) + Math.Pow(pStart.Y - pEnd.Y, 2));
                    
                    for (int i = 0; i < 360; i+=90)
                    {
                        thetar = i * Math.PI / 180;
                        pTemp.X = (int)(pStart.X + Rx * Math.Cos(thetar));
                        pTemp.Y = (int)(pStart.Y + Rx * Math.Sin(thetar));
                        List_pControl.Add(pTemp);
                    }
                    Rx = Math.Sqrt(2)*Math.Sqrt(Math.Pow(pStart.X - pEnd.X, 2) + Math.Pow(pStart.Y - pEnd.Y, 2));

                    for (int i = 45; i < 360; i += 90)
                    {
                        thetar = i * Math.PI / 180;
                        pTemp.X = (int)(pStart.X + Rx * Math.Cos(thetar));
                        pTemp.Y = (int)(pStart.Y + Rx * Math.Sin(thetar));
                        List_pControl.Add(pTemp);
                    }




                    break;

                case 2:
                    pTemp = pStart;
                    List_pControl.Add(pTemp);
                    pTemp = pEnd;
                    List_pControl.Add(pTemp);
                    pTemp.X = pEnd.X;
                    pTemp.Y = pStart.Y;
                    List_pControl.Add(pTemp);
                    pTemp.X = pStart.X;
                    pTemp.Y = pEnd.Y;
                    List_pControl.Add(pTemp);
                    pTemp.X = (pStart.X + pEnd.X) / 2;
                    pTemp.Y = pStart.Y;
                    List_pControl.Add(pTemp);
                    pTemp.X = pStart.X;
                    pTemp.Y = (pEnd.Y + pStart.Y) / 2;
                    List_pControl.Add(pTemp);
                    pTemp.X= pTemp.X = (pStart.X + pEnd.X) / 2;
                    pTemp.Y = pEnd.Y;
                    List_pControl.Add(pTemp);                   
                    pTemp.X = pEnd.X;
                    pTemp.Y = (pEnd.Y + pStart.Y) / 2;
                    List_pControl.Add(pTemp);
                   break;
                case 3:
                    pTemp.X = 2 * pStart.X - pEnd.X;
                    pTemp.Y = 2 * pStart.Y - pEnd.Y;
                    List_pControl.Add(pTemp);
                    pTemp.X = pStart.X;
                    List_pControl.Add(pTemp);
                    pTemp.X = pEnd.X;
                    List_pControl.Add(pTemp);
                    pTemp.Y = pStart.Y;
                    List_pControl.Add(pTemp);
                    List_pControl.Add(pEnd);
                    pTemp.X = 2 * pStart.X - pTemp.X;
                    List_pControl.Add(pTemp);
                    pTemp.Y = pEnd.Y;
                    List_pControl.Add(pTemp);
                    pTemp.X = pStart.X;
                    List_pControl.Add(pTemp);
                    break;
                case 4:
                    Rx = Math.Sqrt(Math.Pow(pStart.X - pEnd.X, 2) + Math.Pow(pStart.Y - pEnd.Y, 2));

                    for (int i = 0; i < 360; i += 120)
                    {
                        thetar = i * Math.PI / 180;
                        pTemp.X = (int)(pStart.X + Rx * Math.Cos(thetar+ Math.PI/2));
                        pTemp.Y = (int)(pStart.Y + Rx * Math.Sin(thetar - Math.PI / 2));
                        List_pControl.Add(pTemp);
                    }
                    pTemp.X = List_pControl[2].X;
                    pTemp.Y = List_pControl[0].Y;
                    List_pControl.Add(pTemp);
                    pTemp.X = List_pControl[1].X;
                    pTemp.Y = List_pControl[0].Y;
                    List_pControl.Add(pTemp);
                    pTemp.X = List_pControl[2].X;
                    pTemp.Y= (List_pControl[2].Y + List_pControl[3].Y) / 2;
                    List_pControl.Add(pTemp);
                    pTemp.X = List_pControl[1].X;
                    pTemp.Y = (List_pControl[4].Y + List_pControl[1].Y) / 2;
                    List_pControl.Add(pTemp);
                    pTemp.X = List_pControl[0].X;
                    pTemp.Y = List_pControl[1].Y;
                    List_pControl.Add(pTemp);
                    break;
                 
                case 5:
                    Rx = Math.Sqrt(Math.Pow(pStart.X - pEnd.X, 2) + Math.Pow(pStart.Y - pEnd.Y, 2));       
                    
                    pTemp.X = pStart.X;
                    pTemp.Y = (int)(pStart.Y - Rx);
                    List_pControl.Add(pTemp);
                    pTemp.X = pStart.X;
                    pTemp.Y = (int)(pStart.Y + Rx * Math.Cos((36 * Math.PI) / 180));
                    List_pControl.Add(pTemp);
                    pTemp.X = (int)(pStart.X + Rx);
                    List_pControl.Add(pTemp);
                    pTemp.X = (int)(pStart.X - Rx);
                    List_pControl.Add(pTemp);
                    pTemp.Y = (int)(pStart.Y - Rx);
                    List_pControl.Add(pTemp);
                    pTemp.X = (int)(pStart.X + Rx);
                    List_pControl.Add(pTemp);
                    pTemp.Y = (int)((pTemp.Y + List_pControl[2].Y) / 2);
                    List_pControl.Add(pTemp);
                    pTemp.X = (int)(pStart.X - Rx);
                    List_pControl.Add(pTemp);

                    break;
                case 6:
                    Rx = Math.Sqrt(Math.Pow(pStart.X - pEnd.X, 2) + Math.Pow(pStart.Y - pEnd.Y, 2));
                    double R1 = Rx * Math.Cos((30 * Math.PI) / 180);
                    pTemp.X = pStart.X;
                    pTemp.Y = (int)(pStart.Y - R1);
                    List_pControl.Add(pTemp);
                    pTemp.Y = (int)(pStart.Y + R1);
                    List_pControl.Add(pTemp);
                    pTemp.Y = pStart.Y;
                    pTemp.X = (int)(pStart.X - Rx);
                    List_pControl.Add(pTemp);
                    pTemp.X = (int)(pStart.X + Rx);
                    List_pControl.Add(pTemp);
                    pTemp.X = List_pControl[2].X;
                    pTemp.Y = List_pControl[0].Y;
                    List_pControl.Add(pTemp);
                    pTemp.Y = List_pControl[1].Y;
                    List_pControl.Add(pTemp);
                    pTemp.X = List_pControl[3].X;
                    List_pControl.Add(pTemp);
                    pTemp.Y = List_pControl[0].Y;
                    List_pControl.Add(pTemp);

                    break;
            }
        }
        //kiêm tra xem chọn được đường thẳng ko

        static bool Select_Line(Point pStart, Point pEnd,Point pClick )//chon duong thẳng hiển thi control point
        {
            double _d_AB = Calc_distance_two_point(pStart, pClick);
            double _d_BC= Calc_distance_two_point(pEnd, pClick);
            double _d_AC= Calc_distance_two_point(pStart, pEnd);
            if (Math.Abs(_d_AB+_d_BC-_d_AC)<=EPCILON)
            return true;
            return false;
        }

        //kiêm tra chon hinh tròn
        static bool Select_Circle(Point pStart, Point pEnd, Point pClick)
        {
            double R = Calc_distance_two_point(pStart, pEnd);
            double R1 = Calc_distance_two_point(pClick, pStart);
            if (Math.Abs(R1 - R) < EPCILON)
            {
                return true;
            }
            return false;
            
        }

        //kiem tra chon elip
        static bool Select_Elippse(Point pStart, Point pEnd, Point pClick)
        {
            double Rx = Math.Abs(pEnd.X - pStart.X);
            double Ry = Math.Abs(pEnd.Y - pStart.Y);
            double c = Math.Sqrt(Math.Pow(Rx, 2) - Math.Pow(Ry, 2));
            Point F1, F2;
            F1 = new Point();
            F2= new Point(); 
            F1.X = (int)(pStart.X + c);
            F2.X = (int)(pStart.X - c);
            F1.Y = pStart.Y;
            F2.Y = pStart.Y;
            double MF1 = Calc_distance_two_point(F1, pClick);
            double MF2 = Calc_distance_two_point(F2, pClick);
            if (Math.Abs(MF1 + MF2 - 2 * Rx) <= EPCILON)
                return true;
            return false;

        }


        //chọn hình hiển thị control point
        static void Select_Graph(ref List<Sgraph> Lgraph, Point pClick,ref int indexResetCtrlPoint)
        {
            if (indexResetCtrlPoint!=-1)
                Reset_Check_Show_Control(ref Lgraph, ref indexResetCtrlPoint);

            for (int index=0;index<Lgraph.Count();index++)
            {
                switch (Lgraph[index].Shape)
                {
                    case 0://chon duong thang
                        if (Select_Line(Lgraph[index].pStart, Lgraph[index].pEnd, pClick))
                        {
                            Turn_Check_Show_Control(ref Lgraph, ref index);
                            indexResetCtrlPoint = index;
                        }
                        break;
                    case 1://chon hinh tron
                        if (Select_Circle(Lgraph[index].pStart, Lgraph[index].pEnd, pClick))
                        {
                            Turn_Check_Show_Control(ref Lgraph, ref index);
                            indexResetCtrlPoint = index;
                        }
                        break;
                    case 2://chon hinh chu nhat
                        for (int i=0;i<Lgraph[index].List_Graph_Point.Count()-1;i++)
                        {
                            if (Select_Line(Lgraph[index].List_Graph_Point[i], Lgraph[index].List_Graph_Point[i+1], pClick))
                            {
                                Turn_Check_Show_Control(ref Lgraph, ref index);
                                indexResetCtrlPoint = index;
                                break;
                            }
                        }
                        
                        if (Select_Line(Lgraph[index].List_Graph_Point[0], Lgraph[index].List_Graph_Point[3], pClick))
                        {
                            Turn_Check_Show_Control(ref Lgraph, ref index);
                            indexResetCtrlPoint = index;
                        }
                        break;
                    case 3:
                        if (Select_Elippse(Lgraph[index].pStart, Lgraph[index].pEnd, pClick))
                        {
                            Turn_Check_Show_Control(ref Lgraph, ref index);
                            indexResetCtrlPoint = index;
                        }
                        break;
                    case 4://chon tam giac
                        if (Select_Line(Lgraph[index].List_Graph_Point[0], Lgraph[index].List_Graph_Point[1], pClick)
                            || Select_Line(Lgraph[index].List_Graph_Point[1], Lgraph[index].List_Graph_Point[2], pClick)
                            || Select_Line(Lgraph[index].List_Graph_Point[2], Lgraph[index].List_Graph_Point[0], pClick))
                        {
                            Turn_Check_Show_Control(ref Lgraph, ref index);
                            indexResetCtrlPoint = index;
                        }
                        break;
                    case 5:
                        for (int i = 0; i < Lgraph[index].List_Graph_Point.Count() - 1; i++)
                        {
                            if (Select_Line(Lgraph[index].List_Graph_Point[i], Lgraph[index].List_Graph_Point[i + 1], pClick))
                            {
                                Turn_Check_Show_Control(ref Lgraph, ref index);
                                indexResetCtrlPoint = index;
                                break;
                            }
                        }

                        if (Select_Line(Lgraph[index].List_Graph_Point[0], Lgraph[index].List_Graph_Point[4], pClick))
                        {
                            Turn_Check_Show_Control(ref Lgraph, ref index);
                            indexResetCtrlPoint = index;
                        }
                        break;
                    case 6:
                        for (int i = 0; i < Lgraph[index].List_Graph_Point.Count() - 1; i++)
                        {
                            if (Select_Line(Lgraph[index].List_Graph_Point[i], Lgraph[index].List_Graph_Point[i + 1], pClick))
                            {
                                Turn_Check_Show_Control(ref Lgraph, ref index);
                                indexResetCtrlPoint = index;
                                break;
                            }
                        }

                        if (Select_Line(Lgraph[index].List_Graph_Point[0], Lgraph[index].List_Graph_Point[5], pClick))
                        {
                            Turn_Check_Show_Control(ref Lgraph, ref index);
                            indexResetCtrlPoint = index;
                        }
                        break;




                }
            }
        }



            //vẽ điểm control point
        static void Draw_Control_Point(int Shape,  List<Point> List_pControl, ref OpenGL gl)
        {
            gl.PointSize(5);
            gl.Color(Color.Yellow.R / 255.0, Color.Yellow.G / 255.0, Color.Yellow.B / 255.0, 0);
            gl.Begin(OpenGL.GL_POINTS);
            for (int i = 0; i < List_pControl.Count(); i++)
            {
                gl.Vertex(List_pControl[i].X, gl.RenderContextProvider.Height - List_pControl[i].Y);
            }
            gl.End();
            gl.Flush(); // Thực hiện lệnh vẽ ngay lập tức thay vì đợi sau 1 khoảng thời gian
        }

        //vẽ hình
        static void Repaint(Point pStart, Point pEnd, int Shape, int Size, Scolor Color,bool checkClickDown,ref Sgraph temp, ref OpenGL gl)
        {
            double Rx, Ry;
            Point pTemp = new Point();
            gl.Color(Color.R / 255.0, Color.G / 255.0, Color.B / 255.0, 0);
            gl.LineWidth(Size);
            switch (Shape)
            {
                case 0: //vẽ đoạn thẳng   

                    gl.Begin(OpenGL.GL_LINES); // chọn chế độ vẽ đường thẳng
                    gl.Vertex(pStart.X, gl.RenderContextProvider.Height - pStart.Y);
                    gl.Vertex(pEnd.X, gl.RenderContextProvider.Height - pEnd.Y);

                    gl.End();
                    gl.Flush();// Thực hiện lệnh vẽ ngay lập tức thay vì đợi sau 1 khoảng thời gian 



                    break;
                case 1://vẽ hình tròn
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
                case 2://ve hình chữ nhật
                    
                    gl.Begin(OpenGL.GL_LINE_LOOP);
                    gl.Vertex(pStart.X, gl.RenderContextProvider.Height - pStart.Y);
                    gl.Vertex(pStart.X, gl.RenderContextProvider.Height - pEnd.Y);
                    gl.Vertex(pEnd.X, gl.RenderContextProvider.Height - pEnd.Y);
                    gl.Vertex(pEnd.X, gl.RenderContextProvider.Height - pStart.Y);
                    
                    gl.End();
                    gl.Flush();// Thực hiện lệnh vẽ ngay lập tức thay vì đợi sau 1 khoảng thời gian
                    if (checkClickDown == false)
                    {
                        temp.List_Graph_Point.Clear();
                        temp.List_Graph_Point.Add(pStart);
                        pTemp.X = pStart.X;
                        pTemp.Y = pEnd.Y;
                        temp.List_Graph_Point.Add(pTemp);
                        temp.List_Graph_Point.Add(pEnd);
                        pTemp.X = pEnd.X;
                        pTemp.Y = pStart.Y;
                        temp.List_Graph_Point.Add(pTemp);
                    }
                    break;

                case 3://ve elip
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

                case 4://ve tam giac deu

                    Rx = Math.Sqrt(Math.Pow(pStart.X - pEnd.X, 2) + Math.Pow(pStart.Y - pEnd.Y, 2));

                    gl.Begin(OpenGL.GL_LINE_LOOP);
                    
                    for (int i = 0; i < 360; i+=120)
                    {
                        
                        {
                            thetar = i * Math.PI / 180;
                            gl.Vertex(pStart.X + Rx * Math.Cos(thetar-Math.PI/2), gl.RenderContextProvider.Height - pStart.Y - Rx * Math.Sin(thetar-Math.PI/2));

                        }
                    }
                    gl.End();
                    gl.Flush();// Thực hiện lệnh vẽ ngay lập tức thay vì đợi sau 1 khoảng thời gian
                    if (checkClickDown == false)
                    {
                        temp.List_Graph_Point.Clear();
                        for (int i = 0; i < 360; i += 120)
                        {

                            {
                                thetar = i * Math.PI / 180;                               
                                pTemp.X = (int)(pStart.X + Rx * Math.Cos(thetar - Math.PI / 2));
                                pTemp.Y = (int)(pStart.Y + Rx * Math.Sin(thetar - Math.PI / 2));
                                temp.List_Graph_Point.Add(pTemp);
                            }
                        }
                    }
                    break;
                case 5://ve ngu giac deu

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
                    gl.Flush();// Thực hiện lệnh vẽ ngay lập tức thay vì đợi sau 1 khoảng thời gian
                    if (checkClickDown == false)
                    {
                        temp.List_Graph_Point.Clear();
                        for (int i = 0; i < 360; i += 72)
                        {

                            {
                                thetar = i * Math.PI / 180;
                                pTemp.X = (int)(pStart.X + Rx * Math.Cos(thetar + Math.PI / 2));
                                pTemp.Y = (int)(pStart.Y - Rx * Math.Sin(thetar + Math.PI / 2));
                                temp.List_Graph_Point.Add(pTemp);
                            }
                        }
                    }
                    break;

                case 6://ve luc giac deu
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
                    gl.Flush();// Thực hiện lệnh vẽ ngay lập tức thay vì đợi sau 1 khoảng thời gian
                    if (checkClickDown == false)
                    {
                        temp.List_Graph_Point.Clear();
                        for (int i = 0; i < 360; i ++)
                        {
                            if (i % 30 == 0 && i % 60 != 0)
                            {
                                thetar = i * Math.PI / 180;
                                pTemp.X = (int)(pStart.X + Rx * Math.Cos(thetar + Math.PI / 2));
                                pTemp.Y = (int)(pStart.Y - Rx * Math.Sin(thetar + Math.PI / 2));
                                temp.List_Graph_Point.Add(pTemp);
                            }
                        }
                    }
                    break;
            }
           
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            check = 3;
            checkClickShowControlPoint = false;
            if (indexResetCtrlPoint != -1)
                Reset_Check_Show_Control(ref Lgraph, ref indexResetCtrlPoint);
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
            

            if (checkClickShowControlPoint)
            {

                //Sgraph temp;
                //for (int index = 0; index < Lgraph.Count(); index++)
                //{
                //    if (Lgraph[index].Shape == 0)
                //        if (Select_Line(Lgraph[index].pStart, Lgraph[index].pEnd, pStart))
                //        {
                //            indexResetCtrlPoint = index;
                //            temp = Lgraph[index];
                //            temp.Check_Show_Ctrl = true;
                //            Lgraph.RemoveAt(index);
                //            Lgraph.Insert(index, temp);

                //            break;
                //        }

                //}
                Select_Graph(ref Lgraph, pStart, ref indexResetCtrlPoint);
            }


        }

        private void openGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            //sau khi thả chuột thì thêm vào list graph
            pEnd = e.Location;
            checkClickDown = false;        
            S_graph_temp.List_pControl = new List<Point>();
            S_graph_temp.List_Graph_Point= new List<Point>();
            S_graph_temp.pStart = pStart;
            S_graph_temp.pEnd = pEnd;
            S_graph_temp.Shape = check;
            S_graph_temp.Size = size;
            S_graph_temp.Color = S_color_temp;
            S_graph_temp.Check_Show_Ctrl = false;
            if (check != -1)
            {
                Create_Control_Point(pStart, pEnd, check, ref S_graph_temp.List_pControl);
                Lgraph.Add(S_graph_temp);
                
            }
            string s = "";
            s = s + Convert.ToString(Lgraph.Count());
            lbl_msg.Text = s;
            pStart = pEnd;


        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            OpenGL gl = openGLControl.OpenGL;
            
            // Clear the color and depth buffer.
            gl.PushMatrix();
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
           // for (int index=0;index<Lgraph.Count();index++)
            
            for (int index=0;index<Lgraph.Count();index++)
            {
                Repaint(Lgraph[index].pStart, Lgraph[index].pEnd,  Lgraph[index].Shape, 
                    Lgraph[index].Size, Lgraph[index].Color,checkClickDown,ref S_graph_temp, ref gl);
                if (Lgraph[index].Check_Show_Ctrl)
                Draw_Control_Point(check, Lgraph[index].List_pControl,ref gl);
                
                
            }
            if (checkClickDown)
            Repaint(pStart, pEnd, check,size, S_color_temp,checkClickDown,ref S_graph_temp, ref gl);
            
            
           





        }

        private void btn_Circle(object sender, EventArgs e)
        {
            check = 1;
            checkClickShowControlPoint = false;
            if (indexResetCtrlPoint != -1)
                Reset_Check_Show_Control(ref Lgraph, ref indexResetCtrlPoint);

        }

        private void btn_Line(object sender, EventArgs e)
        {
            check = 0;
            checkClickShowControlPoint = false;
            if (indexResetCtrlPoint!=-1)
            Reset_Check_Show_Control(ref Lgraph,ref indexResetCtrlPoint);



        }

      

        private void btn_BangMau_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog()==DialogResult.OK)
            {
                S_color_temp.R = colorDialog1.Color.R;
                S_color_temp.G = colorDialog1.Color.G;
                S_color_temp.B = colorDialog1.Color.B;
            }
        }

        private void btn_Rectangle_Click(object sender, EventArgs e)
        {
            check = 2;
            checkClickShowControlPoint = false;
            if (indexResetCtrlPoint != -1)
                Reset_Check_Show_Control(ref Lgraph, ref indexResetCtrlPoint);

        }

        private void btn_TamGiacDeu_Click(object sender, EventArgs e)
        {
            check = 4;
            checkClickShowControlPoint = false;
            if (indexResetCtrlPoint != -1)
                Reset_Check_Show_Control(ref Lgraph, ref indexResetCtrlPoint);
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
            checkClickShowControlPoint = false;
            if (indexResetCtrlPoint != -1)
                Reset_Check_Show_Control(ref Lgraph, ref indexResetCtrlPoint);
        }

        private void openGLControl_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void Paint_Load(object sender, EventArgs e)
        {
            check = -1;
            
        }

        private void Paint_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            check = 6;
            checkClickShowControlPoint = false;
            if (indexResetCtrlPoint != -1)
                Reset_Check_Show_Control(ref Lgraph, ref indexResetCtrlPoint);
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            
            check = -1;
            checkClickShowControlPoint = true;
            

        }

        private void label1_Click(object sender, EventArgs e)
        {
            string s = "";
            s = s + Convert.ToString(Lgraph.Count());
            lbl_msg.Text = s;

        }
    }
}
