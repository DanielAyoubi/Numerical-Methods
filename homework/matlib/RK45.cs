using System;
using static System.Math;
using static System.Console;


public class ODE{
    public static (vector, vector) rkstep45(
        Func<double, vector, vector> f, /* the f from dy/dx=f(x,y) */
        double x,                       /* the current value of the variable */
        vector y,                       /* the current value y(x) of the sought function */
        double h                        /* the step to be taken */
    ){
        vector k1 = f(x,y);
        vector k2 = f(x+h/4.0, y+h*(k1/4.0));
        vector k3 = f(x+h*3.0/8.0, y+h*(k1*3.0/32.0 + k2*9.0/32.0));
        vector k4 = f(x+h*12.0/13.0, y+h*(k1*1932.0/2197.0 - k2*7200.0/2197.0 + k3*7296.0/2197.0));
        vector k5 = f(x+h, y+h*(k1*439.0/216.0 - 8*k2 + k3*3680.0/523.0 - k4*845.0/4140.0));
        vector k6 = f(x+h/2.0, y+h*(-k1*8.0/27.0 + k2*2.0 - k3*3544.0/2565.0 + k4*1859.0/4104.0 - k5*11.0/40.0));

        vector yh = y + h*(k1*15.0/135.0 + k3*6656.0/12825.0 + k4*28561.0/56430.0 - k5*9.0/50.0 + k6*2.0/55.0);
        vector er = h * ( k1*(16.0/135.0 - 25.0/216.0) + k3*(6656.0/12825.0 - 1408.0/2565.0) + k4*(28561.0/56430.0 - 2197.0/4104.0) + k5*(-9.0/50.0+1.0/5.0) + k6*2.0/55.0);

        return (yh,er);
    }

    public static (genlist<double>,genlist<vector>) driver(
        Func<double,vector,vector> f, /* the f from dy/dx=f(x,y) */
        double a,                    /* the start-point a */
        vector ya,                   /* y(a) */
        double b,                    /* the end-point of the integration */
        double h=0.01,               /* initial step-size */
        double acc=0.001,             /* absolute accuracy goal */
        double eps=0.001              /* relative accuracy goal */
    ) {
        if(a>b) throw new ArgumentException("driver: a>b");
        double x=a; vector y=ya.copy();
        var xlist=new genlist<double>(); xlist.add(x);
        var ylist=new genlist<vector>(); ylist.add(y);
        do      {
                if(x>=b) return (xlist,ylist); /* job done */
                if(x+h>b) h=b-x;               /* last step should end at b */
                var (y_h,erv) = rkstep45(f,x,y,h);
                double tol = (acc+eps*y_h.norm()) * Sqrt(h/(b-a));
                double err = erv.norm();
                if(err<=tol){ // accept step
                x+=h; y=y_h;
                xlist.add(x);
                ylist.add(y);
                }
            h *= Min( Pow(tol/err,0.25)*0.95 , 2); // reajust stepsize
                }while(true);
    }//drive

    public static (genlist<double> xlist, genlist<vector> ylist) improved_driver(
    Func<double, vector, vector> F,
    double a, vector ya, double b,
    double acc = 1e-4, double eps = 1e-4, double h = 0.01,
    genlist<double> xlist = null,
    genlist<vector> ylist = null,
    double h_min = 1e-10, int max_retries = 100
    ) {
        vector y = ya;
        double x = a;

        while (true) {
            double[] tol = new double[y.size];
            double[] err = new double[y.size];

            if (x+h>b) h = b - x;

            if (x >= b) {
                if (xlist == null && ylist == null) {
                    xlist = new genlist<double>();
                    ylist = new genlist<vector>();
                    xlist.add(x);
                    ylist.add(y);
                    return (xlist, ylist);
                }
                else { return (xlist, ylist);}
            }

            var (yh, erv) = rkstep45(F, x, y, h);

            for (int i = 0; i < y.size; i++) {
                tol[i] = (acc + eps * Abs(yh[i])) * Sqrt(h / (b - a));
                err[i] = Abs(erv[i]);
            }

            bool ok = true;
            for (int i = 0; i < y.size; i++)
                if (!(err[i] < tol[i])) ok = false;

            int retries = 0;
            while (!ok && retries < max_retries) {
                h *= 0.5;
                if (h < h_min) {
                    h = h_min;
                    break;
                }

                (yh, erv) = rkstep45(F, x, y, h);

                for (int i = 0; i < y.size; i++) {
                    tol[i] = (acc + eps * Abs(yh[i])) * Sqrt(h / (b - a));
                    err[i] = Abs(erv[i]);
                    if (!(err[i] < tol[i])) ok = false;
                }

                retries++;
            }

            x += h;
            y = yh;
            if (xlist != null && ylist != null) {
                xlist.add(x);
                ylist.add(y);
            }

            double factor = tol[0] / err[0];
            for (int i = 1; i < y.size; i++) factor = Min(factor, tol[i] / err[i]);
            h *= Min(Pow(factor, 0.25) * 0.95, 2);
        }
    }
}


