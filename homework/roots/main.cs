using System;
using static System.Console;
using static System.Math;
using System.IO;
using static NewtonMethod;

class program {  
    public static vector f(vector x) {
        double dfdx = -2.0 * (1.0 - x[0]) - 400.0 * x[0] * (x[1] - x[0] * x[0]);
        double dfdy = 200.0 * (x[1] - x[0] * x[0]);
        return new vector(new double[] { dfdx, dfdy });
    }

    public static vector f2(vector x) {
        double fx_val = Pow(x[0], 3) - 6 * Pow(x[0], 2) + 11 * x[0] - 6;
        vector fx = new vector(fx_val);
        return fx;        
    }

    public static System.Func<double, vector, vector> hydrogen(double E) {
        return (double r, vector y) =>
        {
            double dev1 = y[1];
            double dev2 = -2 * (E + 1 / r) * y[0];
            return new vector(dev1, dev2);
        };
    }

    public static System.Func<vector, vector> M(double rmin, double rmax, double accuracy, double epsilon) {
        vector f0 = new vector(rmin - rmin * rmin, 1 - 2 * rmin);
        return delegate(vector E) {
            var (xlist, ylist) = ODE.improved_driver(hydrogen(E[0]), rmin, f0, rmax, acc:accuracy, eps:epsilon);
            vector v = new vector(ylist[0][0]);
            return v;
        };
    }

    static void Main() {
        // Newton method back-tracking linesearch - Rosenbrock and polynomial
        vector x0 = new vector(0.1, 0.1);
        vector root = newton(f, x0);
        Console.WriteLine("Expected roots: (1,1)");
        Console.WriteLine($"Extremums of Rosenbrock's valley found at: ({root[0]}, {root[1]})");

        Console.WriteLine("Expected roots for polynomial f(x) = x³-6x²+11x-6 -> (1, 2, 3)");
        vector[] points = new vector[3];
        for (int i = 0; i < points.Length; i++) {
            double val = 0.1 + 2 * i;
            points[i] = new vector(val);
            vector root_pol = newton(f2, points[i]);
            Console.WriteLine($"Root {i + 1}: {root_pol[0]}");
        }

        // Bound state hydrogen atom 
        //double rmin = 1e-3;
        double rmin = 0.001;
        double rmax = 8;
        vector Eguess = new vector (-0.8);
        double eps = 1e-2;
        double acc = 1e-2;
        double h = 0.01;

        vector E0 = newton(M(rmin, rmax, acc, eps), Eguess);
        Console.WriteLine($"Lowest root (energy): E0 = {E0[0]} hartree. Should be equal to -0.5 hartree");

        Console.WriteLine($"Plots are made with starting guess of E = {Eguess[0]} hartree");


        vector y0 = new vector(rmin - rmin * rmin, 1 - 2 * rmin);
        var xlist = new genlist<double>();
        var ylist = new genlist<vector>();
        var (x_list, y_list) = ODE.improved_driver(F: hydrogen(E0[0]), a:rmin, ya:y0, b:rmax, acc:acc, eps:eps, h:h, xlist:xlist, ylist:ylist);

        using (StreamWriter sw = new StreamWriter("hydrogen.data")) {
            for (int i = 0; i < x_list.size; i++) {
                double r = x_list[i];
                vector y = y_list[i];
                sw.WriteLine($"{r} {y[0]}");
                
            }
        }

        using (StreamWriter sw = new StreamWriter("convergence.data")) {
            // Vary rmin
            rmax = 10; 
            for (rmin = 1; rmin >= 1e-6; rmin /= 2) {
                E0 = newton(M(rmin, rmax, acc, eps), Eguess);
                sw.WriteLine($"{rmin} {E0[0]}");
            }
                sw.WriteLine("");
                sw.WriteLine("");

            // Vary rmax
            rmin = 1e-3;
            for (rmax = 1; rmax <= 10; rmax += 0.5) {
                E0 = newton(M(rmin, rmax, acc, eps), Eguess);
                sw.WriteLine($"{rmax} {E0[0]}");
            }
                sw.WriteLine("");
                sw.WriteLine("");
            
            // Vary acc
            rmin = 1e-3;
            rmax = 10; 
            eps = 1e-6; 
            for (acc = 1e-3; acc <= 1e-1; acc *= 1.1) {
                E0 = newton(M(rmin, rmax, acc, eps), Eguess);
                sw.WriteLine($"{acc} {E0[0]}");
            }
                sw.WriteLine("");
                sw.WriteLine("");

            // Vary eps
            acc = 1e-3;
            for (eps = 1e-3; eps <= 1e-1; eps *= 1.2) {
                E0 = newton(M(rmin, rmax, acc, eps), Eguess);
                sw.WriteLine($"{eps} {E0[0]}");
            }
        }
    } // 

} // program




