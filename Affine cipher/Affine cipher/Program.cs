using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affine_cipher
{
    internal class Program
    {
        public static void Encription(int c, int a, int b, int n, int[] Letters)
        {
            Console.WriteLine("Fucntion of encription:");
            int[] Encr = new int[c];
            for (int i = 0; i < Letters.Length; i++)
            {
                Encr[i] = (a * Letters[i] + b) % n;
                if (Encr[i] < 0)
                {
                    Encr[i] %= n;
                    Encr[i] = n + Encr[i];
                }
                else Encr[i] %= n;
            }
            for (int i = 0; i < Encr.Length; i++)
            {
                Console.WriteLine($"Для элемента {Letters[i]} - {Encr[i]}");
            }
        }
        public static void Description(int c, int a, int b, int n, int[] Letters)
        {
            Console.WriteLine("Fucntion of encription:");
            int[] Descr = new int[c];

            //Reciprocal number search algorithm
            int x;
            int[,] M = new int[2, 8] { {0,0,0,0,n,a,0,1},
                                       {0,0,1,0,0,0,0,0}};
            Console.WriteLine("Матрица поиска:");
            while (M[1, 2] != 0)
            {
                M[1, 0]++;
                M[1, 1] = M[0, 4] / M[0, 5]; //q = n/a
                M[1, 2] = M[0, 4] % M[0, 5]; //r = n%a
                M[1, 3] = M[0, 6] - M[1, 1] * M[0, 7]; //y = y2 - q*y1
                M[1, 4] = M[0, 5];//n = a
                M[1, 5] = M[1, 2];// a = r
                M[1, 6] = M[0, 7];//y2 = y1
                M[1, 7] = M[1, 3];//y1 = y
                M[0, 0] = M[1, 0];
                M[0, 1] = M[1, 1];
                M[0, 2] = M[1, 2];
                M[0, 3] = M[1, 3];
                M[0, 4] = M[1, 4];
                M[0, 5] = M[1, 5];
                M[0, 6] = M[1, 6];
                M[0, 7] = M[1, 7];
            }
            x = M[0, 6];
            if (x < 0)
            {
                x %= n;
                x = n + x;
            }
            else x %= n;

            for (int i = 0; i < Letters.Length; i++)
            {
                Descr[i] = ((Letters[i] - b)*x)%n;
                if (Descr[i] < 0)
                {
                    Descr[i] %= n;
                    Descr[i] = n + Descr[i];
                }
                else Descr[i] %= n;
            }
            Console.WriteLine($"\nopposite a - {x}\n");
            for (int i = 0; i < Descr.Length; i++)
            {
                Console.WriteLine($"Для элемента {Letters[i]} - {Descr[i]}");
            }
        }

        static void Main(string[] args)
        {
            int n, a, b;
            Console.Write("Введите количество символов: ");
            int c = Convert.ToInt32(Console.ReadLine());
            int[] Letters = new int[c];
            for (int i = 0; i < Letters.Length; i++) 
            {
                Console.Write($"Введите символ для {i} элемента: ");
                Letters[i] = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine();
            Console.Write("Введите мощность алфавита: ");
            n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Console.Write("Введите ключ a: ");
            a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Console.Write("Введите ключ b: ");
            b = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            choosingFunc:
            Console.WriteLine("Choose function: 1 - Encription | 2 - Description");
            int choose = Convert.ToInt32(Console.ReadLine());
            if (choose == 1) 
            {
                Console.WriteLine();
                Encription(c, a, b, n, Letters);
            }
            if (choose == 2)
            {
                Console.WriteLine();
                Description(c, a, b, n, Letters);
            }

            Console.WriteLine();
            Console.WriteLine("Перейти к выбору функций - 1 | Перейти к следующей задаче - 2");
            choose = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            if ( choose == 1)
            {
                goto choosingFunc;
            }
            if (choose == 2)
            {
                Main(args);
            }

        }
    }
}
