using System;
using System.Collections.Generic;
using System.Data;
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

namespace week_5_sql
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string srStudentNameDescription = "Student Name", srStudentSurnameDescription = "Student Surname", srStudentScoreDescription = "Student AVG Score";

        private void txtStudenName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtStudenName.Text == srStudentNameDescription)
                txtStudenName.Text = "";
        }

        private void txtSurname_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSurname.Text == srStudentSurnameDescription)
                txtSurname.Text = "";
        }

        private void txtStudenScore_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtStudenScore.Text == srStudentScoreDescription)
                txtStudenScore.Text = "";
        }

        public MainWindow()
        {
            InitializeComponent();
            txtStudenName.Text = srStudentNameDescription;
            txtStudenScore.Text = srStudentScoreDescription;
            txtSurname.Text = srStudentSurnameDescription;
            initStudentsGrid();
        }

        private void btnAddStuden_Click(object sender, RoutedEventArgs e)
        {
            if (txtStudenName.Text.Length > 50)
            {
                MessageBox.Show("error too long student name");
                return;
            }
            if (txtStudenName.Text == srStudentNameDescription)
            {
                MessageBox.Show("error you didnt enter a student name");
                return;
            }

            if (txtSurname.Text.Length > 50)
            {
                MessageBox.Show("error too long student surname");
                return;
            }
            if (txtSurname.Text == srStudentSurnameDescription)
            {
                MessageBox.Show("error you didnt enter a student name");
                return;
            }

            int irStudentEnteredScore = -1;

            bool blResult = Int32.TryParse(txtStudenScore.Text, out irStudentEnteredScore);

            if (blResult == false)
            {
                MessageBox.Show("error you didnt enter a student score");
                return;
            }

            if (irStudentEnteredScore < 0 || irStudentEnteredScore > 255)
            {
                MessageBox.Show("error you have entered incorrect student score must be between 0-255");
                return;
            }

            DateTime dtPickedTime = DateTime.Now;

            if (datePick.SelectedDate != null)
            {
                dtPickedTime = datePick.SelectedDate.Value;
            }

            string srInsertQuery = @"insert into tblOgrenciler (Name,Surname,RegistrationDate,AvgScore) 
                            values ('N{0}','N{1}','{2}',{3})";

            srInsertQuery = string.Format(srInsertQuery,
                 txtStudenName.Text.Replace("'", "''"),
                    txtSurname.Text.Replace("'", "''"),
                        dtPickedTime.ToShortDateString(),
                            irStudentEnteredScore);

            string srQueryResult = "";

            dbConnection.update_database(srInsertQuery, out srQueryResult);

            initStudentsGrid();

            MessageBox.Show(srQueryResult);
        }

        private void initStudentsGrid()
        {
            string srQueryMsg = "";
            DataSet dsStudents = dbConnection.return_data_set("select 'Delete','Update',* from tblOgrenciler", out srQueryMsg);

            if (srQueryMsg != "Success")
            {
                MessageBox.Show(srQueryMsg);
                return;
            }

            if (dsStudents.Tables.Count > 0)
            {
                datagridStudents.ItemsSource = dsStudents.Tables[0].DefaultView;
                datagridStudents.Columns[0].IsReadOnly = true;
            }
        }
    }
}
