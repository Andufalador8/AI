double[] xA = new double[10] { 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9 };
double[] yA = new double[10] { 0.00000, 0.09983, 0.19866, 0.29552, 0.38941, 0.47942, 0.56464, 0.64421, 0.71735, 0.78332 };

double[] xB = new double[10] { 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9 };
double[] yB = new double[10] { 2.00000, 1.95533, 0.82533, 0.62160, 0.36235, 0.07073, 0.77279, 0.49515, 0.26260, 0.09592 };

double[] xC = new double[10] { 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9 };
double[] yC = new double[10] { 0.00000, 0.22140, 0.49182, 0.82211, 1.22554, 1.71828, 2.32011, 3.05519, 3.95303, 5.04964 };


double[,] tableA = DividedDifferences(xA, yA);
double xi = 0.45;
double yiA = NewtonInterpolation(tableA, xA, xi);
Console.WriteLine("Функція А");
Console.WriteLine("f({0}) = {1}", xi, yiA);

double[,] tableB = DividedDifferences(xA, yA);
double xi = 0.45;
double yiB = NewtonInterpolation(tableA, xA, xi);
Console.WriteLine("Функція А");
Console.WriteLine("f({0}) = {1}", xi, yiA);

double[,] tableA = DividedDifferences(xA, yA);
double xi = 0.45;
double yiA = NewtonInterpolation(tableA, xA, xi);
Console.WriteLine("Функція А");
Console.WriteLine("f({0}) = {1}", xi, yiA);


static double[,] DividedDifferences(double[] x, double[] y)
{
    int n = x.Length;
    double[,] table = new double[n, n];

    // заповнюємо першу колонку таблиці розділених різниць
    for (int i = 0; i < n; i++)
    {
        table[i, 0] = y[i];
    }

    // обчислюємо розділені різниці
    for (int j = 1; j < n; j++)
    {
        for (int i = 0; i < n - j; i++)
        {
            table[i, j] = (table[i + 1, j - 1] - table[i, j - 1]) / (x[i + j] - x[i]);
        }
    }

    return table;
}

static double NewtonInterpolation(double[,] table, double[] x, double xi)
{
    int n = x.Length;
    double result = table[0, 0];
    double term = 1.0;

    for (int i = 1; i < n; i++)
    {
        term *= (xi - x[i - 1]);
        result += table[0, i] * term;
    }

    return result;
}

