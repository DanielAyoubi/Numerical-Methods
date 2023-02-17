using System;
using static System.Math;
using static System.Console;

public static class sfuns{
    public const double PI = Math.PI;
    public const double E = Math.E;
    public const double sqrt2pi = Math.PI;

    public static double gamma(double x){
        if (x<=0) return double.NaN;
        if(x<9)return gamma(x+1)/x; // Recurrence relation
        double lngamma=x*Math.Log(x+1/(12*x-1/x/10))-x+Math.Log(2*PI/x)/2;
        return Math.Exp(lngamma);
}
    public static double lngamma(double x){
        if (x<=0) return double.NaN;
        /* if (x<9) return Math.log(gamma(x)); */
        if (x<9) return lngamma(x+1) - Math.Log(x);
        double lnlngamma = Math.Log(gamma(x));
        return lnlngamma;
    }
}
