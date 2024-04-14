using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeistelNetworks
{
    internal class Program
    {
        public static string toBinary(int number)
        {
            string stBs = "";
            int ostatok;
            while (number > 1)
            {
                ostatok = number % 2;
                number = number / 2;
                stBs += Convert.ToString(ostatok);
            }
            stBs += Convert.ToString(number);

            string stBf = ""; //really
            for (int i = stBs.Length - 1; i >= 0; i--)
            {
                stBf += stBs[i];
            }
            return stBf;
        }

        public static int[] charToInt(char[] numeric)
        {
            int[] A = new int[numeric.Length];
            int n;
            for (int i = 0; i < numeric.Length; i++)
            {
                string ht = Convert.ToString(numeric[i]);
                n = Convert.ToInt32(ht);
                A[i] = n;
            }
            return A;
        }

        public static int[] allFuncs(int[] R, int[] L, int[,] K, int iteration)
        {
            int[] F = new int[R.Length];
            int[] Ff = new int[F.Length];
            int[] Fs = new int[L.Length];

            //xor R and K
            if (iteration < 2) Console.Write($"F{iteration+1}: ");
            for (int i = 0; i < R.Length; i++)
            {
                F[i] = (R[i] + K[iteration,i]) % 2;
                if (iteration < 2) Console.Write(F[i]);
            }

            //сдвиг вправо
            //for (int i = 0; i < F.Length; i++)
            //{
            //    int poz = (i - 1) % F.Length;
            //    if (poz < 0)
            //    {
            //        poz = (F.Length + poz) % F.Length;
            //    }
            //    Ff[i] = F[poz];
            //    if (iteration < 2) Console.Write(Ff[i]);
            //}
            if (iteration < 2) Console.WriteLine("\n");

            if (iteration < 2) Console.Write($"R{iteration+1}: ");
            for (int i = 0; i < R.Length; i++)
            {
                Fs[i] = (F[i] + L[i]) % 2;
                if (iteration < 2) Console.Write(Fs[i]);
            }
            if (iteration < 2) Console.WriteLine("\n");

            return Fs; 
        }

        static void Main(string[] args)
        {
            int[,] K = { {1,1,1},
                         {1,0,0},
                         {0,1,1}};

            int iteration = 0;

            int n = 46;

            string nToB = toBinary(n);
            char[] numeric = nToB.ToCharArray();

            int[] LR = charToInt(numeric);
            int[] L = new int[LR.Length / 2];
            int[] R = new int[LR.Length / 2];

            for (int i = 0; i < LR.Length/2; i++)
            {
                L[i] = LR[i];
                R[i] = LR[LR.Length / 2 + i];
            }

            for (int k = 0; k < L.Length; k++)
            {
                Console.Write(L[k]);
            }
            Console.Write(" ");

            for (int k = 0; k < R.Length; k++)
            {
                Console.Write(R[k]);
            }
            Console.WriteLine("\n");

            for (int i = 0; i < 3; i++)
            {
                L = allFuncs(R, L, K, iteration);

                if (iteration < 2)
                {
                    Console.Write($"L{iteration + 1}: ");
                    for (int k = 0; k < R.Length; k++)
                    {
                        Console.Write(R[k]);
                    }
                    Console.WriteLine("\n==========\n");
                }

                iteration++;

                while (i < 2)
                {
                    int[] temp = new int[L.Length];
                    for(int p=0; p < temp.Length; p++)
                    {
                        temp[p] = L[p];
                    }

                    for (int k = 0; k < L.Length; k++)
                    {
                        L[k] = R[k];
                        R[k] = temp[k];
                    }
                    break;
                }
            }

            Console.Write($"L{iteration}: ");
            for (int k = 0; k < L.Length; k++)
            {
                Console.Write(L[k]);
            }
            Console.WriteLine("\n");

            Console.Write($"R{iteration}: ");
            for (int k = 0; k < R.Length; k++)
            {
                Console.Write(R[k]);
            }
            Console.WriteLine("\n");

            Console.ReadLine();
        }
    }
}
