using System;
using static System.Math;

public class ann {
    int n;
    Func<double,double> f = x => x * Exp(-x * x);
    vector p;


    Func<double, double> df = x =>  -(2*x*x-1) * Exp(-x * x);
    Func<double, double> ddf = x =>  (4 * x * x * x - 6 * x) * Exp(-x * x);
    Func<double, double> intf = x => -0.5 * Exp(-x * x);

    public ann(int n) {
        this.n = n;
        p = new vector(3*n);
    }
    
    public double response(double x) {
        double sum = 0;
        for (int i = 0; i < n; i++)
        {
            double a = p[3*i];
            double b = p[3*i+1];
            double w = p[3*i+2];
            sum += f((x-a)/b)*w;
        }
        return sum;
    }

    public vector train(vector x, vector y){
        for(int i=0; i<n; i++) {
            p[i] = 0.5; 
            p[i+n] = 0.5; 
            p[i+2*n] = x[0] + ( x[x.size-1] - x[0] )*i/(n-1);
        }

        Func<vector,double> cost = (v) => {
            this.p = v;
            double sum = 0;
            for (int k = 0; k < x.size; k++){
                double diff = response(x[k]) - y[k];
                sum += diff * diff;
            }
            return sum / x.size;
        };

        var (p_trained, _) = minimize.qnewton(cost, p);
        p = p_trained;
        return p;
    }

    public double derivative(double x) {
        double sum = 0;
        for (int i = 0; i < n; i++)
        {
            double a = p[3*i];
            double b = p[3*i+1];
            double w = p[3*i+2];
            sum += df((x-a)/b) * w / b;
        }
        return sum;
    }

    public double second_derivative(double x) {
        double sum = 0;
        for (int i = 0; i < n; i++)
        {
            double a = p[3*i];
            double b = p[3*i+1];
            double w = p[3*i+2];
            sum += ddf((x-a)/b) * w/b/b;
        }
        return sum;
    }

    public double antiderivative(double x) {
        double sum = 0;
        for (int i = 0; i < n; i++)
        {
            double a = p[3*i];
            double b = p[3*i+1];
            double w = p[3*i+2];
            sum += intf((x-a)/b) * w * b;
        }
        return sum;
    }


}
