using System;
using static System.Console;
using static System.Math;
class main{

public static double fun_to_fit(double x){return 1+2*x+3*x*x;}

public static double Fc(Func<double,double>[] fs,vector c,double x){
   double s=0;
   for(int i=0;i<fs.Length;i++) s+=c[i]*fs[i](x);
   return s;
}

public static void Main(){
	int n = 10;
	var x  = new double[n];
	var y  = new double[n];
	var dy = new double[n];
	var rnd = new Random();
	double dx=1.0/4, a=-1+dx, b=1-dx;
	for(int i=0;i<n;i++){
		double xi=a+(b-a)*i/(n-1);
		x[i]=xi;
		y[i]=fun_to_fit(xi)+(rnd.NextDouble()-0.5);
		dy[i]=0.25+rnd.NextDouble()/2;
		WriteLine($"{x[i]} {y[i]} {dy[i]}");
		}
	Write("\n\n");
	var fs = new Func<double,double>[] { z => 1.0, z => z, z => z*z };
	(vector c, matrix S) = matlib.lsfit(fs,x,y,dy);
	var dc = new vector(fs.Length);
	for(int i=0;i<fs.Length;i++)dc[i]=Sqrt(S[i,i]);
	for(double z=a;z<=b;z+=dx/2) WriteLine($"{z} {Fc(fs,c,z)}");
	Write("\n\n");
	for(double z=a;z<=b;z+=dx/2) WriteLine($"{z} {Fc(fs,c+dc,z)}");
	Write("\n\n");
	for(double z=a;z<=b;z+=dx/2) WriteLine($"{z} {Fc(fs,c-dc,z)}");

	}//Main
}//main
