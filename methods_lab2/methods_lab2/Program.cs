using System;

class JacobiSolver
{
    static double[,] A = {
            { 0.51, -0.074, 0.01, -0.13, 0.09 },
            { 0.08, 0.3, -0.036, 0, 0.05 },
            { 0.15, 0, 0.42, 0.06, -0.07 },
            { 0.19, 0.023, 0.06, 0.438, 0 },
            { 0.05, -0.07, 0.023, 0, 0.36 } };

    static double[] b = { 1.502, -1.898, -1.38, 4.726, -0.431 };
    static double[] x = new double[] { 100, 200, 300 ,400 ,500 };
    static int maxIterations = 100;
    static double epsilon = 0.00001;

    static void Main()
    {
        Solve();
        Console.WriteLine("Residual vector:");
        double[] r = subtract(multiply(A, x), b);
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine("r[" + i + "] = " + r[i]);
        }
    }
    static void Solve()
    {
        int n = b.Length;
        double[] xNew = new double[n];
        double error = epsilon + 1;
        int iteration = 0;

        while (error > epsilon && iteration < maxIterations)
        {
            for (int i = 0; i < n; i++)
            {
                double sum = 0;

                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        sum += A[i, j] * x[j];
                    }
                }

                xNew[i] = (b[i] - sum) / A[i, i];
            }

            error = 0;

            for (int i = 0; i < n; i++)
            {
                double diff = Math.Abs(xNew[i] - x[i]);

                if (diff > error)
                {
                    error = diff;
                }

                x[i] = xNew[i];
            }

            iteration++;
        }

        Console.WriteLine("Solution:");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine("x[{0}] = {1}", i, x[i]);
        }
    }
    static double[] subtract(double[] a, double[] b)
    {
        double[] c = new double[a.Length];
        for (int i = 0; i < a.Length; i++)
        {
            c[i] = a[i] - b[i];
        }
        return c;
    }
    static double[] multiply(double[,] A, double[] x)
    {
        int n = x.Length;
        double[] b = new double[n];
        for (int i = 0; i < n; i++)
        {
            double s = 0;
            for (int j = 0; j < n; j++)
            {
                s += A[i, j] * x[j];
            }
            b[i] = s;
        }
        return b;
    }
}