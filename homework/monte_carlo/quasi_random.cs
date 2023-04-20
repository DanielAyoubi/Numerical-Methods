using System;

public static class QuasiRandomSequences {
    public static double corput(int n, int b) {
        double q = 0;
        double bk = 1.0 / b;
        while (n > 0) {
            q += (n % b) * bk;
            n /= b;
            bk /= b;
        }
        return q;
    }

    public static void halton(int n, int d, double[] x) {
        int[] bases = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61 };
        int maxd = bases.Length;

        if (d <= maxd){
            for (int i = 0; i < d; i++) {
                x[i] = corput(n, bases[i]);
        }
        }
    }

}
