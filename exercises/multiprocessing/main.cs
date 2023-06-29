using System;
using System.Threading;
using static System.Console;
using System.Threading.Tasks;

class main{
    public class data{public int a,b; public double sum;}

    public static void harmonic(object obj){    
        var local = (data)obj;
        local.sum = 0;
        for(int i=local.a;i<local.b;i++)local.sum+=1.0/i;
    }
    static int Main(string[] args){
        int nterms= (int)1e8, nthreads = 1;
        foreach(var arg in args){
            var words = arg.Split(":");
            if(words[0] == "-threads") nthreads = int.Parse(words[1]);
            if(words[0] == "-terms") nterms = (int)float.Parse(words[1]);
        }
	    WriteLine($"nterms={nterms} nthreads={nthreads}");
        
        data[] x = new data[nterms];

        for(int i=0;i<nthreads;i++){
            x[i]= new data();
            x[i].a = 1 + nterms/nthreads*i;
            x[i].b = 1 + nterms/nthreads*(i+1);
   }

        Thread[] threads = new Thread[nthreads];
        for(int i=0;i<nthreads;i++){
            threads[i] = new Thread(harmonic); /* create a thread */
            threads[i].Start(x[i]);        /* run it with x[i] as argument to "harmonic" */
        }
	    WriteLine("master thread: now waiting for othrer threads...");

        for(int i=0; i<nthreads;i++) threads[i].Join();

        double sum_harmonic = 0;
        for(int i=0;i<nthreads;i++) sum_harmonic+=x[i].sum;
        WriteLine($"harmonic sum = {sum_harmonic}");
        
        double sum_parallel=0; 
        Parallel.For( 1, nterms+1, delegate(int i){sum_parallel+=1.0/i;} );
        WriteLine($"Harmonic sum_parallel = {sum_parallel}");
        
    return 0;
    }
}
