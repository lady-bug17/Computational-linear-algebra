using System;
using System.Diagnostics;
using System.Threading;
using System.Xml.Serialization;

namespace ParallelCalc
{
    class ParallelGauss
    {
        static double[,] InitMatrix(int a, int b)
        {
            Random x = new Random();
            double[,] newMatrix = new double[a, b];
            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < b; j++)
                {
                    newMatrix[i, j] = x.Next(1, 10);
                }
            }
            return newMatrix;
        }

        static bool Part1(double[,] m1)
        {
            for (int i = 0; i < m1.GetLength(1) - 2; i++)
            {
                if (m1[i, i] != 0)
                {
                    for (int j = i + 1; j < m1.GetLength(0); j++)
                    {
                        double coef = m1[j, i] / m1[i, i];
                        for (int l = i; l < m1.GetLength(1); l++)
                        {
                            m1[j, l] -= coef * m1[i, l];
                        }
                    }
                }
                else
                {
                    bool isInvertible = true;
                    for (int j = i + 1; j < m1.GetLength(0); j++)
                    {
                        if (m1[j, i] != 0)
                        {
                            swap(m1, i, j);
                            isInvertible = false;
                            break;
                        }
                    }
                    if (isInvertible)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static void GaussMethod(double[,] m1, double[] result)
        {
            if (Part1(m1))
            {
                for (int i = m1.GetLength(0) - 1; i != -1; i--)
                {
                    double value = m1[i, m1.GetLength(1) - 1];
                    for (int j = m1.GetLength(1) - 2; j > i; j--)
                    {
                        value -= m1[i, j] * result[j];
                    }
                    result[i] = value / m1[i, i];
                }
            }
            else
            {
                Console.WriteLine("Your matrix is Invertible");
            }
        }

        static void swap(double[,] m1, int a, int b)
        {
            for (int i = 0; i < m1.GetLength(1); i++)
            {
                double copy = m1[a, i];
                m1[a, i] = m1[b, i];
                m1[b, i] = copy;
            }
        }

        static void PrintMatrix(double[,] m)
        {
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    System.Console.Write($"{m[i, j]} ");
                }
                System.Console.WriteLine();
            }
        }

        static void ResetColumnPart(double[,] m1, int a, int b, int d)
        {
            for (int i = a; i < b; i++)
            {
                double coef = m1[i, d] / m1[d, d];
                for (int j = d; j < m1.GetLength(1); j++)
                {
                    m1[i, j] -= coef * m1[d, j];
                }
            }
        }

        static bool ThreadedPart1(double[,] m1, int NumOfThreads)
        {
            Thread[] threads = new Thread[NumOfThreads - 1];
            bool isInvertable = true;
            for (int d = 0; d < m1.GetLength(1) - 1; d++)
            {
                if (m1[d, d] == 0)
                {
                    for (int j = d + 1; j < m1.GetLength(0); j++)
                    {
                        if (m1[j, d] != 0)
                        {
                            swap(m1, d, j);
                            break;
                        }
                    }
                    if (m1[d, d] == 0)
                    {
                        Console.WriteLine("Your matrix is Invertible");
                    }
                }
                int step = (m1.GetLength(0) - d - 1) / NumOfThreads;
                int currentPos = d + 1;

                for (int i = 0; i < threads.GetLength(0); i++)
                {
                    int cp = currentPos;
                    threads[i] = new Thread(() => ResetColumnPart(m1, cp, cp + step, d));
                    threads[i].Start();
                    currentPos += step;
                }
                ResetColumnPart(m1, currentPos, m1.GetLength(0), d);
                foreach (var item in threads)
                {
                    item.Join();
                }
            }
            return isInvertable;
        }

        public static void ThreadedGauss(double[,] m1, double[] result, int NumOfThreads)
        {
            if (ThreadedPart1(m1, NumOfThreads))
            {
                for (int i = m1.GetLength(0) - 1; i != -1; i--)
                {
                    double value = m1[i, m1.GetLength(1) - 1];
                    for (int j = m1.GetLength(1) - 2; j > i; j--)
                    {
                        value -= m1[i, j] * result[j];
                    }
                    result[i] = value / m1[i, i];
                }
            }
            else
            {
                Console.WriteLine("Your matrix is Invertible");
            }
        }

        //static void Main()
        //{
        //    int threadNumber = 100;
        //    int n = 1000;
        //    double[,] m1 = InitMatrix(n, n + 1);
        //    double[] x = new double[n];
        //    var watch = Stopwatch.StartNew();
        //    GaussMethod(m1, x);
        //    watch.Stop();
        //    Console.WriteLine($"Linear execution time: {watch.ElapsedMilliseconds} ms");
        //    watch = Stopwatch.StartNew();
        //    ThreadedGauss(m1, x, threadNumber);
        //    watch.Stop();
        //    Console.WriteLine($"Threaded execution time: {watch.ElapsedMilliseconds} ms");
        //}
    }
}

// for matrixes 1000x1001
//1 thread execution: 8352 ms (main thread)
//2 threads execution: 7949 ms
//4 threads execution: 6707 ms
//8 threads execution: 5238 ms
//100 threads execution: 23886 ms
