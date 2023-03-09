
Random rand = new Random();
int[] initialState = GetBoard();

PrintBoard(initialState);
int[] solution = SimulatedAnnealing(initialState, 1000, 0.9995, 0.001);
if (solution != null)
{
    Console.WriteLine("Solution found:");
    PrintBoard(solution);
}
else
{
    Console.WriteLine("No solution found.");
}
Console.ReadKey();





int[] SimulatedAnnealing(int[] initialState, double initialTemperature, double coolingRate, double minimumTemperature)
{
    int[] currentState = initialState;
    int[] bestState = currentState;
    int currentConfliscts = CalculateConflicts(currentState);
    int bestEnergy = currentConfliscts;
    double temperature = initialTemperature;

    while (temperature > minimumTemperature)
    {
        int[] nextState = GenerateNeighbor(currentState);
        int nextConflicts = CalculateConflicts(nextState);
        int conflictsDelta = nextConflicts - currentConfliscts;
        if (conflictsDelta <= 0 || Math.Exp(-conflictsDelta / temperature) > rand.NextDouble())
        {
            currentState = nextState;
            currentConfliscts = nextConflicts;
            if (currentConfliscts < bestEnergy)
            {
                bestState = currentState;
                bestEnergy = currentConfliscts;
            }
        }
        temperature *= coolingRate;
    }
    return bestState;
}


int CalculateConflicts(int[] state)
{
    int conflicts = 0;
    for (int i = 0; i < state.Length; i++)
    {
        for (int j = i + 1; j < state.Length; j++)
        {
            if (state[i] == state[j] || Math.Abs(state[i] - state[j]) == j - i)
            {
                conflicts++;
            }
        }
    }
    return conflicts;
}

int[] GenerateNeighbor(int[] state)
{
    int[] neighbor = (int[])state.Clone();
    int i = rand.Next(state.Length);
    int j = rand.Next(state.Length);
    neighbor[i] = j;
    return neighbor;
}

int[] GetBoard()
{
    int[] queens= new int[12];
    for(int i = 0; i < queens.Length; i++)
    {
        queens[i] = rand.Next(12);
    }
    return queens;
}
void PrintBoard(int[] state)
{
    int n = state.Length;
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            if (state[i] == j)
            {
                Console.Write("Q ");
            }
            else
            {
                Console.Write(". ");
            }
        }
        Console.WriteLine();
    }
}