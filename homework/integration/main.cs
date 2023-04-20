using System;
using static System.Console;
using static System.Math;
using static adaptiveintegrator;

class main{
	public static void Main()
    {
		double integral1 = Integrate(x => Sqrt(x), 0, 1);
        double integral2 = Integrate(x => 1 / Sqrt(x), 0, 1);
        double integral3 = Integrate(x => 4 * Sqrt(1 - x * x), 0, 1);
        double integral4 = Integrate(x => Log(x) / Sqrt(x), 0, 1);

        double delta = 0.001;
        double epsilon = 0.001;

        Console.WriteLine("∫01 dx √(x) = 2/3:");
        Console.WriteLine($"Result: {integral1}, Accuracy goal met: {Abs(integral1 - 2.0 / 3.0) <= delta + epsilon * Abs(integral1)}");

        Console.WriteLine("∫01 dx 1/√(x) = 2:");
        Console.WriteLine($"Result: {integral2}, Accuracy goal met: {Abs(integral2 - 2.0) <= delta + epsilon * Abs(integral2)}");

        Console.WriteLine("∫01 dx 4√(1-x²) = π:");
        Console.WriteLine($"Result: {integral3}, Accuracy goal met: {Abs(integral3 - PI) <= delta + epsilon * Abs(integral3)}");

        Console.WriteLine("∫01 dx ln(x)/√(x) = -4:");
        Console.WriteLine($"Result: {integral4}, Accuracy goal met: {Abs(integral4 + 4.0) <= delta + epsilon * Abs(integral4)}");

		Console.WriteLine("Error function for z = 0.5:");
        Console.WriteLine(Erf(0.5));	

		using( var s = new System.IO.StreamWriter("erf.data")) {
		for (double i = -3; i <= 3; i += 0.01)
		{
            double adaptiveErf = Erf(i);
			s.WriteLine($"{i}\t{adaptiveErf}");
		}

		Console.WriteLine("∫01 dx 1/√(x) using Clenshaw-Curtis transformation:");
        Console.WriteLine(Integrate_CC(x => 1 / Sqrt(x), 0, 1));

        Console.WriteLine("∫01 dx ln(x)/√(x) using Clenshaw-Curtis transformation:");
        Console.WriteLine(Integrate_CC(x => Log(x) / Sqrt(x), 0, 1));
		}
    }

     
}

