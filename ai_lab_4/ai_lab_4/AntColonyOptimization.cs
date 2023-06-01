using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ai_lab_4
{
    public class AntColonyOptimization
    {
        private int numAnts;
        private int numNodes;
        private double[,] distanceMatrix;
        private double[,] pheromoneMatrix;
        private double alpha;
        private double beta;
        private double evaporationRate;
        private double initialPheromoneLevel;
        private Random rand = new Random();

        public AntColonyOptimization(int numAnts, int numNodes, double[,] distanceMatrix, double alpha, double beta, double evaporationRate, double initialPheromoneLevel)
        {
            this.numAnts = numAnts;
            this.numNodes = numNodes;
            this.distanceMatrix = distanceMatrix;
            this.alpha = alpha;
            this.beta = beta;
            this.evaporationRate = evaporationRate;
            this.initialPheromoneLevel = initialPheromoneLevel;

            // Initialize pheromone matrix with initial value
            pheromoneMatrix = new double[numNodes, numNodes];
            for (int i = 0; i < numNodes; i++)
            {
                for (int j = 0; j < numNodes; j++)
                {
                    pheromoneMatrix[i, j] = initialPheromoneLevel;
                }
            }
        }

        public List<int> FindBestRoute(int numIterations)
        {
            List<int> bestRoute = null;
            double bestRouteLength = double.MaxValue;

            // Iterate for the given number of iterations
            for (int iteration = 0; iteration < numIterations; iteration++)
            {
                List<List<int>> antRoutes = new List<List<int>>();

                // Create ants and let them find routes
                for (int i = 0; i < numAnts; i++)
                {
                    List<int> antRoute = FindAntRoute();
                    antRoutes.Add(antRoute);

                    // Update best route if this ant found a shorter route
                    double antRouteLength = CalculateRouteLength(antRoute);
                    if (antRouteLength < bestRouteLength)
                    {
                        bestRoute = antRoute;
                        bestRouteLength = antRouteLength;
                    }
                }

                // Update pheromone matrix
                UpdatePheromoneMatrix(antRoutes);

                // Evaporate pheromone
                EvaporatePheromone();
            }

            return bestRoute;
        }

        public List<int> FindAntRoute()
        {
            int startingNode = rand.Next(numNodes);
            List<int> antRoute = new List<int>();
            antRoute.Add(startingNode);

            while (antRoute.Count < numNodes)
            {
                int currentNode = antRoute[antRoute.Count - 1];
                int nextNode = ChooseNextNode(currentNode, antRoute);
                antRoute.Add(nextNode);
            }

            return antRoute;
        }

        public int ChooseNextNode(int currentNode, List<int> visitedNodes)
        {
            double[] probabilities = new double[numNodes];
            double probabilitiesSum = 0;

            // Calculate probabilities for each unvisited node
            for (int i = 0; i < numNodes; i++)
            {
                if (!visitedNodes.Contains(i))
                {
                    double pheromoneLevel = pheromoneMatrix[currentNode, i];
                    double distance = distanceMatrix[currentNode, i];
                    double probability = Math.Pow(pheromoneLevel, alpha) * Math.Pow(1 / distance, beta);
                    probabilities[i] = probability;
                    probabilitiesSum += probability;
                }
            }

            // Normalize probabilities
            for (int i = 0; i < numNodes; i++)
            {
                probabilities[i] /= probabilitiesSum;
            }

            // Choose the next node based on the probabilities
            double randomValue = rand.NextDouble();
            double cumulativeProbability = 0;

            for (int i = 0; i < numNodes; i++)
            {
                if (!visitedNodes.Contains(i))
                {
                    cumulativeProbability += probabilities[i];

                    if (randomValue <= cumulativeProbability)
                    {
                        return i;
                    }
                }
            }

            // If the function hasn't returned yet, it means all nodes have been visited
            // Return the first unvisited node
            for (int i = 0; i < numNodes; i++)
            {
                if (!visitedNodes.Contains(i))
                {
                    return i;
                }
            }

            // This code should never be reached
            return -1;
        }

        public double CalculateRouteLength(List<int> route)
        {
            double routeLength = 0;

            for (int i = 0; i < numNodes - 1; i++)
            {
                int node1 = route[i];
                int node2 = route[i + 1];
                routeLength += distanceMatrix[node1, node2];
            }

            int lastNode = route[numNodes - 1];
            int firstNode = route[0];
            routeLength += distanceMatrix[lastNode, firstNode];

            return routeLength;
        }

        public void UpdatePheromoneMatrix(List<List<int>> antRoutes)
        {
            // Initialize a new pheromone matrix with the same initial value
            double[,] newPheromoneMatrix = new double[numNodes, numNodes];
            for (int i = 0; i < numNodes; i++)
            {
                for (int j = 0; j < numNodes; j++)
                {
                    newPheromoneMatrix[i, j] = initialPheromoneLevel;
                }
            }

            // Update pheromone based on ant routes
            foreach (List<int> antRoute in antRoutes)
            {
                double routeLength = CalculateRouteLength(antRoute);

                for (int i = 0; i < numNodes - 1; i++)
                {
                    int node1 = antRoute[i];
                    int node2 = antRoute[i + 1];
                    double pheromoneDelta = 1 / routeLength;
                    newPheromoneMatrix[node1, node2] += pheromoneDelta;
                    newPheromoneMatrix[node2, node1] += pheromoneDelta;
                }

                int lastNode = antRoute[numNodes - 1];
                int firstNode = antRoute[0];
                double pheromoneDelta2 = 1 / routeLength;
                newPheromoneMatrix[lastNode, firstNode] += pheromoneDelta2;
                newPheromoneMatrix[firstNode, lastNode] += pheromoneDelta2;
            }

            // Replace old pheromone matrix with the updated one
            pheromoneMatrix = newPheromoneMatrix;
        }

        public void EvaporatePheromone()
        {
            for (int i = 0; i < numNodes; i++)
            {
                for (int j = 0; j < numNodes; j++)
                {
                    pheromoneMatrix[i, j] *= (1 - evaporationRate);
                }
            }
        }
    }
}
