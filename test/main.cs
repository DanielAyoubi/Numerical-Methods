using System;
public static class main{
   public static  double multiply(double a, double b)
{
double r = a*b;   /* do some useful stuff */
return r;         /* return a value of declared type */
}

}

class Klasse{
    static void Main(){
    double start = 2.0;
    double end = 2.0;
    double x = main.multiply(start, end);
    Console.Write(x);
}
}


