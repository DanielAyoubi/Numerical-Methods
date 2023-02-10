using System;
using static System.Console;
using static System.Math;
public static class main{

	public static string s = "class scope s";

	public static void Main(){
        int n = 9;
        double [] a = new double[n]; // Initiate array with 0s
        for(int i=0; i<n;i++) Write($"a[{i}] = {a[i]} ");
        WriteLine();

    }}
