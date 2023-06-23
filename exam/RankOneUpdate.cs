using System;
using static System.Console;
using static System.Math;

public static class program {
    public static vector RankOneUpdate(matrix D, vector u, double sigma){
        if (D.size1 != D.size2) {
        throw new ArgumentException("Input matrix D for RankOneUpdate must be square");
        }

        int n = D.size1;
        vector d = new vector(n);
        vector lambda = new vector(n);

        for (int i = 0; i < n; i++) {
            d[i] = D[i, i];
        }

        if (sigma == 0) {
            return d; // in case of σ=0 the eigenvalues do not change and just return diagonal elements
        }

        Func<vector, vector> f = (x) => {
            vector fx = new vector(x.size);
            for (int i = 0; i < x.size; i++) {
                fx[i] = SecularEquation(u, d, x[i], sigma);
            }
            return fx;
        };

        vector lambda_guess = new vector(n);
        double uTu = 0;
        for (int i = 0; i < n; i++) {
            uTu += u[i]*u[i];
        }
        
        if (sigma > 0) {
            for (int i = 0; i < n-1; i++) {
                lambda_guess[i] = (d[i] + d[i+1])/2.0;
            }
            lambda_guess[n-1] = d[n-1] + sigma*uTu/2.0;
        } else {
            lambda_guess[0] = d[0] + sigma*uTu/2.0;
            for (int i = 1; i < n; i++) {
                lambda_guess[i] = (d[i-1] + d[i])/2.0;
            }
        }
        
        lambda = NewtonMethod.newton(f, lambda_guess);

        return lambda;
    }

    public static double SecularEquation(vector u, vector d, double lambda, double sigma) {
        double sum = 0;
        double eps = 1e-9;

        for (int i = 0; i < u.size; i++) {
            if (u[i] < eps && Abs(d[i] - lambda) < eps) continue;
            else sum += sigma * u[i] * u[i] / (d[i] - lambda);
            
        }
        return 1 + sum;
    }
}
