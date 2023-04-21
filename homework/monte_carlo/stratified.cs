using System;
using static System.Math;

public static class StratifiedSampling {
    static Random random = new Random();

    public static double strata(int dim, Func<vector, double> f, double[] a, double[] b, double acc, double eps, int n_reuse, double mean_reuse) {
        int N = 16 * dim;
        double V = 1;
        for (int k = 0; k < dim; k++) V *= b[k] - a[k];

        int[] n_left = new int[dim];
        int[] n_right = new int[dim];
        double[] x = new double[dim], mean_left = new double[dim], mean_right = new double[dim];
        double mean = 0;

        for (int i = 0; i < N; i++) {
            for (int k = 0; k < dim; k++) x[k] = a[k] + random.NextDouble() * (b[k] - a[k]);
            double fx = f(x);
            mean += fx;

            for (int k = 0; k < dim; k++) {
                if (x[k] > (a[k] + b[k]) / 2) { n_right[k]++; mean_right[k] += fx; }
                else { n_left[k]++; mean_left[k] += fx; }
            }
        }
        mean /= N;

        for (int k = 0; k < dim; k++) {
            mean_left[k] /= n_left[k];
            mean_right[k] /= n_right[k];
        }

        int kdiv = 0;
        double maxvar = 0;

        for (int k = 0; k < dim; k++) {
            double var = Abs(mean_right[k] - mean_left[k]);
            if (var > maxvar) { maxvar = var; kdiv = k; }
        }

        double integ = (mean * N + mean_reuse * n_reuse) / (N + n_reuse) * V;
        double error = Abs(mean_reuse - mean) * V;
        double toler = acc + Abs(integ) * eps;
        if (error < toler) return integ;

        double[] a2 = new double[dim], b2 = new double[dim];
        Array.Copy(a, a2, dim);
        Array.Copy(b, b2, dim);

        a2[kdiv] = (a[kdiv] + b[kdiv]) / 2;
        b2[kdiv] = (a[kdiv] + b[kdiv]) / 2;

        double integLeft = strata(dim, f, a, b2, acc / Sqrt(2), eps, n_left[kdiv], mean_left[kdiv]);
        double integRight = strata(dim, f, a2, b, acc / Sqrt(2), eps, n_right[kdiv], mean_right[kdiv]);
        return integLeft + integRight;
    }
}

