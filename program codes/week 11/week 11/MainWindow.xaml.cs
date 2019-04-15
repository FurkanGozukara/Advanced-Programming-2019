using System;
using System.Collections.Generic;
using System.IO;
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

namespace week_11
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

        private void btnSelectfile_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new Microsoft.Win32.OpenFileDialog();
            var result = ofd.ShowDialog();
            if (result == false) return;
            txtFilePath.Text = ofd.FileName;
        }

        private void btnSHA_Click(object sender, RoutedEventArgs e)
        {
            string srPath = txtFilePath.Text;

            var t = new Task(() =>
            {
                calculateHash(srPath);
            });

            t.Start();
        }

        private void calculateHash(string srFilePath)
        {
            if (!File.Exists(srFilePath))
            {
                MessageBox.Show("invalid file path is chosen");
                return;
            }

            string srFileHashLog = $"File: {srFilePath} SHA256 Hash \r\n{Cryptology.ComputeSha256HashFromFile(srFilePath)}";

            Dispatcher.BeginInvoke((Action)delegate
            {
                lstLogs.Items.Insert(0, srFileHashLog);
            });
        }
    }
}
