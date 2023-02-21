using System;
using static System.Console;
using static System.Math;
using System.Numerics;

class Program{

    public static void Main(){

  		/* Complex imag = Complex.Sqrt(-1); */
		/* Complex I = Complex.Sqrt(imag); */
		/* Complex E_i_pi = Complex.Exp(imag * Math.PI); */
        /* Complex E_I = Complex.Exp(imag); */

		complex i = cmath.sqrt(new complex(-1,0));     // √-1

		complex sqrt_i = cmath.sqrt(i);  // √i
        		
		complex e_i = cmath.exp(i);      // e^i
		
		complex e_i_pi = cmath.exp(i * Math.PI);   // e^iπ
		
		complex i_i = cmath.pow(i, i);

		complex ln_i = cmath.log(i);

		complex sin_i = cmath.sin(i * Math.PI);


		WriteLine($"√-1 = {i} \n√i = {sqrt_i} \ne^i = {e_i}  \ne^iπ = {e_i_pi} \ni^i = {i_i} \nln(i) = {ln_i} \nsin(iπ) = {sin_i}");
	
		WriteLine(complex.approx(Math.Sqrt(-1), i));
    }
}
