using System;

public class qspline{
    public double[] x, y, b, c;

    public qspline(double[] xs, double[] ys){
        int n = xs.Length;
        x = xs;
        y = ys;
        b = new double[n - 1];
        c = new double[n - 1];

        double[] p = new double[n - 1];
        double[] h = new double[n - 1];

        for (int i = 0; i < n - 1; i++){
            h[i] = x[i + 1] - x[i];
            p[i] = (y[i + 1] - y[i]) / h[i];
        }

        c[0] = 0;

        for (int i = 0; i < n - 2; i++){
            c[i + 1] = (p[i + 1] - p[i] - c[i] * h[i]) / h[i + 1];
        }

        c[n - 2] /= 2;

        for (int i = n - 3; i >= 0; i--){
            c[i] = (p[i + 1] - p[i] - c[i + 1] * h[i + 1]) / h[i];
        }

        for (int i = 0; i < n - 1; i++){
            b[i] = p[i] - c[i] * h[i];
        }
    }


    public double evaluate(double z){
        int i = binsearch(x, z);
        double dx = z - x[i];

        return y[i] + b[i] * dx + c[i] * dx * dx;
    }

    public double derivative(double z){
        int i = binsearch(x, z);
        double dx = z - x[i];

        return b[i] + 2 * c[i] * dx;
    }

    public double integral(double z){
        int i = binsearch(x, z);
        double integral = 0.0;

        for (int j = 0; j < i; j++) {
            double dx = x[j+1] - x[j];
            integral += y[j] * dx + 0.5 * b[j] * dx * dx + (1.0/3.0) * c[j] * dx * dx * dx;
        }
        double dxLast = z - x[i];
        integral += y[i] * dxLast + 0.5 * b[i] * dxLast * dxLast + (1.0/3.0) * c[i] * dxLast * dxLast * dxLast;

        return integral;
    }

    private int binsearch(double[] x, double z){
        int i = 0, j = x.Length - 1;
        while (j - i > 1) {
            int mid = (i + j) / 2;
            if (z > x[mid]) i = mid;
            else j = mid;
        }
        return i;
    }
}
