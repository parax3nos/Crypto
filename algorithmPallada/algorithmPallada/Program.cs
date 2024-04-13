using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithmPallada
{
    internal class Program
    {
        public static double NOD(double a, double b)
        {
            double max = 0;
            double nod = 0;
            if (a > b) max = a;
            else max = b;

            for (int i = 1; i < b; i++)
            {
                if (a % i == 0 && b % i == 0)
                    nod = i;
            }
            return nod;
            
        }
        public static bool noSimple(double n)
        {
            int count = 0;
            for (int i = 0; i < n; i++)
            {
                if (n % i == 0) count++;
            }
            if (count == 1) return false;
            else return true;
        }
        static void Main(string[] args)
        {
        start:
            double p = Convert.ToDouble(Console.ReadLine());
            double pF = p;
            double pSH = 0;
            double dF;
            int count = 0;
            int it = 0;

            double[,] A = new double[2, 4];

            A[0, 0] = 0;
            A[0, 1] = 2;
            A[0, 2] = 2;
            A[0, 3] = 0;

            while (noSimple(p))
            {
                it++;
                A[1, 0]++;
                A[1, 1] = (Math.Pow(A[0, 1], 2) + 1) % p;
                double bSH = (Math.Pow(A[0, 2], 2) + 1) % p;
                A[1, 2] = (Math.Pow(bSH, 2) + 1) % p;

                dF = (A[1, 1] - A[1, 2]);
                if (dF < 0)
                {
                    dF = (p + dF);
                }
                else dF %= p;

                A[1, 3] = NOD(dF % p, p);

                if (A[1, 3] != 1)
                {
                    if (noSimple(A[1, 3]))
                    {
                        pSH = p / A[1, 3];
                        p = A[1, 3];
                    }
                    else
                    {
                        pSH = A[1, 3];
                        p /= A[1, 3];
                    }
                    count++;
                   // Console.Write($"{pSH}*\t");
                }

                for (int i = 0; i < 2; i++) 
                {
                    for (int j = 0; j < 4; j++)
                    {
                    Console.Write(A[i,j] + "\t");
                    }
                    Console.WriteLine();
                }

                A[0, 0] = A[1, 0];
                A[0, 1] = A[1, 1];
                A[0, 2] = A[1, 2];
                A[0, 3] = A[1, 3];
            }
            Console.Write($"{p} = {pF}\n{it}");
            goto start;
            Console.ReadLine();
        }
    }
}
