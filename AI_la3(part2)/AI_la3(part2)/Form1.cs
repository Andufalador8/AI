using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI_la3_part2_
{
    public partial class Form1 : Form
    {
        public class Point
        {
            public double X;
            public double Y;

            public Point(double x, double y)
            {
                X = x;
                Y = y;
            }
        }
        public Form1()
        {
            InitializeComponent();
            List<Point> points = new List<Point>();
            Random random = new Random();
            for (int i = 0; i < 300; i++)
            {
                points.Add(new Point(GetRandomNumber(50, 55, random), 
                    GetRandomNumber(50, 55, random)));
                points.Add(new Point(GetRandomNumber(40, 45, random),
                    GetRandomNumber(40, 45, random)));
                points.Add(new Point(GetRandomNumber(5, 20, random),
                    GetRandomNumber(5, 20, random)));
            }

            foreach (Point f in points)
            {
                chart1.Series["Series1"].Points.AddXY(f.X, f.Y);
            }

            double eps = 2;
            double minNeighbors = 3;
            Application.DoEvents();
        }
        private bool InCircle(Point centrePoint, Point checkPoint, double radius)
        {
            if(EuclideanDistance(centrePoint, checkPoint) > radius)
            {
                return false;
            }
            return true;
        }
        double GetRandomNumber(double minimum, double maximum, Random random)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        private double EuclideanDistance(Point firstPoint, Point secondPoint)
        {
            return Math.Pow(firstPoint.X - secondPoint.X, 2) + Math.Pow(firstPoint.Y - secondPoint.Y, 2);
        }
        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
