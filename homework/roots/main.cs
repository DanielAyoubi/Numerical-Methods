using System;
using static System.Console;
using static System.Math;
using System.IO;

class program {
    
    public static vector hydrogen(double r, vector y) {
        double E = y[2];
        vector dy = new vector(3);
        dy[0] = y[1];
        dy[1] = -2 * (E + 1 / r) * y[0];
        dy[2] = 0;
        return dy;
    }

        public static double shooting_method(double rmin, double rmax, double Eguess, double eps=1e-4, double acc=1e-4) {
            double E = Eguess;
            double dE = 0.1;
            double M;
            
            do {
                vector y0 = new vector(new double[] { rmin - rmin * rmin, 1 - 2 * rmin, E });
                var result = ODE.improved_driver(hydrogen, rmin, y0, rmax, acc, eps);
                M = result[result.Size - 1][0];

                if (M > 0) {
                    E += dE;
                } else {
                    E -= dE;
                    dE /= 2;
                }
            } while (Math.Abs(M) > eps);

            return E;
        }


    static void Main() {
        double rmin = 1e-6;
        double rmax = 8;
        double Eguess = -0.6;
        double eps = 1e-6;
        double acc = 1e-6;

        double E0 = shooting_method(rmin, rmax, Eguess, eps, acc);
        Console.WriteLine($"Lowest root: E0 = {E0}");

        vector y0 = new vector(new double[] { rmin - rmin * rmin, 1 - 2 * rmin, E0 });
        var solution = ODE.improved_driver(hydrogen, rmin, y0, rmax, acc, eps);

        using (StreamWriter sw = new StreamWriter("wave_function.data")) {
            for (int i = 0; i < solution.xlist.Size; i++) {
                double r = solution.xlist[i];
                vector y = solution.ylist[i];
                sw.WriteLine($"{r} {y[0]}");
            }
        }
    } // Main
} // program




    //     vector x0 = new vector(new double[] { 0.1, 0.1 });
    //     vector root = newton(f, x0);
    //     Console.WriteLine("Expected roots: (±1,1)");
    //     Console.WriteLine($"Extremums of Rosenbrock's valley found at: ({root[0]}, {root[1]})");

    //     Console.WriteLine("Expected roots for polynomial f(x) = x³-6x²+11x-6 -> (1, 2, 3)");
    //     vector[] points = new vector[3];
    //     for (int i = 0; i < points.Length; i++) {
    //         double val = 0.1 + 2 * i;
    //         points[i] = new vector(new double[] { val });
    //         vector root_pol = newton(f2, points[i]);
    //         Console.WriteLine($"Root {i + 1}: {root_pol[0]}");
    //     }

    // } // Main

    //     public static vector f(vector x) {
    //         double dfdx = -2.0 * (1.0 - x[0]) - 400.0 * x[0] * (x[1] - x[0] * x[0]);
    //         double dfdy = 200.0 * (x[1] - x[0] * x[0]);

    //     return new vector(new double[] { dfdx, dfdy });
    // }

    //     public static vector f2(vector x) {
    //         double fx_val = Pow(x[0], 3) - 6 * Pow(x[0], 2) + 11 * x[0] - 6;
    //         vector fx = new vector(fx_val);
    //         return fx;