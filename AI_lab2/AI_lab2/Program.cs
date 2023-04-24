

using ScottPlot;
using ScottPlot.Palettes;
using System;
using System.Security;

double PowerFunction(double x, double a)
{
    return Math.Pow(a, x);
}
double ExponentialFunction(double x)
{
    return Math.Pow(Math.Exp(x), 2);
}
double RectangleMethod(double a, double b, int n) // Метод прямокутників
{
    double h = (b - a) / n; // крок сітки
    double result = 0;

    for (int i = 0; i < n; i++)
    {
        double x = a + i * h; // Точка розбиття
        result += ExponentialFunction(x + h / 2) * h; // Площа прмокутника
    }

    return result;
}
double MonteCarloMethod(double a, double b, int n)
{
    Random random = new Random();
    double funcSum = 0;

    for(int i = 0; i < n; i++)
    {
        double randomFunc = random.NextDouble() * (b - a) + a;
        funcSum += ExponentialFunction(randomFunc);

    }
    return (funcSum / n) * (b - a);

}

double low = 1, high = 2;
int pointsAmount = 200;

double result = RectangleMethod(low, high, pointsAmount);
double monteCarloResult = MonteCarloMethod(low, high, pointsAmount);
double approximationError = Math.Abs(result - monteCarloResult);
double relativeError = (approximationError / result) * 100;

Console.WriteLine($"Rectangle method integral: {result}");

Console.WriteLine($"Monte Carlo method integral: {monteCarloResult}");

Console.WriteLine($"Approximation error: {approximationError}");

Console.WriteLine($"Relative error: {relativeError}" + "%");

var plt = new ScottPlot.Plot(600, 400);

// sample data
double[] x = new double[pointsAmount];
double lowP = 1;
double highP = 2;
double eps = lowP / pointsAmount;

for(int i = 0; i < pointsAmount; i++)
{
    x[i] = lowP;
    lowP += eps;
}
double[] y = new double[pointsAmount];
for (int i = 0; i < pointsAmount; i++)
{
    y[i] = ExponentialFunction(x[i]);
}

Random random = new Random();
double[] rands = new double[pointsAmount];
for (int i = 0; i < pointsAmount; i++)
{
    rands[i] = random.NextDouble() * (high - low) + low;
}

double[] func = new double[pointsAmount];

for (int i = 0; i < pointsAmount; i++)
{
    func[i] = ExponentialFunction(rands[i]);
}

// plot the data
plt.AddScatter(rands, func);
plt.AddScatter(x, y);


// customize the axis labels
plt.Title("Monte Carlo method");
plt.XLabel("Horizontal Axis");
plt.YLabel("Vertical Axis");

plt.SaveFig("montecarlo.png");




