using System;
using static System.Console;
using static System.Math;
using System.Numerics;

class Program{

    public static void Main(){

		complex num = new complex(-1,0);
  		Complex imag = Complex.Sqrt(-1);

		complex i = cmath.sqrt(num);     // √-1
		Complex I = Complex.Sqrt(imag);

		complex sqrt_i = cmath.sqrt(i);  // √i
		
		complex e_i = cmath.exp(i);      // e^i
		Complex E_I = Complex.Exp(imag);
		
		complex e_i_pi = cmath.exp(i * Math.PI);   // e^iπ
		Complex E_i_pi = Complex.Exp(imag * Math.PI);
		
		complex i_i = cmath.pow(i, i);

		complex ln_i = cmath.log(i);

		complex sin_i = cmath.sin(i * Math.PI);


		WriteLine($"√-1 = {num} \n√i = {sqrt_i} \ne^i = {e_i}  \ne^iπ = {e_i_pi} \ni^i = {i_i} \nln(i) = {ln_i} \nsin(iπ) = {sin_i}");
	
		WriteLine(complex.approx(Math.Sqrt(-1), i));
		WriteLine(complex.approx(new complex(0,1), cmath.log(i)));
    }
}
