using System;

public class cspline
{
    public double[] x, y, b, c, d;

    public cspline(int n, double[] xs, double[] ys)
    {
        x = xs;
        y = ys;
        b = new double[n];
        c = new double[n - 1];
        d = new double[n - 1];

        double[] h = new double[n - 1];
        double[] p = new double[n - 1];

        for (int i = 0; i < n - 1; i++)
        {
            h[i] = x[i + 1] - x[i];
            p[i] = (y[i + 1] - y[i]) / h[i];
        }

        double[] D = new double[n];
        double[] Q = new double[n - 1];
        double[] B = new double[n];

        D[0] = D[n - 1] = 2;
        Q[0] = 1;
        B[0] = 3 * p[0];
        B[n - 1] = 3 * p[n - 2];

        for (int i = 1; i < n - 1; i++)
        {
            D[i] = 2 * h[i - 1] / h[i] + 2;
            Q[i] = h[i - 1] / h[i];
            B[i] = 3 * (p[i - 1] + p[i] * h[i - 1] / h[i]);
        }

        for (int i = 1; i < n; i++)
        {
            D[i] -= Q[i - 1] / D[i - 1];
            B[i] -= B[i - 1] / D[i - 1];
        }

        b[n - 1] = B[n - 1] / D[n - 1];

        for (int i = n - 2; i >= 0; i--)
            b[i] = (B[i] - Q[i] * b[i + 1]) / D[i];

        for (int i = 0; i < n - 1; i++)
        {
            c[i] = (-2 * b[i] - b[i + 1] + 3 * p[i]) / h[i];
            d[i] = (b[i] + b[i + 1] - 2 * p[i]) / (h[i] * h[i]);
        }
    }

    public double evaluate(double z)
    {
        int i = binsearch(x, z);
        double h = z - x[i];

        return y[i] + h * (b[i] + h * (c[i] + h * d[i]));
    }

    private int binsearch(double[] x, double z)
    {
        int i = 0, j = x.Length - 1;

        while (j - i > 1)
        {
            int mid = (i + j) / 2;
            if (z > x[mid]) i = mid;
            else j = mid;
        }
        return i;
    }
}
