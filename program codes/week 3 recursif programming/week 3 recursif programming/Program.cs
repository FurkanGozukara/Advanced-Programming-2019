using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week_3_recursif_programming
{
    class Program
    {
        static void Main(string[] args)
        {
            //int irSum = CalculateSum(0, 1, 5, 1);
            //Console.WriteLine("result is " + irSum);

            csCalculateFactoriel myFactorielClass = new csCalculateFactoriel();

            Stopwatch myWatch = new Stopwatch();
            myWatch.Start();
            for (int i = 0; i < 100; i++)
            {
                var myFactorielIterativeResult = myFactorielClass.func_Calculate_Factoriel_iterative(7777);
            }
            myWatch.Stop();

            double dblTotalMs = myWatch.ElapsedMilliseconds;

            Console.WriteLine("iterative calculation total ms: " + dblTotalMs);
            Console.WriteLine("iterative calculation avg ms: " + (dblTotalMs / 100).ToString("N2"));

            double dblTotalTicks = myWatch.ElapsedTicks;

            Console.WriteLine("iterative calculation total ticks: " + dblTotalTicks);
            Console.WriteLine("iterative calculation total ticks avg ms: " + (dblTotalTicks / 100).ToString("N2"));

            myWatch.Reset();
            myWatch.Start();
            for (int i = 0; i < 100; i++)
            {
                ulong myFactorielRecursiveResult = csStaticFactoriel.func_calculateFactoriel(7777);
            }
            myWatch.Stop();

            dblTotalMs = myWatch.ElapsedMilliseconds;

            Console.WriteLine("recursive calculation total ms: " + dblTotalMs);
            Console.WriteLine("recursive calculation avg ms: " + (dblTotalMs / 100).ToString("N2"));

            dblTotalTicks = myWatch.ElapsedTicks;

            Console.WriteLine("iterative calculation total ticks: " + dblTotalTicks);
            Console.WriteLine("iterative calculation total ticks avg ms: " + (dblTotalTicks / 100).ToString("N2"));

            Console.ReadLine();
        }

        static int CalculateSum(int sum, int n, int m, int irStepNumber)
        {
            Console.WriteLine($"starting step {irStepNumber}..");

            if (n < m)
            {
                Console.WriteLine($"since n ({n}) is smaller then m ({m}) calling itself again");
                sum = CalculateSum(sum, n + 1, m, irStepNumber += 1);
            }


            sum = sum + n;
            Console.WriteLine($"step {irStepNumber} sum is {sum}");

            return sum;
        }
    }
}
