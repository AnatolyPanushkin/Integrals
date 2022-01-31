using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Integrals
{
    public class Function
    {

        private static Mutex threadWait = new Mutex();
        private double x;
        private double a;
        private double b;
        private int i;
        private double _width;
        private double result;
        
        private double s = 0.0;
        private double s1 = 0.0;
        private int n = 1;
       
       

        public Function(double a, int i, double width)
        {
            this.a = a;
            this.i = i;
            _width = width;
            
        }
        
        public Function(double a, double b)
        {
            this.a = a;
            this.b = b;
        }
        public Function(){}

        public double FunctionWithoutThreads()
        {
            x = a + i * _width;
            result = x*x-5;
            return result;
        }

        public void FunctionWithThreads()
        {
            x = a + i * _width;
            result = x*x-5;
        }

        public void WithThreads()
        {
            threadWait.WaitOne();
            _width = (b-a) / n;
            double y = 0;
            for (int j = 0; j < n; j++)
            {
                x = a + j * _width;
                y+=x * x - 5;
            }

            s1 = y * _width;
            
            if (n == 1)
            {
                s = s1;
                n = n * 2;
                WithThreads();
            }
        
            if (Math.Abs(s1 - s) >= Program.eps)
            {
                n = n * 2;
                s = s1;
                WithThreads();
                
            }
            result = s1;
          threadWait.ReleaseMutex();

        }
        
        
        public double GetResult()
        {
            return result;
        }
    }
}