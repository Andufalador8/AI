double PowerFunction(double x, double a)
{
    return Math.Pow(a, x);
}
double ExponentialFunction(double x)
{
    return Math.Exp(x);
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
    int num = 5000;

    for(int i = 0; i < n; i++)
    {
        double randomFunc = random.NextDouble() * (b - a) + a;
        funcSum += ExponentialFunction(randomFunc);
    }
    return (funcSum / n) * (b - a);

}

double low = 1, high = 2;
double a = 2;
int pointsAmount = 5000;

double result = RectangleMethod(low, high, pointsAmount);
double monteCarloResult = MonteCarloMethod(low, high, pointsAmount);

Console.WriteLine($"Значення інтегралу: {result}");
Console.WriteLine($"Оцінка значень інтегралу: {monteCarloResult}");


