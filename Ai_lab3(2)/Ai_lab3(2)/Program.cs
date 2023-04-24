class Program
{
    static void Main(string[] args)
    {
        
        double[,] points = { { 1, 2 }, { 2, 2 }, { 2, 3 }, { 8, 9 }, { 8, 8 }, { 8, 7 }, { 25, 80 } };

        
        double eps = 5;
        int minPts = 2;

        
        var clusters = DBSCAN(points, eps, minPts);

        
        Console.WriteLine("Number of clusters: " + clusters.Count);
        for (int i = 0; i < clusters.Count; i++)
        {
            Console.WriteLine("Cluster " + i + ":");
            foreach (var point in clusters[i])
            {
                Console.WriteLine("(" + point[0] + ", " + point[1] + ")");
            }
        }
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
}