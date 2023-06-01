using ai_lab_4;

string filePath = "D:/example.txt";  // Replace with your desired file path
string textToWrite = "Hello, world!";  // Replace with the text you want to write

Random random = new Random();
int numAnts = 20;
int numNodes = 20;

double[,] pheromones = new double[,]
{ {0.5, 1}, { 0.5, 2 }, { 0.5, 3 }, { 0.2, 1 },
{ 0.3, 1 }, { 0.4, 1 }, { 0.2, 2 }, { 0.3, 2 },
{ 0.7, 1 }, { 0.9, 5 } };

using (StreamWriter writer = new StreamWriter(filePath))
{
    double[,] matrix = GenerateWeightMatrix(numNodes, random);
    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        writer.Write("\n");
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            writer.Write(matrix[i, j] + " ");
        }
    }
    for (int n = 0; n < 10; n++)
    {
        
        writer.Write("\n");
        AntColonyOptimization solve = new AntColonyOptimization(numAnts, numNodes, GenerateWeightMatrix(numNodes, random), 1, 2, pheromones[n, 0], pheromones[n, 1]);
        List<int> road = solve.FindBestRoute(1000);
        foreach (int i in road)
        {
            writer.Write(i + "->");
        }
        writer.Write("\n");
        writer.Write("Path Length: " + PathLength(matrix, road));
        writer.Write("\nInitial pheromone: " + pheromones[n, 1]);
        writer.Write("\nEvaporation rate: " + pheromones[n, 0]);
        writer.Write("\n\n\n");
    }
}

static int PathLength(double[,] weightMatrix, List<int> route)
{
    double length = 0;
    for(int i = 0; i < route.Count - 1; i++)
    {
        length += weightMatrix[route[i], route[i + 1]];
    }
    return (int)length;
}
static double[,] GenerateWeightMatrix(int amountOfNodes, Random random)
{
    double[,] distanceMatrix = new double[amountOfNodes, amountOfNodes];
    for (int i = 0; i < distanceMatrix.GetLength(0); i++)
    {
        for (int j = i + 1; j < distanceMatrix.GetLength(1); j++)
        {
            int weight = random.Next(10, 100);
            distanceMatrix[i, j] = weight;
            distanceMatrix[j, i] = weight;
        }
    }
    return distanceMatrix;
}
static void CreateNodes(int amountOfNodes, Graph graph)
{
    for (int i = 0; i < amountOfNodes; i++)
    {
        graph.AddVertex("Vertex " + i);
    }
}
static void CreateEdges(int amountOfNodes, Graph graph, Random random)
{
    for (int i = 0; i < amountOfNodes; i++)
    {
        for (int j = i + 1; j < amountOfNodes; j++)
        {
            int weight = random.Next(10, 100);
            graph.AddEdge("Vertex " + i, "Vertex " + j, weight);
        }
    }
}