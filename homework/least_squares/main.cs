using System;
using static System.Console;
using static System.Math;
using System.IO;

public class main{ 
	public static (vector, matrix, vector) lsfit(Func<double, double>[] fs, vector x, vector y, vector dy){
		int n = x.size; 
		int m = fs.Length;
		var A = new matrix(n,m);
		var b = new vector(n);
		for(int i = 0; i < n; i++){
			b[i] = y[i] / dy[i];
			for(int k = 0; k<m; k++) A[i,k] = fs[k](x[i])/dy[i]; 
		}
	vector c = QRGS.solve(A, b);
	var pinvA = QRGS.inverse(A);
	var S = pinvA*pinvA.transpose();

	vector uncertainties = new vector(m);
	for (int i = 0; i < S.size1; i++){
		uncertainties[i] = Sqrt(S[i,i]);
	}
	return (c, S, uncertainties);
	}


public static double fittedvalue(double x, vector c, vector dc, Func<double, double>[] fs, bool add){
    double fittedValue = 0.0;
    for (int k = 0; k < c.size; k++){
        double coefficient;
        if (add){
            coefficient = c[k] + dc[k];
        }
        else{
            coefficient = c[k] - dc[k];
        }
        fittedValue += coefficient * fs[k](x);
    }
    return fittedValue;
}
 // fittedvalue


	public static void Main(){
		#pragma warning disable CS0219
		double[] t = new double[] { 1, 2, 3, 4, 6, 9, 10, 13, 15 };
		double[] y = new double[] { 117, 100, 88, 72, 53, 29.5, 25.2, 15.2, 11.1};
		double[] deltaY = new double[] { 5, 5, 5, 4, 4, 3, 3, 2, 2 };
		vector x = new vector(t);
		vector yVec = new vector(y);
		vector dy = new vector(deltaY);
		vector lnY = new vector(yVec.size);
		vector dLnY = new vector(dy.size);

		for(int i = 0; i < yVec.size; i++){
			lnY[i] = Log(yVec[i]);
			dLnY[i] = dy[i] / yVec[i];
		}

	var fs = new Func<double, double>[] { z => 1.0, z => -z };

    (vector coefficients, matrix covariance, vector uncertainties) = lsfit(fs, x, lnY, dLnY);

    double lnA = coefficients[0];
    double lambda = coefficients[1];
    double T_half = Math.Log(2) / lambda;
	double delta_lambda = uncertainties[1];
	double delta_T_half = Abs((-Log(2) / (lambda * lambda)) * delta_lambda);

    Console.WriteLine($"ln(a) = {lnA}, λ = {lambda}");
    Console.WriteLine($"Half-life time, T½ = {T_half} ± {delta_T_half} days");


	var outfile = new StreamWriter("values.data");
	var outfile_uncertainty = new StreamWriter("uncertainties.data");


	for(int i = 0; i < x.size; i++){
		double fitted_value = Exp(lnA - lambda * t[i]);
		outfile.WriteLine($"{t[i]} {y[i]} {deltaY[i]} {fitted_value}");

		double fitted_value_add = fittedvalue(t[i], coefficients, uncertainties, fs, true);
        double fitted_value_sub = fittedvalue(t[i], coefficients, uncertainties, fs, false);
        outfile_uncertainty.WriteLine($"{t[i]} {fitted_value + fitted_value_add} {fitted_value - 	fitted_value_sub}");
	}
	outfile.Close();
	outfile_uncertainty.Close();
	
	double modern_half_life = 3.63;
    Console.WriteLine($"Modern half-life time value for 224Ra: {modern_half_life} days");
    Console.WriteLine($"Difference: {Math.Abs(T_half - modern_half_life)} days");

    bool withinUncertainty = Math.Abs(T_half - modern_half_life) <= delta_T_half;
    Console.WriteLine($"Agrees with modern value within the estimated uncertainty: {withinUncertainty}");
	
	}

}//classmain
