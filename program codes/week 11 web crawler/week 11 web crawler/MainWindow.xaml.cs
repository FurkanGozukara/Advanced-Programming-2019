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
using System.IO;

namespace week_11_web_crawler
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

        private static string sourceFilesDirectory = "downloaded_source_codes";

        private void btnStartCrawling_Click(object sender, RoutedEventArgs e)
        {
            Directory.CreateDirectory(sourceFilesDirectory);

            string srUrlHash = Cryptology.ComputeSha256HashFromString(txtRootUrl.Text);
            string srDownloadedFileSaveName = sourceFilesDirectory + "/" + srUrlHash + ".txt";

            if (File.Exists(srDownloadedFileSaveName))
                return;

            HTTPDownloader.WebPageDownloadResult myDownloadResult = HTTPDownloader.FuncCrawlGivenURL(txtRootUrl.Text);

            if (myDownloadResult.httpStatusResult == System.Net.HttpStatusCode.OK)
            {
                File.WriteAllText(srDownloadedFileSaveName, myDownloadResult.srCrawledPageSource);
            }
        }
    }
}
