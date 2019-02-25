using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week_4
{
    public static class StaticPublicVariables
    {
        public static int irStudentCount = 0;

        private static int _irStudentCount_v2 = 0;

        public static int irStudentCount_v2
        {
            get
            {
                return _irStudentCount_v2 * 10;
            }

            set
            {
                _irStudentCount_v2 = value / 2;
            }
        }

        //this is empty constrcuter deafult one called when the object of this class is initizaliated 
        public static void PublicValues()//signature of this is empty
        {
            irStudentCount = 10;
        }

        public static void PublicValues(int _irStudentCount)//signature of this is an int
        {
            irStudentCount = _irStudentCount;
        }

        public static void PublicValues(int _irStudentCount, int _irMultiplier)//signature of this is int+int
        {
            irStudentCount = _irStudentCount * _irMultiplier;
        }

        private static void DoFix()
        {
            if (irStudentCount < 100)
                irStudentCount = 100;
        }

        public static int DivideStudentNumber(int irDivideNum)
        {
            DoFix();
            return irStudentCount / irDivideNum;
        }

        public class PerStudent
        {
            public PerStudent(int _irStudentId, int _irStudentScore)
            {
                irStudentId = _irStudentId;
                irStudentScore = _irStudentScore;
            }

            public int irStudentId = 0;
            public int irStudentScore = 0;
        }
    }
}
