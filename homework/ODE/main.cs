using System;
using static System.Console;
using static ODE;

public class main {
    public static void Main() {
        Lotka_Volterra();


        Func<double, vector, vector> f = (x, y) => {
            double u = y[0];
            double v = y[1];
            return new vector(v, -u);
        };

        double a = 0; // start-point
        vector ya = new vector(1, 0); // initial conditions
        double b = 2 * Math.PI; // end-point
        double h = 0.001; // initial step-size
        double acc = 0.0001;
        double eps = 0.00001; 

        var (xlist, ylist) = driver(f, a, ya, b, h, acc, eps);

        for (int i = 0; i < xlist.size; i++) {
            WriteLine($"{xlist[i]}\t{ylist[i][0]}\t{ylist[i][1]}");
        }	

        WriteLine("");
        WriteLine("");

		// Damped harmonic oscillator
        double b_ = 0.25;
        double c_ = 5.0;

        Func<double, vector, vector> f_ = (t, y) => {
            double theta = y[0];
            double omega = y[1];
            return new vector(omega, -b_*omega - c_*Math.Sin(theta));
        };

        double a_ = 0;
        vector ya_ = new vector(Math.PI - 0.1, 0.0);
        double b_end = 10;
        double h_ = 0.001;
        double acc_ = 0.0001; 
        double eps_ = 0.00001; 

        var (tlist_, ylist_) = driver(f_, a_, ya_, b_end, h_, acc_, eps_);
          
        for (int i = 0; i < tlist_.size; i++) {
            WriteLine($"{tlist_[i]}\t{ylist_[i][0]}\t{ylist_[i][1]}");
        }

        WriteLine("");
        WriteLine("");

	}//Main	
    static void Lotka_Volterra() {
    Func<double, vector, vector> lotkavolterra = (t, z) => {
        double a = 1.5;
        double b = 1;
        double c = 3;
        double d = 1;
        double x = z[0];
        double y = z[1];
        return new vector(a * x - b * x * y, -c * y + d * x * y);
    };

    double t0 = 0;
    vector y0 = new vector(10, 5);
    double tEnd = 15;
    double h = 0.001;
    double acc = 0.0001;
    double eps = 0.00001;

    var tlist = new genlist<double>();
    var zlist = new genlist<vector>();
    ODE.improved_driver(lotkavolterra, t0, y0, tEnd, acc, eps, h, tlist, zlist);

    int n = 300;
    double[] t_dense = new double[n];
    vector[] z_dense = new vector[n];
    for (int i = 0; i < n; i++) {
        double t = i * 15.0 / (n - 1);
        t_dense[i] = t;

        int idx = 0;
        for (int j = 0; j < tlist.size - 1; j++) {
            if (tlist[j] <= t && tlist[j + 1] > t) {
                idx = j;
                break;
            }
        }

        double dt = t - tlist[idx];
        vector dz = zlist[idx + 1] - zlist[idx];
        z_dense[i] = zlist[idx] + dz * (dt / (tlist[idx + 1] - tlist[idx]));
    }

    using (var file = new System.IO.StreamWriter("lotka_volterra.data")) {
        for (int i = 0; i < t_dense.Length; i++) {
            file.WriteLine($"{t_dense[i]}\t{z_dense[i][0]}\t{z_dense[i][1]}");
        }
    }
}
}//classmain
