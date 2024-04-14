using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    internal class Program
    {
        public static int[] shiftLeft(int[] A)
        {
            int[] temp = new int[A.Length];
            for (int i = 0; i < A.Length; i++) 
            {
                temp[i] = A[(i+1)%A.Length];
            }
            return temp;
        }

        public static int[] XOR(int[] A1, int[] A2)
        {
            int[] res = new int[A1.Length];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = (A1[i] + A2[i]) % 2;
            }
            return res;
        }

        public static int[] toB(int[] xorRK, int iS, int iF)
        {
            int[] B = new int[6];
            int l = 0;
            for (int i = iS; i < iF; i++)
            {
                B[l] = xorRK[i];
                l++;
            }
            return B;
        }

        public static int lineForB(int[] B)
        {
            int str = B[0]*2 + B[5];
            return str;
        }
        public static int columnForB(int[] B)
        {
            int row = B[1] * 2 * 2 * 2 + B[2] * 2 * 2 + B[3] * 2 + B[4];
            return row;
        }

        public static int[] tenTo2(int N)
        {
            int ostatok;
            int[] A = new int[4];
            for (int i = 3; i >= 0; i--)
            {
                ostatok = N % 2;
                N = N / 2;
                A[i] = ostatok;
            }
            return A;
        }

        static void Main(string[] args)
        {
            int[] C = new int[64];
            for (int i = 0; i < C.Length; i++)
            {
                C[i] = 0;
            } //основное сообщение - 64 бит
            int[] M = { 1,1,1,0,    1,1,0,0,
                        1,1,1,1,    0,0,1,1,
                        1,1,1,1,    0,0,0,0,
                        1,1,1,0,    0,1,1,1,
                        1,1,1,0,    1,0,0,0,
                        1,1,1,0,    1,1,0,1};//сообщение

            int l = M.Length-1;
            for (int i = C.Length-1; i >=0; i--) 
            {
                while (l >= 0)
                { 
                    C[i] = M[l];
                    l--;
                    break;
                }
            } //начальное присвоение сообщения


            int[] K64 = new int[64];
            for (int i = 0; i < K64.Length; i++)
            {
                K64[i] = 0;
            } //основной ключ - 64 бит
            int[] startK = { 1,1,1,0,   1,0,1,0,
                             1,1,1,0,   1,0,1,1,
                             1,1,1,1,   1,1,1,0,
                             1,1,1,1,   0,1,1,1}; //начальный ключ

            l = startK.Length - 1;
            for (int i = K64.Length - 1; i >= 0; i--)
            {
                while (l >= 0)
                {
                    K64[i] = startK[l];
                    l--;
                    break;
                }
            } //начальное присвоение ключа


            int[] IP = { 58,50,42,34,   26,18,10,2,
                         60,52,44,36,   28,20,12,4,
                         62,54,46,38,   30,22,14,6,
                         64,56,48,40,   32,24,16,8,
                         57,49,41,33,   25,17,9,1,
                         59,51,43,35,   27,19,11,3,
                         61,53,45,37,   29,21,13,5,
                         63,55,47,39,   31,23,15,7}; //таблица начальной перестановки IP

            int[] L = new int[C.Length / 2];
            int[] R = new int[C.Length / 2];

            for (int i = 0; i < L.Length; i++)
            {
                L[i] = C[IP[i]-1];
                R[i] = C[IP[L.Length + i] - 1];
            }//разделение основного сообщения на два блока

            Console.Write("L: "); //output L
            for (int i = 0; i < L.Length; i++)
            {
                if (i % 4 == 0 && i != 0) Console.Write(" ");
                Console.Write(L[i]);
            }
            Console.WriteLine();
            Console.Write("R: "); //output R
            for (int i = 0; i < R.Length; i++)
            {
                if (i % 4 == 0 && i != 0) Console.Write(" ");
                Console.Write(R[i]);
            }
            Console.WriteLine();


            int[] tableC0 = { 57,49,41,33,  25,17,9,1,
                              58,50,42,34,  26,18,10,2,
                              59,51,43,35,  27,19,11,3,
                              60,52,44,36};
            int[] tableD0 = { 63,55,47,39,  31,23,15,7,
                              62,54,46,38,  30,22,14,6,
                              61,53,45,37,  29,21,13,5,
                              28,20,12,4};//таблицы подготовки ключа и разделение его

            int[] C0 = new int[28];
            int[] D0 = new int[28];
            for (int i = 0; i < tableC0.Length; i++)
            {
                C0[i] = K64[tableC0[i]-1];
                D0[i] = K64[tableD0[i]-1];
            }//присвоение ключам значений таблиц

            C0 = shiftLeft(C0);//сдвиг битов на 1 влево
            D0 = shiftLeft(D0);

            Console.Write("\nC0: "); //output C0
            for (int i = 0; i < C0.Length; i++)
            {
                if (i % 4 == 0 && i != 0) Console.Write(" ");
                Console.Write(C0[i]);
            }
            Console.WriteLine();
            Console.Write("D0: "); //output D0
            for (int i = 0; i < D0.Length; i++)
            {
                if (i % 4 == 0 && i != 0) Console.Write(" ");
                Console.Write(D0[i]);
            }
            Console.WriteLine();


            int[] K56 = new int[56]; 
            for (int i = 0; i < K56.Length; i++)
            {
                if(i < 28) K56[i] = C0[i];
                else K56[i] = D0[i%D0.Length];
            } //сжатие К до 56 бит С0+D0


            int[] K48 = new int[48];
            int[] tableK48 = { 14,17,11,24, 1,5,3,28,
                               15,6,21,10,  23,19,12,4,
                               26,8,16,7,   27,20,13,2,
                               41,52,31,37, 47,55,30,40,
                               51,45,33,48, 44,49,39,56,
                               34,53,46,42, 50,36,29,32};//таблица сжатия ключа до 48 бит
            for (int i = 0; i < K48.Length; i++)
            {
                K48[i] = K56[tableK48[i]-1];
            }

            Console.Write("\nK48: "); //output K48
            for (int i = 0; i < K48.Length; i++)
            {
                if (i % 4 == 0 && i != 0) Console.Write(" ");
                Console.Write(K48[i]);
            }
            Console.WriteLine();

            int[] R48 = new int[48];
            int[] tableR48 = { 32,1,2,3,    4,5,4,5,
                               6,7,8,9,     8,9,10,11,
                               12,13,12,13, 14,15,16,17,
                               16,17,18,19, 20,21,20,21,
                               22,23,24,25, 24,25,26,27,
                               28,29,28,29, 30,31,32,1};//таблица расширения R до 48 бит

            for (int i = 0; i < R48.Length; i++)
            {
                R48[i] = R[tableR48[i] - 1];
            }

            Console.Write("\nR48: "); //output R48
            for (int i = 0; i < R48.Length; i++)
            {
                if (i % 4 == 0 && i != 0) Console.Write(" ");
                Console.Write(R48[i]);
            }
            Console.WriteLine();


            int[] xorRK = XOR(R48, K48);
            Console.Write("\nxorRK: "); //output xorRK
            for (int i = 0; i < xorRK.Length; i++)
            {
                if (i % 4 == 0 && i != 0) Console.Write(" ");
                Console.Write(xorRK[i]);
            }
            Console.WriteLine();

            int[] B1 = toB(xorRK, 0, 6);
            int[] B2 = toB(xorRK, 6, 12);
            int[] B3 = toB(xorRK, 12, 18);
            int[] B4 = toB(xorRK, 18, 24);
            int[] B5 = toB(xorRK, 24, 30);
            int[] B6 = toB(xorRK, 30, 36);
            int[] B7 = toB(xorRK, 36, 42);
            int[] B8 = toB(xorRK, 42, 48);

            int columnB1 = columnForB(B1);
            int lineB1 = lineForB(B1);
            int columnB2 = columnForB(B2);
            int lineB2 = lineForB(B2);
            int columnB3 = columnForB(B3);
            int lineB3 = lineForB(B3);
            int columnB4 = columnForB(B4);
            int lineB4 = lineForB(B4);
            int columnB5 = columnForB(B5);
            int lineB5 = lineForB(B5);
            int columnB6 = columnForB(B6);
            int lineB6 = lineForB(B6);
            int columnB7 = columnForB(B7);
            int lineB7 = lineForB(B7);
            int columnB8 = columnForB(B8);
            int lineB8 = lineForB(B8);

            int[,] S1 = { {14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7 },
                         { 0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8 },
                         { 4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0 },
                         { 15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13} };
            int[,] S2 = { {15, 1,   8,   14,  6,   11,  3,   4,   9,   7,   2,   13,  12,  0,   5,   10 },
                         { 3,   13,  4,   7,   15,  2,   8,   14,  12,  0,   1,   10,  6,   9,   11,  5 },
                         { 13,  6,   4,   9,   8,   15,  3,   0,   11,  1,   2,   12,  5,   10,  14,  7 },
                         { 1,   10,  13,  0,   6,   9,   8,   7,   4,   15,  14,  3,   11,  5,   2,  12} };
            int[,] S3 = { {10, 0,   9,   14,  6,   3,   15,  5,   1,   13,  12,  7,   11,  4,   2,   8 },
                         { 13,  7,   0,   9,   3,   4,   6,   10,  2,   8,   5,   14,  12,  11,  15,  1 },
                         { 13,  6,   4,   9,   8,   15, 3,   0,   11,  1,   2,   12,  5,   10,  14,  7 },
                         { 1,   10,  13,  0,   6,   9,   8,   7,   4,   15,  14,  3,   11,  5,   2,   12} };
            int[,] S4 = { {7,  13,  14,  3,   0,   6,   9,   10,  1,   2,   8,   5,   11,  12,  4,   15 },
                         { 13,  8,   11,  5,   6,   15,  0,   3,   4,   7,   2,   12,  1,   10, 14,  9 },
                         { 10,  6,   9,   0,   12,  11,  7,   13,  15, 1,   3,   14,  5,   2,   8,   4 },
                         { 3,   15,  0,   6,   10,  1,   13,  8,   9,   4,   5,   11,  12,  7,   2,   14} };
            int[,] S5 = { {2,  12,  4,  1,   7,   10,  11,  6,   8,   5,   3,   15,  13,  0,   14,  9 },
                         { 14,  11,  2,   12,  4,   7,   13,  1,   5,   0,   15,  10,  3,   9,   8,   6 },
                         { 4,   2,   1,   11,  10,  13,  7,   8,   15,  9,   12,  5,   6,   3,   0,   14 },
                         { 11,  8,   12,  7,   1,   14,  2,   13,  6,   15,  0,   9,   10,  4,   5,   3} };
            int[,] S6 = { {12, 1,   10,  15,  9,   2,   6,   8,   0,   13, 3,   4,   14,  7,  5,   11 },
                         { 10,  15,  4,   2,   7,   12,  9,   5,   6,   1,   13,  14,  0,   11,  3,   8 },
                         { 9,   14,  15, 5,   2,   8,   12,  3,   7,   0,   4,   10,  1,   13,  11,  6 },
                         { 4,   3,   2,   12,  9,   5,   15,  10,  11,  14,  1 ,  7,   6,   0,   8,   13} };
            int[,] S7 = { {4,  11,  2,   14, 15,  0,  8,   13,  3,   12,  9,   7,   5,   10,  6,   1 },
                         { 13,  0,   11,  7,  4,   9,   1,   10,  14,  3,   5,   12,  2,   15,  8,  6 },
                         { 1,   4,   11,  13,  12,  3,   7,   14,  10,  15,  6,   8,   0,   5,   9,   2 },
                         { 6,   11,  13,  8,   1,   4,   10,  7,   9,   5,   0,   15,  14,  2,   3,   12} };
            int[,] S8 = { {13, 2,   8,   4,   6,   15,  11,  1,   10,  9,   3,   14,  5,   0,   12, 7 },
                         { 1,   15,  13,  8,   10,  3,   7,   4,   12,  5,   6,   11,  0,   14,  9,   2 },
                         { 7,   11,  4,   1,   9,   12,  14,  2,   0,   6,   10,  13,  15,  3,   5,   8 },
                         { 2,   1,   14,  7,  4,   10,  8,   13,  15,  12,  9,   0,   3,   5,   6,   11} };

            int[] numB1 = tenTo2(S1[lineB1,columnB1]);
            int[] numB2 = tenTo2(S2[lineB2,columnB2]);
            int[] numB3 = tenTo2(S3[lineB3,columnB3]);
            int[] numB4 = tenTo2(S4[lineB4,columnB4]);
            int[] numB5 = tenTo2(S5[lineB5,columnB5]);
            int[] numB6 = tenTo2(S6[lineB6,columnB6]);
            int[] numB7 = tenTo2(S7[lineB7,columnB7]);
            int[] numB8 = tenTo2(S8[lineB8,columnB8]);

            int[] B = new int[32];
            for (int i = 0; i <= 3; i++) B[i] = numB1[i%4];
            for (int i = 4; i <= 7; i++) B[i] = numB2[i%4];
            for (int i = 8; i <= 11; i++) B[i] = numB3[i % 4];
            for (int i = 12; i <= 15; i++) B[i] = numB4[i % 4];
            for (int i = 16; i <= 19; i++) B[i] = numB5[i % 4];
            for (int i = 20; i <= 23; i++) B[i] = numB6[i % 4];
            for (int i = 24; i <= 27; i++) B[i] = numB7[i % 4];
            for (int i = 28; i <= 31; i++) B[i] = numB8[i % 4];


            int[] P = new int[32];
            int[] tableP = { 16, 7, 20,  21 , 29,  12,  28,  17,
                             1,   15,  23 , 26 , 5 ,  18,  31,  10,
                             2,   8 ,  24,  14,  32 , 27 , 3 ,  9,
                             19,  13 , 30 , 6 ,  22 , 11 , 4 ,  25,};//Таблица перестановки для B
            for (int i = 0; i < P.Length; i++)
            {
                P[i] = B[tableP[i]-1];
            }
            Console.Write("\nP: "); //output P
            for (int i = 0; i < P.Length; i++)
            {
                if (i % 4 == 0 && i != 0) Console.Write(" ");
                Console.Write(P[i]);
            }
            Console.WriteLine();

            int[] finallAnswer = XOR(P, L);
            Console.Write("\nF: "); //output finallAnswer
            for (int i = 0; i < finallAnswer.Length; i++)
            {
                if (i % 4 == 0 && i != 0) Console.Write(" ");
                Console.Write(finallAnswer[i]);
            }

            Console.ReadLine();
        }
    }
}
