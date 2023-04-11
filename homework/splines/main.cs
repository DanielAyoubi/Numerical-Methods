using System;
using static System.Console;
using static System.Math;
using System.IO;

public class main{
    public static void Main(){
        testQspline();

        int nPoints = 1000;
        int nInterpPoints = 15;
        double[] x = new double[nPoints];
        double[] y = new double[nPoints];
        double[] xInterp = new double[nInterpPoints];
        double[] yInterp = new double[nInterpPoints];

        // Create plot points
        for (int i = 0; i < nPoints; i++) {
            x[i] = (i * (8 * PI) / (nPoints - 1)) - (4 * PI);
            y[i] = Sin(x[i]);
        }

       // Create interpolation points
        for (int i = 0; i < nInterpPoints; i++) {
            if (i == nInterpPoints - 1) {
                xInterp[i] = x[nPoints - 1];
            } else {
                xInterp[i] = (i * (8 * PI) / (nInterpPoints - 1)) - (4 * PI);
            }
            yInterp[i] = Sin(xInterp[i]);
        }


        // Use splines class to interpolate and integrate
        double[] interpY = new double[nInterpPoints];
        double[] integY = new double[nInterpPoints];
        for (int i = 0; i < nInterpPoints; i++) {
            interpY[i] = linspline.linterp(xInterp, yInterp, xInterp[i]);
            integY[i] = linspline.linterpInteg(xInterp, yInterp, xInterp[i]);
        }


        // Write data to file
        using (StreamWriter sw = new StreamWriter("data_points.data")){
            for(int i = 0; i < nPoints; i++) {
                sw.WriteLine($"{x[i]}\t{y[i]}");
            }
            sw.WriteLine();
            sw.WriteLine();

            for(int i = 0; i < nInterpPoints; i++) {
                sw.WriteLine($"{xInterp[i]}\t{interpY[i]}\t{integY[i]}");
            }

            sw.WriteLine();
            sw.WriteLine();


            for(int i = 0; i < nInterpPoints; i++) {
                sw.WriteLine($"{xInterp[i]}\t{yInterp[i]}");
            }
        }
        using (StreamWriter sw = new StreamWriter("qspline.data")){

            qspline spline = new qspline(xInterp,yInterp);

            for (int i = 0; i < nPoints; i++) {
                double splineY = spline.evaluate(x[i]); 
                double splineDerivativeY = spline.derivative(x[i]);
                sw.WriteLine($"{x[i]}\t{splineY}\t{splineDerivativeY}");
            }
        }
    }


    public static void testQspline(){
        double[] x = new double[] {1, 2, 3, 4, 5};
        double[] y1 = new double[] {1, 1, 1, 1, 1};
        double[] y2 = new double[] {1, 2, 3, 4, 5};
        double[] y3 = new double[] {1, 4, 9, 16, 25};

        qspline s1 = new qspline(x, y1);
        qspline s2 = new qspline(x, y2);
        qspline s3 = new qspline(x, y3);

        Console.WriteLine("For table {xi=i, yi=1}:");
        PrintParameters(s1.b, s1.c);

        Console.WriteLine("\nFor table {xi=i, yi=xi}:");

        PrintParameters(s2.b, s2.c);

        Console.WriteLine("\nFor table {xi=i, yi=xi^2}:");

        PrintParameters(s3.b, s3.c);
    }

    private static void PrintParameters(double[] b, double[] c) {
        Console.WriteLine("Bi and Ci values:");

        for (int i = 0; i < b.Length; i++) {
            Console.WriteLine($"b[{i}] = {b[i]:0.000}, c[{i}] = {c[i]:0.000}");
        }
    }
}

