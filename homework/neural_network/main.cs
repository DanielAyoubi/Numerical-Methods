using System;
using static System.Console;
using static System.Math;
using System.IO;

class program{
    static void Main(){
        int datapoints= 5;
        vector x = new vector(datapoints);
        vector y = new vector(datapoints);

        Func<double, double> fx = xi => Cos(5*xi-1)*Exp(-xi*xi);

        for (int i = 0; i < datapoints; i++) {
            double xi = -1 + 2.0 * i / (datapoints - 1);
            x[i] = xi;
            y[i] = fx(xi);
        }

        ann network = new ann(5);
        network.train(x, y);

        Console.WriteLine("For f(x) = Cos(5x-1)*exp(-x²): ");
        Console.WriteLine("");
        for (int i = -10; i <= 10; i++)         {
            double xi = i / 10.0;
            Console.WriteLine($"x: {xi}    f(x): {network.response(xi)}");
        }       
        
    } // Main
} // program
