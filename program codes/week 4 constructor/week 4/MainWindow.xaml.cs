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

        private void btnReturValue_Click(object sender, RoutedEventArgs e)
        {
            PublicValues myPublicVals = new PublicValues();
            lblVal.Content = $"Student count for default constructor is: {myPublicVals.irStudentCount}";
        }

        private void btnReturValue_Copy_Click(object sender, RoutedEventArgs e)
        {
            PublicValues myPublicVals = new PublicValues(55);
            lblVal.Content = $"Student count for integer set constructor is: {myPublicVals.irStudentCount}";
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
    }
}
