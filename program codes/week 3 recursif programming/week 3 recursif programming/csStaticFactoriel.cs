using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week_3_recursif_programming
{
    public static class csStaticFactoriel
    {
        public static ulong func_calculateFactoriel(ulong irNumberToCalculate)
        {
            return (func_calculateFactoriel(irNumberToCalculate, 1, 1))/ irNumberToCalculate;
        }

        private static ulong func_calculateFactoriel(ulong irNumberToCalculate, ulong irLastNumberCalculated, ulong irCurrentResult)
        {
            if (irNumberToCalculate  > irLastNumberCalculated)
            {
                irCurrentResult = func_calculateFactoriel(irNumberToCalculate, irLastNumberCalculated += 1, irCurrentResult);
            }

            return irCurrentResult * irLastNumberCalculated;
        }
    }
}
