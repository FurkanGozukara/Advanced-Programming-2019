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

            dbConnection.cmd_update_DB
        }
    }
}
