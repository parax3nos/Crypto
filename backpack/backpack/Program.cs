using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backpack
{
    internal class Program
    {
        public static int reciprocalNumber(int el, int module)
        {
            for (int i = 1; i < module; i++)
            {
                if ((el % module) * (i % module) % module == 1)
                    return i;
            }
            return -1;
        }

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

            //mirror
            //return stBs;

            string stBf = "";
            for (int i = stBs.Length - 1; i >= 0; i--)
            {
                stBf += stBs[i];
            }
            return stBf;
        }

        public static int stringToInt(int s, int i, int length)
        {
            double delitel = Math.Pow(10, length - 1 - i);
            int n = s / Convert.ToInt32(delitel);
            if (i != 0) return n % 10;
            else return n;
        }

        public static int vStepen(int stepen, int el, int module)
        {
            string stB = toBinary(stepen);
            int[] k = new int[stB.Length];
            int[] A = new int[stB.Length];
            int[] b = new int[stB.Length];
            int s = int.Parse(stB);
            for (int i = 0; i < stB.Length; i++)
            {
                k[i] = stringToInt(s, i, stB.Length);
            }

            A[0] = el;

            if (k[0] == 0) b[0] = 1;
            else b[0] = A[0];

            for (int i = 1; i < stB.Length; i++)
            {
                A[i] = (A[i - 1] * A[i - 1]) % module;
                if (k[i] == 0) b[i] = b[i - 1];
                else b[i] = (b[i - 1] * A[i]) % module;
            }
            return b[stB.Length - 1];
        }
        static void Main(string[] args)
        {
            int p, S, r, repR, m;

            S = 0;

            int[] W = {2, 7, 11, 21};

            for (int i = 0; i < W.Length; i++) 
            {
                S += W[i];
            }

            Console.Write($"Введите p(простое число - p > {S}): ");
            p = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();
            Console.Write($"Введите r(1;{p}]: ");
            r = Convert.ToInt32(Console.ReadLine());

            int[] E = new int[W.Length];

            for (int i = 0; i < W.Length; i++)
            {
                E[i] = W[i] * r % p;
            }

            Console.Write("E:\t");
            for (int i = 0; i < E.Length; i++)
            {
                Console.Write(E[i] + "\t");
            }
        start:
            Console.WriteLine();
            Console.Write($"Введите m: ");
            m = Convert.ToInt32(Console.ReadLine());
            
            int mInt = int.Parse(toBinary(m));

            int[] mA = new int[toBinary(m).Length];

            for (int i = 0; i < mA.Length; i++)
            {
                mA[i] = stringToInt(mInt, i, mA.Length);
            }

            Console.WriteLine(  );
            Console.Write("Бинарное: ");
            for (int i = 0; i < mA.Length; i++)
            {
                Console.Write(mA[i]);
            }
            Console.WriteLine(  );

            int Em = 0;
            for (int i = 0; i < mA.Length; i++)
            {
                Em += mA[i] * E[i];
            }

            repR = reciprocalNumber(r, p);

            int repEm = Em * repR % p;

            Console.WriteLine($"repEm: {repEm}");

            int[] Wb = new int[W.Length];
            for (int i = Wb.Length - 1; i >= 0; i--)
            {
                if (repEm - W[i] < 0) Wb[i] = 0;
                else
                {
                    repEm -= W[i];
                    Wb[i] = 1;
                }
            }

            Console.Write("\nW бинарная: ");
            for (int i = 0; i < Wb.Length; i++)
            {
                Console.Write(Wb[i] + "\t");
            }

            Console.WriteLine($"\nS: {S}\nrepR: {repR}\nEm: {Em}");
            goto start;
            Console.ReadKey();
        }
    }
}
