using System;
using static System.Console;
using static MonteCarloIntegrator;
using static QuasiRandomSequences;


class main{
    static void Main(){
        Func<vector, double> unit_circle = v =>
        {
            double x = v[0];
            double y = v[1];
            if (x * x + y * y <= 1) {
                return 1.0;
            }
            else {
                return 0.0;
            }
        };

        vector a = new vector(-1, -1 );
        vector b = new vector( 1, 1 );

        using (var data = new System.IO.StreamWriter("errors.data"))
        {
            for (int n = 10; n <= 1000000; n *= 10)
            {
                var (result, error) = plainmc(unit_circle, a, b, n);
                var (result_QR, error_QR) = QuasiRandomMonteCarlo(unit_circle, a, b, n);

                double actualerror = Math.PI - result;
                double actualerror_QR = Math.PI - result_QR;
                data.WriteLine($"{n} {error} {actualerror} {error_QR} {actualerror_QR}");
            }
        }
    }
}
