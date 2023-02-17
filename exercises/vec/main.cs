using System;
using static System.Console;
using static System.Math;
using vector; // added namespace vector in vec.cs
public static class main{


	public static void print(this double x,string s){
		Write(s);
        WriteLine(x);
		}

	public static void print(this double x){
		x.print("");
	}
	public static void Main(){
		vec u = new vec(1,2,3);
		vec v = new vec(2,3,4);
		vec p = new vec(2,3,4);
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
		WriteLine($"u.dot(v) = {p.dot(v)}");
		WriteLine($"u.approx(v) = {p.approx(v)}");
        bool vec_approx = vec.approx(u,v);
       WriteLine(vec_approx); 
       WriteLine(u.ToString());
	}
}



