using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AI_lab3
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
            for (int i = 0; i < 1000; i++)
            {
                points.Add(new Point(GetRandomNumber(0, 1000, random), GetRandomNumber(0, 1000, random)));

            }
            

            foreach (Point f in points)
            {
                chart1.Series["Series1"].Points.AddXY(f.X, f.Y);
            }
            chart1.Series["Series1"].Points[0].Color = Color.Red;
            Color[] colors = new Color[4] { Color.Red, Color.Blue, Color.Yellow, Color.Green };

            
            KMeans(points, GetRandomClusters(random, 4), 1000, colors);

            
            Application.DoEvents();
        }
        void KMeans(List<Point> dataPoints, List<Point> clusters, int iterations, Color[] colors)
        {
            for(int k = 0; k < 50; k++)
            {
                //points color
                List<Point>[] clusterPoints = new List<Point>[clusters.Count];
                for(int i = 0; i < clusterPoints.Length; i++)
                {
                    clusterPoints[i] = new List<Point>();
                }
                for (int i = 0; i < iterations; i++)
                {
                    double distance = EuclideanDistance(dataPoints[i], clusters[0]);
                    int cluster = 0;
                    for (int j = 1; j < clusters.Count; j++)
                    {
                        if (distance > EuclideanDistance(dataPoints[i], clusters[j]))
                        {
                            distance = EuclideanDistance(dataPoints[i], clusters[j]);
                            cluster = j;
                        }
                    }
                    clusterPoints[cluster].Add(dataPoints[i]);
                    chart1.Series["Series1"].Points[i].Color = colors[cluster];
                }

                //new centroid
                for (int i = 0; i < clusterPoints.Length; i++)
                {
                    double xAvg = 0, yAvg = 0;
                    for (int j = 0; j < clusterPoints[i].Count; j++)
                    {
                        xAvg += clusterPoints[i][j].X;
                        yAvg += clusterPoints[i][j].X;
                    }
                    xAvg /= clusterPoints[i].Count;
                    yAvg /= clusterPoints[i].Count;
                    clusters[i] = new Point(xAvg, yAvg);
                }
            }

        }
        double GetRandomNumber(double minimum, double maximum, Random random)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        List<Point> GetRandomClusters(Random random, int n)
        {
            List<Point> clusters = new List<Point>();
            for(int i = 0; i < n; i++)
            {
                clusters.Add(new Point(GetRandomNumber(0, 1000, random), GetRandomNumber(0, 1000, random)));
            }
            return clusters;
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
