using System;
using static System.Math;

public static class minimize {
    public static (vector, int) qnewton(
        Func<vector,double> f, /* objective function */
        vector start, /* starting point */
        double acc=1e-6 /* accuracy goal, on exit |gradient| should be < acc */
    ) {
        vector x = start.copy();
        int n = x.size;
        matrix B = matrix.id(n);
        vector grad = gradient(f, x);
        int steps = 0;


        while (grad.norm() > acc) {
            vector d = -B * grad;  
            double lambda  = 1.0;
            double lambda_min = Pow(2, -26);

            while (true) { 
                if (f(x + lambda * d) < f(x)) {
                    x += lambda * d;
                    vector y = gradient(f, x + lambda * d) - grad;
                    grad = gradient(f, x);
                    vector u = lambda * x - B * y;
                    double u_dot_y = u.dot(y);
                    if (Abs(u_dot_y) > acc) {
                        B = B + (matrix.outer(u,u) / u.dot(y));    
                    }        
                    break;
                }
                lambda = lambda/2.0;
                if (lambda < lambda_min) {
                    x = x + lambda * d;
                    grad = gradient(f, x);
                    B = matrix.id(n);
                    break;
                }
            } 
            steps++;
        }
        return (x, steps);
    }

    public static vector gradient(Func<vector, double> f, vector x, double h = 1e-7) {
        int n = x.size;
        vector grad = new vector(n);
        vector x_pos = x.copy();
        vector x_neg = x.copy();
        for (int i = 0; i < n; i++) {
            x_pos[i] += h;
            x_neg[i] -= h;
            grad[i] = (f(x_pos) - f(x_neg)) / (2.0 * h);
        }
        return grad;
    }
    
    public static double breit_wigner(double E, double A, double m, double gamma) {
    return A / (Pow(E - m, 2) + Pow(gamma, 2) / 4.0);
    }

    public static double deviation(vector p, genlist<double> energy, genlist<double> signal, genlist<double> error) {
        double m = p[0];
        double gamma = p[1];
        double A = p[2];
        double sum = 0.0;

        for(int i = 0; i < energy.size; i++)         {
            double E_i = energy[i];
            double sigma_i = signal[i];
            double delta_sigma_i = error[i];
            sum += Pow((breit_wigner(E_i, A, m, gamma) - sigma_i) / delta_sigma_i, 2);
        }
        return sum;
    }
}
