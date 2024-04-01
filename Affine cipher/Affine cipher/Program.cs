using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Affine_cipher
{
    internal class Program
    {
        public static string Encription(int c, int a, int b, int n, string word)
        {

            char[] alph = "абвгдежзийклмнопрстуфхцчшщъыьэюя".ToCharArray();

            string newword = "";

            int count;

            for (int i = 0; i < word.Length; i++)
            {
                count = 0;
                for (int j = 0; j < alph.Length; j++)
                {
                    if (word[i] == alph[j])
                    {
                        int num = (a*j+b) % n;
                        if (num < 0)
                        {
                            num %= n;
                            num = n + num;
                        }
                        else num %= n;
                        newword += alph[num];
                        count++;
                        break;
                    }
                }
                if (count == 0) newword += word[i];
            }

            return newword;

        }
        public static string Description(int c, int a, int b, int n, string word)
        {
            char[] alph = "абвгдежзийклмнопрстуфхцчшщъыьэюя".ToCharArray();

            string newword = "";
            //Reciprocal number search algorithm
            int x;//
            int[,] M = new int[2, 8] { {0,0,0,0,n,a,0,1},
                                       {0,0,1,0,0,0,0,0}};
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
            else x %= n;//end RNSA

            int count;

            for (int i = 0; i < word.Length; i++)
            {
                count = 0;
                for (int j = 0; j < alph.Length; j++)
                {
                    if (word[i] == alph[j])
                    {
                        int num = ((j - b) * x) % n;
                        if (num < 0)
                        {
                            num %= n;
                            num = n + num;
                        }
                        else num %= n;
                        newword += alph[num];
                        count++;
                        break;
                    }
                }
                if (count == 0) newword += word[i];
            }

            return newword;
        }

        static void Main(string[] args)
        {
            int n, a, b;
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
            //Console.Write("Введите слово: ");

            string firstPath = @"C:\Users\Evgeny\Downloads\3 юмор.txt";
            string lastPath = @"C:\Users\Evgeny\Desktop\desc.txt";

            string word = File.ReadAllText(firstPath, Encoding.UTF8);

            Console.WriteLine(word);

            Console.WriteLine();
            Console.WriteLine("\nВыберите функцию: 1 - Зашифровать | 2 - Расшифровать");
            int choose = Convert.ToInt32(Console.ReadLine());

            if (choose == 1) Console.WriteLine(Encription(word.Length, a, b, n, word));

            if (choose == 2)
            {
                //File.WriteAllText(lastPath, Description(word.Length, a, b, n, word),Encoding.UTF8);
                Console.WriteLine(Description(word.Length, a, b, n, word));
            }

            Console.WriteLine("\n\nДругой текст - 1 | Следущая задача - 2");
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
