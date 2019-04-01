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

namespace Week_9
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

        private void refreshListBox_Click(object sender, RoutedEventArgs e)
        {
            refreshList();
        }

        private void refreshList()
        {
            string srResult = "";
            lstRecords.Items.Clear();
            foreach (DataRow drw in dbConnection.return_data_set("select * from tblLessonStudents order by LessonId asc , StudentName asc",
                out srResult).Tables[0].Rows)
            {
                lstRecords.Items.Add("Lesson ID: " + drw["LessonId"].ToString() + "\t\t" + drw["StudentName"].ToString());
            }

            //DataSet ds = dbConnection.return_data_set("select * from tblLessonStudents order by LessonId asc , StudentName asc",
            //     out srResult);

            //foreach (DataRow drw in ds.Tables[0].Rows)
            //{

            //}
        }

        private void btnAddRecord_Click(object sender, RoutedEventArgs e)
        {
            int irLessonId = 0;

            Int32.TryParse(txtStudentId.Text, out irLessonId);

            if(irLessonId<1)
            {
                MessageBox.Show("incorrect lesson id");
                return;
            }

            string srQuery = "insert into tblLessonStudents (LessonId,StudentName) values (@LessonId,@StudentName)";

            List<dbConnection.cmdParameterType> lstMyValues = new List<dbConnection.cmdParameterType> {
                new dbConnection.cmdParameterType("@LessonId",irLessonId),
                new dbConnection.cmdParameterType("@StudentName",txtStudentName.Text)
            };

            dbConnection.cmd_update_DB(srQuery, lstMyValues);

            lstRecords.Items.Insert(0, "Lesson ID: " + irLessonId + "\t\t" + txtStudentName.Text);
        }

        private void btnUpdateRecord_Click(object sender, RoutedEventArgs e)
        {
            if(lstRecords.SelectedIndex<0)
            {
                MessageBox.Show("no item is selected to be updated");
                return;
            }

            string srSelectedItem = lstRecords.SelectedItem.ToString();

            string srStudentName = srSelectedItem.Split('\t').Last();
            string srSelectedLessonId = srSelectedItem.Split('\t').First().Replace("Lesson ID: ","");

          

            int irLessonId = 0;

            Int32.TryParse(txtStudentId.Text, out irLessonId);

            if (irLessonId < 1)
            {
                MessageBox.Show("incorrect lesson id");
                return;
            }

            string srQuery = @"update tblLessonStudents set LessonId=@LessonId, StudentName=@StudentName 
                            where LessonId = @OldLessonId and StudentName = @OldStudentName";

            List<dbConnection.cmdParameterType> lstMyValues = new List<dbConnection.cmdParameterType> {
                new dbConnection.cmdParameterType("@LessonId",irLessonId),
                new dbConnection.cmdParameterType("@StudentName",txtStudentName.Text),
                    new dbConnection.cmdParameterType("@OldLessonId",srSelectedLessonId),
                new dbConnection.cmdParameterType("@OldStudentName",srStudentName),
            };

            dbConnection.cmd_update_DB(srQuery, lstMyValues);

            refreshList();
        }
    }
}
