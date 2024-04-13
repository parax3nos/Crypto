using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RSA
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

        public static int reciprocalNumber (int el, int module)
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
                number  = number / 2;
                stBs += Convert.ToString(ostatok);
            }
            stBs += Convert.ToString(number);
            
            return stBs; // mirror;
            string stBf = ""; //really
            for (int i = stBs.Length-1; i >= 0; i--)
            {
                stBf += stBs[i];
            }
            return stBf;
        }

        public static ulong stringToInt(ulong s, int i, int length)
        {
            double delitel = Math.Pow(10,length-1-i);
            ulong n = s / Convert.ToUInt64(delitel);
            if (i != 0) return n%10;
            else return n;
        }

        public static int vStepen(int stepen, int el, int module)
        {
            string stB = toBinary(stepen);
            int[] k = new int[stB.Length];
            int[] A = new int[stB.Length];
            int[] b = new int[stB.Length];
            ulong s = ulong.Parse(stB);
            for (int i = 0; i < stB.Length; i++)
            {
                k[i] = Convert.ToInt32(stringToInt(s, i, stB.Length));
            }

            A[0] = el;

            if (k[0] == 0) b[0] = 1;
            else b[0] = A[0];

            for (int i = 1; i < stB.Length; i++)
            {
                A[i] = (A[i - 1] * A[i-1])%module;
                if (k[i] == 0) b[i] = b[i - 1];
                else b[i] = (b[i - 1] * A[i]) % module;
            }

            //output
            //for (int i = 0; i < stB.Length; i++)
            //{
            //    Console.Write(k[i] + "\t");
            //}
            //Console.WriteLine();
            //for (int i = 0; i < stB.Length; i++)
            //{
            //    Console.Write(A[i] + "\t");
            //}
            //Console.WriteLine();
            //for (int i = 0; i < stB.Length; i++)
            //{
            //    Console.Write(b[i] + "\t");
            //}
            //Console.WriteLine(  );
            return b[stB.Length - 1];
        }

        public static int[] RSA(int p, int q, int e, int[] text)
        {
            int n = p * q;
            int fin = (p - 1) * (q - 1);
            int d = reciprocalNumber(e, fin);
            Console.WriteLine($"\nn= {n} | \tfin= {fin} | \td= {d}");

            int[] textE = new int[text.Length];

            Console.WriteLine("\nЗашифрованный текст:");
            for (int i = 0; i < textE.Length; i++)
            {
                textE[i] = vStepen(e, text[i],n);
                Console.Write(textE[i] + "\t");
            }

            int[] textD = new int[text.Length];

            Console.WriteLine("\n\nРасшифрованный текст:");
            for (int i = 0; i < text.Length; i++)
            {
                textD[i] = vStepen(d, text[i], n);
                Console.Write(textD[i] + "\t");
            }
            return text;
        }

        static void Main(string[] args)
        {
            int p = 4;
            start:
            Console.Write("a: ");
            double a = Convert.ToDouble(Console.ReadLine());
            Console.Write("b: ");
            double b = Convert.ToDouble(Console.ReadLine());
            double alg = (a - b)%p;
            if (alg < 0) alg = p + alg;
            Console.WriteLine($"NOD({alg},{p}): {NOD(alg,p)}");
            double anew = (a*a+1)%p;
            double bSH = (b * b + 1) % p;
            double bnew = (bSH*bSH+1) % p;
            Console.Write($"anew: {anew}, bnew: {bnew}\n");
            goto start;
            Console.ReadLine();
        //start:
        //    int p, q, e;
        //    Console.Write("Введите p: ");
        //    p = Convert.ToInt32(Console.ReadLine());
        //    Console.Write("\nВведите q: ");
        //    q = Convert.ToInt32(Console.ReadLine());
        //    choose:
        //    Console.Write($"\nВведите e (1<{(p-1)*(q-1)}): ");
        //    e = Convert.ToInt32(Console.ReadLine());
        //    if (NOD(e, (p - 1) * (q - 1)) != 1)
        //        goto choose;
        //    Console.Write("\nВведите количество символов массива: ");
        //    int c = Convert.ToInt32(Console.ReadLine());
        //    int[] text = new int[c];
        //    for (int i = 0; i < text.Length; i++)
        //    {
        //        Console.Write($"\nВведите {i} элемент: ");
        //        text[i] = Convert.ToInt32(Console.ReadLine());
        //    }
        //    RSA(p, q, e, text);
        //    Console.WriteLine("\n");
        //    goto start;
        }
    }
}
