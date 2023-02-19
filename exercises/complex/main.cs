using System;
using static System.Console;

class Program{

    public static void Main(){
        complex i = new complex(0, -1);
        complex sqrt_of_i = cmath.sqrt(i);
        WriteLine(sqrt_of_i);
    }
}
