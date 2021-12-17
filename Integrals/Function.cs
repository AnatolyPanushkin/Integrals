using System;

namespace Integrals
{
    public class Function
    {


        private double x;
        private double a;
        private int i;
        private readonly double _width;
        private double result;

        public Function(double a, int i, double width)
        {
            this.a = a;
            this.i = i;
            _width = width;
        }

        public double FunctionWithoutThreads()
        {
            x = a + i * _width;
            result = 10 - x;
            return result;
        }

        public void FunctionWithThreads()
        {
            x = a + i * _width;
            result = 10 - x;
        }
        public double GetResult()
        {
            return result;
        }
    }
}