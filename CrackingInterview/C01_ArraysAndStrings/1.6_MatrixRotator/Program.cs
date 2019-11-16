using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MatrixRotator
{
    class Program
    {
        static void Main(string[] args)
        {
            var matrix = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            ClockwiseRotate(matrix, 2);
        }

        static void ClockwiseRotate(int[,] matrix, int rotateCount)
        { }

        static void CounterClockwiseRotate(int[,] matrix, int rotateCount)
        { }

        static void PrintMatrix(int[,] matrix)
        { }
    }
}
