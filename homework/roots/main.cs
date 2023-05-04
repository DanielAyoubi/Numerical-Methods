using System;
using static System.Console;
using static System.Math;
using static NewtonMethod;
using static ODE;

class program {
    static void Main() {
        vector x0 = new vector(new double[] { 0.1, 0.1 });
        vector root = newton(f, x0);
        Console.WriteLine("Expected roots: (±1,1)");
        Console.WriteLine($"Extremums of Rosenbrock's valley found at: ({root[0]}, {root[1]})");

        Console.WriteLine("Expected roots for polynomial f(x) = x³-6x²+11x-6 -> (1, 2, 3)");
        vector[] points = new vector[3];
        for (int i = 0; i < points.Length; i++) {
            double val = 0.1 + 2 * i;
            points[i] = new vector(new double[] { val });
            vector root_pol = newton(f2, points[i]);
            Console.WriteLine($"Root {i + 1}: {root_pol[0]}");
        }

    } // Main

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
} // program
