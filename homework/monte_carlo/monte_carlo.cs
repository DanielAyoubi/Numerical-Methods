using System;
using static System.Console;
using static System.Math;

public class MonteCarloIntegrator{
    public static (double,double) plainmc(Func<vector,double> f,vector a,vector b,int N){
        int dim=a.size; double V=1; for(int i=0;i<dim;i++) V*=b[i]-a[i];
        double sum=0, sum2=0;
        var x = new vector(dim);
        var rnd = new Random();
            for(int i=0;i<N;i++){
                    for(int k=0;k<dim;k++) {
                        x[k] = a[k]+rnd.NextDouble() * (b[k]-a[k]);
                    }
                    double fx=f(x);
                    sum += fx; 
                    sum2 += fx*fx;
            }

            double mean = sum/N;
            double sigma = Sqrt(sum2/N-mean*mean);
            var result = (mean*V,sigma*V/Sqrt(N));
            return result;
    }

    public static (double, double) QuasiRandomMonteCarlo(Func<vector, double> f, vector a, vector b, int N) {
        int dim = a.size;
        double V = 1;
        for (int i = 0; i < dim; i++) V *= b[i] - a[i];
        double sum = 0, sum2 = 0;
        var x = new vector(dim);

        for (int i = 0; i < N; i++) {
            QuasiRandomSequences.halton(i, dim, x);
            for (int k = 0; k < dim; k++){
                x[k] = a[k] + x[k] * (b[k] - a[k]);
            }
            double fx = f(x);
            sum += fx;
            sum2 += fx * fx;
        }

        double mean = sum / N;
        double sigma = Sqrt(sum2 / N - mean * mean);
        var result = (mean * V, sigma * V / Sqrt(N));
        return result;
    }
    
} // monte carlo