using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElGamal
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
            return stBs;
            string stBf = "";
            for (int i = stBs.Length - 1; i >= 0; i--)
            {
                stBf += stBs[i];
            }
            return stBf;
        }

        public static int stringToInt(int s, int i, int length)
        {
            int oiu = s / 10;
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
            int p, g, e, k, m, a, b, d;

            p = 53;
            g = 5;
            d = 13;

            e = vStepen(d,g,p);
            start:
            m = Convert.ToInt32(Console.ReadLine());

            k = 7;

            a = vStepen(k, g, p);
            b = (vStepen(k, e, p) * m) % p;

            m = (vStepen(p-1-d,a,p)*b)%p;

            Console.Write($"e: {e}\na: {a}\nШифр: {b}\nРасшифр: {m}");
            Console.WriteLine();
            goto start;
            Console.ReadKey();
        }
    }
}
