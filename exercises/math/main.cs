using System;
using static System.Console;
using static System.Math;
public static class Program{
        public static void Main(){
            double sqrt2 = Math.Sqrt(2.0);
            Write($"sqrt2^2 = {sqrt2*sqrt2} (should equal 2)\n");
            double num =(double)1/5;
            double number1 = Math.Pow(2,num);
            WriteLine($"2 to the power of 1/5 = {number1}");
            double exppi = Math.Exp(Math.PI);
            WriteLine($"e to the power of pi is = {exppi}");
            double piexp = Math.Pow(Math.PI,Math.E);
            WriteLine($"Pi to the power of e = {piexp}");
            
         
            double gamma0 = sfuns.lngamma(0);
            double gamma1 = sfuns.lngamma(1);
            double gamma2 = sfuns.lngamma(2);
            double gamma3 = sfuns.lngamma(3);
            double gamma10 = sfuns.lngamma(10);


        Console.WriteLine($"{gamma0} {gamma1} {gamma2} {gamma3} {gamma10}");
        }
}
