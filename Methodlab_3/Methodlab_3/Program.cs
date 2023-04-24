double F(double x)
{
    return 2d * Math.Cos(x / 2d) - Math.Pow(x, 3) + 1d;
}
bool DifferentSign(double num1, double num2)
{
    return F(num1) < 0 && F(num2) > 0 || F(num1) > 0 && F(num2) < 0;
}


double eps = 0.0001;
double ep2 = 0.00000001;

double divideMethod = DivideMethod(eps);

Console.WriteLine("root(divide method): " + divideMethod);
Console.WriteLine("root(simple iteration): " + SimpleIteration(ep2 , divideMethod));

double DivideMethod(double eps)
{
    double low = 1, high = 1.5;
    double mid = 0;

    int iter = 0;
    if (DifferentSign(low, high))
    {
        while ((high - low) > eps)
        {
            iter++;
            mid = (low + high) / 2;
            if (DifferentSign(mid, high))
            {
                low = mid;
            }
            else if (DifferentSign(mid, low))
            {
                high = mid;
            }
            Console.WriteLine("low: " + low + " .mid: " + mid + " .high: " + high);
        }
    }
    Console.WriteLine("iterations(divide method): " + iter);
    Console.WriteLine("function result(divide method): " + F(mid));
    return mid;
}
double SimpleIteration(double eps, double rootA)
{
    double root = rootA;
    double rootPrev = 0;
    int iter = 0;
    while(Math.Abs(root - rootPrev) > eps)
    {
        Console.WriteLine(iter + ": " + root);
        iter++;
        rootPrev = root;
        root = Math.Pow(2 * Math.Cos(root / 2) + 1, 1.0 / 3.0);
    }
    Console.WriteLine("iterations(simple iteration): " + iter);
    Console.WriteLine("function result(simple iteration): " + F(root));
    return root;
}