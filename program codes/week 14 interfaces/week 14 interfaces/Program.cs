using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week_14_interfaces
{

    class Program
    {
        interface Cars
        {
            int getCarWeight();
            string getCarColor();
            string getCarBrand();
        }

        public class Bus : Cars
        {
            public int getCarWeight()
            {
                return 0;
            }
            public string getCarColor()
            {
                return "Red";
            }
            public string getCarBrand()
            {
                return "Tesla";
            }
        }

        public class School
        {
            private static string SchoolId = "1";
            protected static string SchoolName = "";
            public static int StudentCount = 0;

            public string returnSchoolId()
            {
                return SchoolId;
            }

            public class SubSchool
            {
                public string returnShoolId()
                {
                    return SchoolId;
                }
            }
        }

        public class University : School
        {
            public string returnSchoolName()
            {
                return SchoolName;
            }
        }

        static void Main(string[] args)
        {
            School mySchool = new School();
            School.StudentCount = 25;

            School mySchool2 = new School();
            School.StudentCount = 25;

            University myUni = new University();
            myUni.returnSchoolName();
        }
    }
}
