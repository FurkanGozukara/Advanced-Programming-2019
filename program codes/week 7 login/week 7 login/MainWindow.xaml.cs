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
using System.Data;
using System.Data.SqlClient;

namespace week_7_login
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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            safeLogin();
        }

        private void safeLogin()
        {
            DataTable dtTable = new DataTable();

            //parameterized queries are safe
            string cmdStr = "select * from tblUsers where Username=@Username and Password=@Password";

            using (SqlConnection connection = new SqlConnection(dbConnection.srConnectionString))
            using (SqlCommand command = new SqlCommand(cmdStr, connection))
            {
                command.Parameters.AddWithValue("@Username", txtUsername.Text);
                command.Parameters.AddWithValue("@Password", txtPassword.Password.ToString());
                connection.Open();

                dtTable.Load(command.ExecuteReader());
            }

            if (dtTable.Rows.Count == 0)
            {
                MessageBox.Show("incorrect username or password is entered");
                return;
            }

            MessageBox.Show("successfull");
        }

        private void UnsafeLoginCode()
        {
            string srResult = "";
            DataSet dsUser =
                dbConnection.return_data_set($"select * from tblUsers where Username=N'{txtUsername.Text}' and Password=N'{txtPassword.Password.ToString()}'", out srResult);

            if (dsUser.Tables.Count == 0)
            {
                MessageBox.Show("incorrect username or password is entered");
                return;
            }
            if (dsUser.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("incorrect username or password is entered");
                return;
            }
            MessageBox.Show("successfull");
        }

        private void btnUnsafeLogin_Click(object sender, RoutedEventArgs e)
        {
            UnsafeLoginCode();
        }

        private void btnSafeUpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            //parameterized queries are safe
            string cmdStr = "update tblUsers set Password=@NewPassword where Username=@Username and Password=@Password";

            int irUpdatedRowCount = 0;

            using (SqlConnection connection = new SqlConnection(dbConnection.srConnectionString))
            using (SqlCommand command = new SqlCommand(cmdStr, connection))
            {
                command.Parameters.AddWithValue("@Username", txtUsername.Text);
                command.Parameters.AddWithValue("@Password", txtPassword.Password.ToString());
                command.Parameters.AddWithValue("@NewPassword", txtNewPassword.Password.ToString());
                connection.Open();

                irUpdatedRowCount = command.ExecuteNonQuery();
            }

            if (irUpdatedRowCount == 0)
            {
                MessageBox.Show("incorrect username or password is entered");
                return;
            }

            MessageBox.Show("successfully updated password");
        }
    }
}
