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
using HtmlAgilityPack;
using System.Diagnostics;

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

            string srBaseUrl = txtRootUrl.Text;

            HTTPDownloader.WebPageDownloadResult myDownloadResult = new HTTPDownloader.WebPageDownloadResult();

            if (File.Exists(srDownloadedFileSaveName))
            {
                myDownloadResult.srCrawledPageSource = File.ReadAllText(srDownloadedFileSaveName);
            }
            else
            {
                myDownloadResult = HTTPDownloader.FuncCrawlGivenURL(srBaseUrl);

                if (myDownloadResult.httpStatusResult == System.Net.HttpStatusCode.OK)
                {
                    File.WriteAllText(srDownloadedFileSaveName, myDownloadResult.srCrawledPageSource);
                }
            }

            HtmlDocument hdDoc = new HtmlDocument();
            hdDoc.LoadHtml(myDownloadResult.srCrawledPageSource);

            var links = hdDoc.DocumentNode.SelectNodes("//a");

            foreach (var vrNode in links)
            {
                if (vrNode.Attributes["href"] != null)
                {
                    string srNewAbsLink = vrNode.Attributes["href"].Value.ToString();
                    srNewAbsLink = HTTPDownloader.ReturnAbsUrl(srBaseUrl, srNewAbsLink);
                    Debug.WriteLine(srNewAbsLink);
                }
            }
        }


    }
}
