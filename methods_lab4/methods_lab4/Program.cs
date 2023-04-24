using Extreme.Mathematics;


Func<double, double> f = x => 2d * Math.Cos(x / 2d) - Math.Pow(x, 3) + 1d;
double x = 1;
double x_prev = 0;
double eps = 0.0001;

while(Math.Abs(x - x_prev) > eps)
{
    Console.WriteLine(x);
    double derivative = f.CentralDerivative(x);
    x_prev = x;
    x = x - f(x) / derivative;
}

Console.WriteLine(x);

