using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncrHill
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] x = {26, 2, 11, 25};

            int[] E = new int[x.Length];

            int n = 31;

            int[,] K =  {{ 2, 22 },
                        { 1, 10 }};

            int adsk = K[0, 0] * K[1, 1] - K[0, 1] * K[1, 0];
            if (adsk < 0)
            {
                adsk = adsk % n;
                adsk = n + adsk;
            }
            else adsk = adsk % n;

            E[0] = (K[0, 0] * x[0] + K[0, 1] * x[1])%n;
            E[1] = (K[1, 0] * x[0] + K[1, 1] * x[1])%n;
            E[2] = (K[0, 0] * x[2] + K[0, 1] * x[3])%n;
            E[3] = (K[1, 0] * x[2] + K[1, 1] * x[3])%n;
            Console.WriteLine($"K {adsk}"); 
            Console.WriteLine("E");
            Console.WriteLine($"{E[0]}\n" +
                              $"{E[1]}\n" +
                              $"{E[2]}\n" +
                              $"{E[3]}\n");
            Console.ReadLine();
        }
    }
}
