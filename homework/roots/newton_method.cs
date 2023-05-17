using System;
using static System.Math;

public static class NewtonMethod {
    public static vector newton(Func<vector, vector> f, vector x, double eps = 1e-6) {
        int n = x.size;
        double lambda = 1.0;
        vector fx = new vector(n);
        vector deltax = new vector(n);
        matrix J = new matrix(n, n);

        do {
            fx = f(x);
            if (fx.norm() < eps) break;

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++) {
                    double dx = Abs(x[j]) * Pow(2, -26);
                    vector x_plus = x.copy();
                    x_plus[j] += dx;
                    J[i, j] = (f(x_plus)[i] - fx[i]) / dx;
                }
            }

            QRGS.decomp(J);
            deltax = QRGS.solve(J, -fx);
            if (deltax.norm() < Pow(2, -26)) break;

            lambda = 1.0;
            while (f(x + lambda * deltax).norm() > (1.0 - lambda / 2.0) * fx.norm() && lambda >= Pow(2, -26)) {
                lambda /= 2;
            }

            x += lambda * deltax;
        } while (fx.norm() > eps);

        return x;
    }
}

