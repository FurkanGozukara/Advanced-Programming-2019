using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace week_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnReturValue_Copy_Click(object sender, RoutedEventArgs e)
        {
            PublicValues myPublicVals = new PublicValues(55);
            lblVal.Content = $"Student count for integer set constructor is: {myPublicVals.irStudentCount}";
        }

        private void btnReturValue_Copy5_Click(object sender, RoutedEventArgs e)
        {
            StaticPublicVariables.PublicValues(55);
            lblVal.Content = $"Student count for integer set constructor is: {StaticPublicVariables.irStudentCount}";
        }

        private void btnReturValue_Copy1_Click(object sender, RoutedEventArgs e)
        {
            PublicValues myPublicVals = new PublicValues(3, 9);
            lblVal.Content = $"Student count for integer+integer set constructor is: {myPublicVals.irStudentCount}";
        }

        private void btnReturValue_Copy2_Click(object sender, RoutedEventArgs e)
        {
            PublicValues myPublicVals = new PublicValues();
            myPublicVals.irStudentCount = 100;
            lblVal.Content = $"Student count my assigned value: {myPublicVals.irStudentCount}";

            lblVal2.Content = $"Student v2 count default value: {myPublicVals.irStudentCount_v2}";

            myPublicVals.irStudentCount_v2 = 10;
            lblVal2.Content = $"Student v2 count assigned value 10 value: {myPublicVals.irStudentCount_v2}";
        }

        private void btnReturValue_Copy3_Click(object sender, RoutedEventArgs e)
        {
            PublicValues myPublicVals = new PublicValues();
            myPublicVals.irStudentCount = 33;
            lblVal.Content = $"Student count 33/3 is: { myPublicVals.DivideStudentNumber(3)}";

            myPublicVals.irStudentCount = 233;
            lblVal2.Content = $"Student count 233/3 is: { myPublicVals.DivideStudentNumber(3)}";
        }

        private void btnGenerateRandomStudents_Click(object sender, RoutedEventArgs e)
        {
            List<PublicValues.PerStudent> lstStudents = new List<PublicValues.PerStudent>();
            Random randGenerator = new Random();

            for (int i = 0; i < 100; i++)
            {
                int irStudentNumber = randGenerator.Next();
                int irStudentScore = randGenerator.Next(0,100);
                PublicValues.PerStudent randStudent = new PublicValues.PerStudent(irStudentNumber, irStudentScore);
                lstStudents.Add(randStudent);
            }

            lstBoxStudents.Items.Clear();

            for (int i = 0; i < lstStudents.Count; i++)
            {
                lstBoxStudents.Items.Add($"No: {i+1}\tID: {lstStudents[i].irStudentId}\tScore: {lstStudents[i].irStudentScore}");
            }

            string srBase = "No: {0}\t\tID: {1}\t\tScore: {2}";

            lstBoxStudents.Items.Clear();

            for (int i = 0; i < lstStudents.Count; i++)
            {
                string srFormatedText = string.Format(srBase
                    , (i + 1)
                    , lstStudents[i].irStudentId
                    , lstStudents[i].irStudentScore);

                lstBoxStudents.Items.Add(srFormatedText);
            }
        }

        private void btnReturValue_Click(object sender, RoutedEventArgs e)
        {
            PublicValues myPublicVals = new PublicValues();
            lblVal.Content = $"Student count for default constructor is: {myPublicVals.irStudentCount}";
        }

        private void btnReturValue_Copy4_Click(object sender, RoutedEventArgs e)
        {
            StaticPublicVariables.PublicValues();
            lblVal.Content = $"Student count for default constructor is: {StaticPublicVariables.irStudentCount}";
        }

      
    }
}
