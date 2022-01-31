using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Integrals
{
    class Program
    {
        internal static double s;
        internal static double eps;
        public static Stopwatch sw = new Stopwatch();
        static double WithoutThreads(double a, double b, int n)
        {
            double width;
            double heights = 0.0;
            width = (b - a) / n;//длина основания прямоугольника при кол-ве шагов = n

            List<Double> results = new List<Double>();
            for (int i = 0; i < n; i++)
            {
                Function height = new Function(a, i, width);
                results.Add(height.FunctionWithoutThreads());
            }

            for (int i = 0; i < n; i++)
            {
                heights += results[i];
            }

            double s1 = heights * width;
            
            if (n == 1)
            {
                s = s1;
                n = n * 2;
                WithoutThreads(a, b, n);
            }

            if (Math.Abs(s1 - s) >= eps)
            {
                n = n * 2;
                s = s1;
                s1 = WithoutThreads(a, b, n);
            }

            return (s1);
        }

        static double WithThreads(double a, double b)
        {
            double heights = 0.0;
            double width = (b-a)/8;
            List<Thread> threads = new List<Thread>();
            List<Function> results = new List<Function>();

            for (int i = 0; i < 8; i++)
            {
                Function height = new Function(a,a + width);
                threads.Add(new Thread(height.WithThreads));
                results.Add(height);
                a += width;
            }
            sw.Reset();
            sw.Start();
            foreach (var t in threads)
            {
                t.Start();
            }
        
            foreach (var t in threads)
            {
                t.Join();
            }
        
            for (int i = 0; i < 8; i++)
            {
                heights += results[i].GetResult();
            }
            
            sw.Stop();
            return (heights); 
        
            
        }

        static void Main(string[] args)
        {
            double a, b;
            int n = 1; //начальное число шагов

            Console.Write("Введите левую границу интегрирования a = ");
            a = double.Parse(Console.ReadLine());

            Console.Write("\nВведите правую границу интегрирования b = ");
            b = double.Parse(Console.ReadLine());

            Console.Write("\nВведите требуемую точность eps = ");
            eps = double.Parse(Console.ReadLine());

            sw.Start();
            var res1 = WithoutThreads(a, b, n); //первое приближение для интеграла
            sw.Stop();
            Console.Write($"\nБез потоков :: Интеграл = {res1}\n" +
                          $"Время :: {sw.Elapsed}"+"\n");
           // sw.Reset();
           // sw.Start();
            var res2 = WithThreads(a, b);
            //sw.Stop();
            Console.Write($"\nС потоками :: Интеграл = {res2}\n" +
                          $"Время :: {sw.Elapsed}");

            Console.ReadKey();
        }
    }

}