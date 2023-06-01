using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI_lab3_2__viz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            double[,] points = new double[1000,2] ;
            double eps = 30;
            int minPts = 5;



            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 500; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        points[i, k] = GetRandomNumber(600, 700, random);
                    }
                }
            }
            for (int i = 500; i < 1000; i++)
            {
                for (int j = 500; j < 1000; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        points[i, k] = GetRandomNumber(400, 500, random);
                    }
                }
            }
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    points[i, j] = GetRandomNumber(0, 1000, random);
                }
            }
            for (int i = 0; i < 1000; i++)
            {
                chart2.Series["Series2"].Points.AddXY(points[i, 0], points[i, 1]);
            }
            var clusters = DBSCAN(points, eps, minPts);
            
            Color[] colors = new Color[4] { Color.Red, Color.Blue, Color.Yellow, Color.Green };
            int iter = 0;
            int iter2 = 0;
            Console.WriteLine("Number of clusters: " + clusters.Count);
            for (int i = 0; i < clusters.Count; i++)
            {
                Console.WriteLine("Cluster " + i + ":");
                foreach (var point in clusters[i])
                {
                    chart1.Series["Series1"].Points.AddXY(point[0], point[1]);
                    iter++;
                }
                for(int j = iter2; j < iter; j++)
                {
                    iter2++;
                    chart1.Series["Series1"].Points[j].Color = Color.Red;
                }
            }
            
            chart1.Series["Series1"].Points[0].Color = Color.Red;
        }
        double GetRandomNumber(double minimum, double maximum, Random random)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }


        static List<List<double[]>> DBSCAN(double[,] points, double eps, int minPts)
        {
            int n = points.GetLength(0);
            bool[] visited = new bool[n];
            List<List<double[]>> clusters = new List<List<double[]>>();

            for (int i = 0; i < n; i++)
            {
                if (!visited[i])
                {
                    visited[i] = true;
                    List<double[]> cluster = new List<double[]>();
                    cluster.Add(new double[] { points[i, 0], points[i, 1] });

                    List<int> neighbors = RangeQuery(points, i, eps);
                    if (neighbors.Count >= minPts)
                    {
                        ExpandCluster(points, visited, neighbors, cluster, eps, minPts);
                        clusters.Add(cluster);
                    }
                }
            }

            return clusters;
        }

        static void ExpandCluster(double[,] points, bool[] visited, List<int> neighbors, List<double[]> cluster, double eps, int minPts)
        {
            for (int i = 0; i < neighbors.Count; i++)
            {
                int index = neighbors[i];
                if (!visited[index])
                {
                    visited[index] = true;
                    List<int> newNeighbors = RangeQuery(points, index, eps);
                    if (newNeighbors.Count >= minPts)
                    {
                        neighbors.AddRange(newNeighbors);
                    }
                }

                bool alreadyInCluster = false;
                for (int j = 0; j < cluster.Count; j++)
                {
                    if (cluster[j][0] == points[index, 0] && cluster[j][1] == points[index, 1])
                    {
                        alreadyInCluster = true;
                        break;
                    }
                }

                if (!alreadyInCluster)
                {
                    cluster.Add(new double[] { points[index, 0], points[index, 1] });
                }
            }
        }

        static List<int> RangeQuery(double[,] points, int index, double eps)
        {
            List<int> neighbors = new List<int>();
            for (int i = 0; i < points.GetLength(0); i++)
            {
                if (i != index)
                {
                    double dist = Math.Sqrt(Math.Pow(points[i, 0] - points[index, 0], 2) + Math.Pow(points[i, 1] - points[index, 1], 2));
                    if (dist <= eps)
                    {
                        neighbors.Add(i);
                    }
                }
            }
            return neighbors;
        }
        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }
    }
}
