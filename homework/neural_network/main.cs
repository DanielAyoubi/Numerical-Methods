using System;
using static System.Math;
using static System.Console;

public class main{
    public static void Main(){
        int datapoints = 1000;
        int neurons=10;
        int trainingdata = 50;

        Func<double, double> fx = x => Cos(5*x-1)*Exp(-x*x);
        Func<double, double> dfdx = x => -( 5.0*Sin(5.0*x-1.0) + 2.0*x*Cos(5.0*x-1.0))*Exp(-x*x);
        Func<double, double> ddfdx = x => ( 20.0*x*Sin(5.0*x-1.0) + (4.0*x*x-27.0)*Cos(5.0*x-1.0))*Exp(-x*x);
       
        vector x_train = new vector(trainingdata);
        vector y_train = new vector(trainingdata);

        Console.WriteLine($"Artificial neural network trained to predict on Cos(5x-1)*exp(-x²) with {neurons} neurons, {trainingdata} training points");
        // Generate points for training
        for(int i = 0; i < trainingdata; i++){
            x_train[i] = -1 + 2.0 * i / (trainingdata - 1);
            y_train[i] = fx(x_train[i]);
        }

        var network = new ann(neurons);
        network.train(x_train,y_train);

        WriteLine("Part A: Predicting values Cos(5x-1)*exp(-x²) in range [-1:1]");
        for (double i = -10.0; i <= 10.0; i++) {
            double xi = i / 10.0;
            Console.WriteLine($"x: {xi}    f(x): {network.response(xi)}");
        }

        WriteLine("Part B: NNplot.svg have been made comparing NN prediction with analytical results");
        using (System.IO.StreamWriter s = new System.IO.StreamWriter("NN.data")){
            // NN predict on Cos(5x-1)*Exp(-x*x)
            for(int i = 0; i < datapoints; i++){
                double x_i = -1 + i * 2.0/(1000-1);
                double y_i = network.response(x_i);
                double analytical_y = fx(x_i);
                s.WriteLine($"{x_i} \t {y_i} \t {analytical_y}");
            }
            s.WriteLine("");
            s.WriteLine("");


            // NN predict on derivative of Cos(5x-1)*Exp(-x*x);
            for(int i = 0; i < datapoints; i++){
                double x_i = -1 + i * 2.0/(1000-1);
                double y_i = network.derivative(x_i);
                double analytical_y = dfdx(x_i);
                s.WriteLine($"{x_i} \t {y_i} \t {analytical_y}");
            }
            s.WriteLine("");
            s.WriteLine("");


            // NN predict on second derivative of Cos(5x-1)*Exp(-x*x);
            for(int i = 0; i < datapoints; i++){
                double x_i = -1 + i * 2.0/(1000-1);
                double y_i = network.second_derivative(x_i);
                double analytical_y = ddfdx(x_i);
                s.WriteLine($"{x_i} \t {y_i} \t {analytical_y}");
            }
            s.WriteLine("");
            s.WriteLine("");

            // NN predict on anti-derivative
            for(int i = 0; i < datapoints; i++){
                double x_i = -1 + i * 2.0/(1000-1);
                double y_i = network.antiderivative(x_i);
                s.WriteLine($"{x_i} \t {y_i}");
            }
        }
        
    }
}