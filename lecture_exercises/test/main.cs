using System;
class Program
  {
    static void Main(string[] args)
    {
      // dna strand
      string startStrand = "ATGCGATGAGCTTAC";

      // find location of "tga"
            int tga = startStrand.IndexOf("TGA");
      int startPoint = 0;
      int ength = tga + 3;

     string dna = startStrand.Substring(tga, tga+3);
      Console.WriteLine(dna);
    }
}
