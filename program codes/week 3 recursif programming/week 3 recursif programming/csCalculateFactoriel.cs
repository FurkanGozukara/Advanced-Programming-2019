using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week_3_recursif_programming
{
    public class csCalculateFactoriel
    {
        public decimal func_Calculate_Factoriel_iterative(ulong irWhichNumberToCalculate)
        {
            ulong irResult = 1;
            for (ulong i = 1; i < (irWhichNumberToCalculate+1); i++)
            {
                irResult = irResult * i;
            }
            return irResult;
        }
    }
}
