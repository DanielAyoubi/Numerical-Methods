using System.Numerics;
using System.Data;
using System.Threading;
using System;
using static System.Console;
using static System.Math;

class main{
    static void Main(){
        matrix A = new matrix("1,2,3; 4,5,6; 7,8,7");
        matrix R = new matrix(3, 3);
        QRGS.decomp(A, R);
    }

}
