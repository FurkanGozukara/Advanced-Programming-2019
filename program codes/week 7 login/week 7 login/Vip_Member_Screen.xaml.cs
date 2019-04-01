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
using System.Windows.Shapes;

namespace week_7_login
{
    /// <summary>
    /// Interaction logic for Vip_Member_Screen.xaml
    /// </summary>
    public partial class Vip_Member_Screen : Window
    {
        public Vip_Member_Screen()
        {
            InitializeComponent();
            logged_userid.Content = "Logged User ID: " + Global_Variables.irLogged_User_UserId + "\t\tLogged User Rank: " + Global_Variables.irLogged_User_Rank;
        }
    }
}
