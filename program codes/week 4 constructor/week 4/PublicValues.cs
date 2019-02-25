using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week_4
{
    public class PublicValues
    {
        public int irStudentCount = 0;

        private int _irStudentCount_v2 = 0;

        public int irStudentCount_v2
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
        public PublicValues()//signature of this is empty
        {
            irStudentCount = 10;
        }

        public PublicValues(int _irStudentCount)//signature of this is an int
        {
            irStudentCount = _irStudentCount;
        }

        public PublicValues(int _irStudentCount, int _irMultiplier)//signature of this is int+int
        {
            irStudentCount = _irStudentCount * _irMultiplier;
        }
    }
}
