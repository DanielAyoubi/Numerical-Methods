using System;
using static System.Math;

public static class minimize {
    public static (vector, int) qnewton(
        Func<vector,double> f, /* objective function */
        vector start, /* starting point */
        double acc=1e-2 /* accuracy goal, on exit |gradient| should be < acc */
    ) {
        vector x = start.copy();
        int n = x.size;
        matrix B = matrix.id(n);
        vector grad = gradient(f, x);
        int steps = 0;


        while (grad.norm() > acc) {
            vector d = -B * grad;  
            double lambda  = 1.0;
            double lambda_min = Pow(2, -15);

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

    public static (vector, int) downhill_simplex(Func<vector, double> f, matrix simplex, double acc=1e-6) {
        int n = simplex.size2;
        int maxsteps = 50000;
        int steps = 0;
        matrix p = simplex.copy();
        vector fx = new vector(n+1);

        for (int i = 0; i <= n; i++) {
            fx[i] = f(p[i]);
        }

        while (true) { 
            int highest = 0;
            int lowest = 0;
            for (int i = 0; i <= n; i++) {
                if (fx[i] < fx[lowest]) lowest = i;
                if (fx[i] > fx[highest]) highest = i;
            }

            vector centroid = new vector(n);
            for (int i = 0; i <= n; i++) {
                if (i != highest) centroid += p[i];
            }
            centroid /= n;

            vector reflected = centroid + (centroid - p[highest]);
            double f_reflected = f(reflected);
            if (f_reflected < fx[lowest]) {
                vector expanded = centroid + 2 * (reflected - centroid);
                double f_expanded = f(expanded);

                if (f_expanded < f_reflected) {
                    p[highest] = expanded;
                    fx[highest] = f_expanded;
                } else {
                    p[highest] = reflected;
                    fx[highest] = f_reflected;
                }
            } else {
                if (f_reflected < fx[highest]) {
                    p[highest] = reflected;
                    fx[highest] = f_reflected;
                } else {
                    vector contracted = centroid + 0.5 * (p[highest] - centroid);
                    double f_contracted = f(contracted);

                    if (f_contracted < fx[highest]) {
                        p[highest] = contracted;
                        fx[highest] = f_contracted;
                    } else {
                        for (int i = 0; i <= n; i++) {
                            if (i != lowest) {
                                p[i] = 0.5 * (p[i] + p[lowest]);
                                fx[i] = f(p[i]);
                            }
                        }
                    }
                }
            }

            steps++;
            if ((p[0] - p[highest]).norm() < acc || steps > maxsteps) {
                break;
            }
        }

        int min_index = 0;
        for (int i = 1; i <= n; i++) {
            if (fx[i] < fx[min_index]) min_index = i;
        }

        return (p[min_index], steps);
    }
}


