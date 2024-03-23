using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reciprocal_number_search_algorithm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n, a;

            Console.WriteLine("Введите следующие параметры:");
            Console.Write("Введите n: ");
            n = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nВведите a: ");
            a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

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
                Console.WriteLine($"{M[0, 0]}\t|{M[0, 1]}\t|{M[0, 2]}\t|{M[0, 3]}\t|{M[0, 4]}\t|{M[0, 5]}\t|{M[0, 6]}\t|{M[0, 7]}");
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
            Console.WriteLine($"{M[0,0]}\t|{M[0, 1]}\t|{M[0, 2]}\t|{M[0, 3]}\t|{M[0, 4]}\t|{M[0 , 5]}\t|{M[0, 6]}\t|{M[0,7]}");
            Console.WriteLine("\nИтоговый ответ: ");
            Console.WriteLine(x);
            Main(args);
        }
    }
}
