using System;
using static System.Math;

public class adaptiveintegrator
{
    public static double Integrate(
        Func<double, double> f, double a, double b, double acc = 0.0001, double eps = 0.0001, double f2 = double.NaN, double f3 = double.NaN) 
        {
        double h = b - a;
        if (double.IsNaN(f2)) {
            f2 = f(a + 2 * h / 6);
            f3 = f(a + 4 * h / 6);
        }
        double f1 = f(a + h / 6);
        double f4 = f(a + 5 * h / 6);
        double Q = (2 * f1 + f2 + f3 + 2 * f4) / 6 * (b - a);
        double q = (f1 + f2 + f3 + f4) / 4 * (b - a);
        double err = Abs(Q - q);
        if (err <= acc + eps * Abs(Q)) {
            return Q;
        }
        else {
            return Integrate(f, a, (a + b) / 2, acc / Sqrt(2), eps, f1, f2) +
                   Integrate(f, (a + b) / 2, b, acc / Sqrt(2), eps, f3, f4);
        }
    }

    public static double Integrate_CC(
        Func<double, double> f, double a, double b, double acc = 0.0001, double eps = 0.0001, double f2 = double.NaN, double f3 = double.NaN)
    {
        Func<double, double> transformedF = theta => f((a + b) / 2 + (b - a) / 2 * Cos(theta)) * Sin(theta) * (b - a) / 2;
        return Integrate(transformedF, 0, PI, acc, eps, f2, f3);
    }

    public static double Erf(double z)
    {
        double erf = 0;
        double sqrtPi = Sqrt(PI);
        if (z < 0) {
            erf = -Erf(-z);
        }
        else if (z <= 1) {
            Func<double, double> f = x => Exp(-x * x);
            erf = 2 / sqrtPi * Integrate(f, 0, z);
        }
        else {
            Func<double, double> f = t => Exp(-(z + (1 - t) / t) * (z + (1 - t) / t)) / (t * t);
            erf = 1 - 2 / sqrtPi * Integrate(f, 0, 1);
        }
        return erf;
    }

    public static (double, double) Integrate_err(
    Func<double, double> f, double a, double b, double acc = 0.0001, double eps = 0.0001, double f2 = double.NaN, double f3 = double.NaN)
    {
        double h = b - a;
        if (double.IsNaN(f2))
        {
            f2 = f(a + 2 * h / 6);
            f3 = f(a + 4 * h / 6);
        }
        double f1 = f(a + h / 6);
        double f4 = f(a + 5 * h / 6);
        double Q = (2 * f1 + f2 + f3 + 2 * f4) / 6 * (b - a);
        double q = (f1 + f2 + f3 + f4) / 4 * (b - a);
        double err = Abs(Q - q);
        double tolerance = acc + eps * Abs(Q);
        if (err < tolerance) 
        {
            return (Q, err);
        }
        else 
        {
            var (Q1, err1) = Integrate_err(f, a, (a + b) / 2, acc / Sqrt(2), eps, f1, f2);
            var (Q2, err2) = Integrate_err(f, (a + b) / 2, b, acc / Sqrt(2), eps, f3, f4);
            return (Q1 + Q2, err1 + err2);
        }
    }

    public static (double, double) IntegrateInfinite(Func<double, double> f, double a, double b, double acc = 0.0001, double eps = 0.0001) {
        if (double.IsNegativeInfinity(a) && double.IsPositiveInfinity(b))
        {
            Func<double, double> g = t => f(t / (1 - t * t)) * (1 + t * t) / ((1 - t * t) * (1 - t * t));
            return Integrate_err(g, -1, 1, acc, eps);
        }
        else if (double.IsNegativeInfinity(a))
        {
            Func<double, double> g = t => f(b - t / (1 + t)) * 1 / ((1 + t) * (1 + t));
            return Integrate_err(g, -1, 0, acc, eps);
        }
        else if (double.IsPositiveInfinity(b))
        {
            Func<double, double> g = t => f(a + t / (1 - t)) * 1 / ((1 - t) * (1 - t));
            return Integrate_err(g, 0, 1, acc, eps);
        }
        else
        {
            return Integrate_err(f, a, b, acc, eps);
        }
    }



}