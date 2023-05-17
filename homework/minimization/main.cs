using System;
using static System.Console;
using static System.Math;
using static minimize;
using System.IO;

class program{
    static void Main(){
        Func<vector, double> rosenbrock = x => Pow(1 - x[0], 2) + 100 * Pow(x[1] - Pow(x[0], 2), 2);
        vector start_rosenbrock = new vector(-1.2, 1.1);
        var  (min_rosenbrock, steps_rosenbrock) = qnewton(rosenbrock, start_rosenbrock);
        Console.WriteLine($"Minimum of the Rosenbrocks's valley function with initial (x,y) = ({start_rosenbrock[0]}, {start_rosenbrock[1]}) is found at x = {min_rosenbrock[0]}, y = {min_rosenbrock[1]} {steps_rosenbrock} steps taken to minimum");

        Console.WriteLine("---------------------------------------------------------------------------------------------------------");

        Func<vector, double> himmelblau = x => Pow( Pow(x[0], 2) + x[1] - 11.0, 2) + Pow(x[0] + Pow(x[1], 2) - 7.0, 2);
        vector start_himmelblau = new vector(-0.5, 1.5);
        var  (min_himmelblau, steps_himmelblau) = qnewton(himmelblau, start_himmelblau);
        Console.WriteLine($"Minimum of the Himmelblau's function with initial (x,y) = ({start_himmelblau[0]}, {start_himmelblau[1]}) is found at x = {min_himmelblau[0]}, y = {min_himmelblau[1]} {steps_himmelblau} steps taken to minimum");

        Console.WriteLine("---------------------------------------------------------------------------------------------------------");

        var energy = new genlist<double>();
        var signal = new genlist<double>();
        var error  = new genlist<double>();
        var separators = new char[] {' ','\t'};
        var options = StringSplitOptions.RemoveEmptyEntries;
        do{
            string line=Console.In.ReadLine();
            if(line==null)break;
            string[] words=line.Split(separators,options);
            energy.add(double.Parse(words[0]));
            signal.add(double.Parse(words[1]));
            error .add(double.Parse(words[2]));
        } while(true);

        Func<vector, double> D = p => deviation(p, energy, signal, error);
        vector start = new vector(120.0, 12.0, 15.0);
        var (min, steps) = qnewton(D, start);
        Console.WriteLine($"Mass of Higgs boson converged to {min[0]} GeV/c², the experimental width to {min[1]} and the scale-factor to {min[2]} in {steps} steps.");

        using (StreamWriter sw = new StreamWriter("fitted.data")) {
            double startE = 100;
            double endE = 160;
            double stepsize = (endE - startE) / 2000;
            for(double E = startE; E <= endE; E+=stepsize) {
                double fittedSignal = breit_wigner(E, min[2], min[0], min[1]);
                sw.WriteLine($"{E} {fittedSignal}");
            }
        }



    } // Main
} // program
