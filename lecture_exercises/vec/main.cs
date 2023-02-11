using System;
using static System.Console;
using static System.Math;
public static class main{


	public static void print(this double x,string s){ /* x.print("x="); */
		Write(s);WriteLine(x);
		}

	public static void print(this double x){ /* x.print() */
		x.print("");
	}
	public static void Main(){
		/*
		int n=9;
		double[] a = new double[n];
		for(int i=0;i<n;i++) Write($"a[{i}]={a[i]} ");
		WriteLine();
		for(int i=0;i<n;i++) a[i]=i;
		for(int i=0;i<n;i++) Write($"a[{i}]={a[i]} ");
		WriteLine();
		WriteLine($"array length = {a.Length}");
		foreach(double ai in a) Write($" ai = {ai} ");
		WriteLine();
		*/
		vec u = new vec(1,2,3);
		vec v = new vec(2,3,4);
		u.print    ("u  =");
		v.print    ("v  =");
		(u+v).print("u+v=");
		(2*u).print("2*u=");
		vec w=u*2;
		w.print("u*2=");
		vec w2=u+6*v-w;
		w2.print("w2=");
		(-u).print("-u=");
		WriteLine($"u%v      = {u % v}");
		WriteLine($"u.dot(v) = {u.dot(v)}");

		double x = 1.23;
		x.print("x=");
		(x+5).print("x+5=");
		(5*x).print("5*x=");
	double d1 = 0.1+0.1+0.1+0.1+0.1+0.1+0.1+0.1;
	double d2 = 8*0.1;
	double d3 = 0.125+0.125+0.125+0.125+0.125+0.125+0.125+0.125;
	double d4 = 8*0.125;
	WriteLine(d1==d2);
	WriteLine($"d1 = {d1:e15}");
	WriteLine($"d2 = {d2:e15}");
	WriteLine(d3==d4);
	WriteLine($"d3 = {d3:e15}");
	WriteLine($"d4 = {d4:e15}");

	}
}



