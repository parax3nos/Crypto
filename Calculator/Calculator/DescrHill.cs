using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class DescrHill
    {
        static void Main(string[] args)
        {
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

            Console.WriteLine($"K {adsk}");

            int y1 = 3; 
            int y2 = 15;
            int y3 = 14; 
            int y4 = 13;

            int k_1 = 15;

            int y11 = 26; int y12 = 11;
            int y21 = 16; int y22 = 30;

            int k_11 = (k_1 * y11) % n; int k_12 = (k_1 * y12) % n;
            int k_21 = (k_1 * y21) % n; int k_22 = (k_1 * y22) % n;

            Console.WriteLine("K-1");
            Console.WriteLine($"{k_11} {k_12}\n" +
                              $"{k_21} {k_22}\n");

            int D1 = (k_11*y1 + k_12*y2)%n;
            int D2 = (k_21*y1 + k_22*y2)%n;
            int D3 = (k_11 * y3 + k_12 * y4) % n;
            int D4 = (k_21 * y3 + k_22 * y4) % n;

            Console.WriteLine("D");
            Console.WriteLine($"{D1}\n" +
                              $"{D2}\n" +
                              $"{D3}\n" +
                              $"{D4}\n");
            Console.ReadLine();
        }
    }
}
