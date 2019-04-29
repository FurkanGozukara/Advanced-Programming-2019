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

        private static string srLinksDiscoveredPath = "discovered_links.txt";
        private static string srCrawledLinksPath = "crawled_links.txt";

        private static HashSet<string> hsDiscoveredLinks = new HashSet<string>();
        private static HashSet<string> hsCrawledLinks = new HashSet<string>();

        private static Dictionary<string, HTTPDownloader.csUrlFails> dicFailedUrls = 
            new Dictionary<string, HTTPDownloader.csUrlFails>();


        public MainWindow()
        {
            InitializeComponent();
        }

        private static string sourceFilesDirectory = "downloaded_source_codes";

        private void btnStartCrawling_Click(object sender, RoutedEventArgs e)
        {
            Directory.CreateDirectory(sourceFilesDirectory);

            if (File.Exists(srCrawledLinksPath))
            {
                foreach (var item in File.ReadLines(srCrawledLinksPath))
                {
                    hsCrawledLinks.Add(item);
                }
            }

            if (File.Exists(srLinksDiscoveredPath))
            {
                foreach (var item in File.ReadLines(srLinksDiscoveredPath))
                {
                    if (hsCrawledLinks.Contains(item))
                        continue;
                    hsDiscoveredLinks.Add(item);
                }
            }

            HTTPDownloader.readFailedUrls(ref dicFailedUrls);

            hsDiscoveredLinks.Add(txtRootUrl.Text);

            while (true)
            {
                if (hsDiscoveredLinks.Count == 0)
                    break;
                string srNextUrl = "";
                List<string> lstRemove = new List<string>();
                foreach (var item in hsDiscoveredLinks)
                {
                    if (dicFailedUrls.ContainsKey(item.func_GenerateURLHash()))
                    {
                        if (dicFailedUrls[item.func_GenerateURLHash()].irFailCount >= 3)
                        {
                            if (dicFailedUrls[item.func_GenerateURLHash()].dtPause.AddHours(24) < DateTime.Now)
                            {
                                continue;
                            }
                            else
                            {
                                dicFailedUrls.Remove(item.func_GenerateURLHash());
                            }
                        }
                    }

                    if (hsCrawledLinks.Contains(item))
                        lstRemove.Add(item);
                    else
                    {
                        srNextUrl = item;
                        break;
                    }
                }
                foreach (var item in lstRemove)
                {
                    hsDiscoveredLinks.Remove(item);
                }
                if (srNextUrl.Length == 0)
                    break;

                crawlGivenURL(srNextUrl);
            }
        }



        private static void crawlGivenURL(string srCrawlURL)
        {
            string srUrlHash = srCrawlURL.func_GenerateURLHash();
            string srDownloadedFileSaveName = sourceFilesDirectory + "/" + srUrlHash + ".txt";

            string srBaseUrl = srCrawlURL;

            HTTPDownloader.WebPageDownloadResult myDownloadResult = new HTTPDownloader.WebPageDownloadResult();

            if (File.Exists(srDownloadedFileSaveName))
            {
                myDownloadResult.srCrawledPageSource = File.ReadAllText(srDownloadedFileSaveName);
            }
            else
            {
                myDownloadResult = HTTPDownloader.FuncCrawlGivenURL(srBaseUrl);

                if (myDownloadResult.occuredException != null)
                {
                    File.AppendAllText("errors.txt", srBaseUrl + "\r\n" + myDownloadResult.occuredException.StackTrace + "\r\n\r\n\r\n");

                    if(dicFailedUrls.ContainsKey(srBaseUrl.func_GenerateURLHash()))
                    {
                        dicFailedUrls[srBaseUrl.func_GenerateURLHash()].irFailCount++;
                        dicFailedUrls[srBaseUrl.func_GenerateURLHash()].dtPause = DateTime.Now;
                    }
                    else
                    {
                        dicFailedUrls.Add(srBaseUrl.func_GenerateURLHash(), new HTTPDownloader.csUrlFails { dtPause = DateTime.Now, irFailCount = 1, srUrl = srBaseUrl });
                    }

                    HTTPDownloader.writeFailedUrlsToFile(dicFailedUrls);
                }

                if (myDownloadResult.httpStatusResult == System.Net.HttpStatusCode.OK)
                {
                    hsCrawledLinks.Add(srBaseUrl);
                    File.AppendAllText(srCrawledLinksPath, srBaseUrl + "\r\n");//we add to our crawled url database the new successfully crawled url
                    File.WriteAllText(srDownloadedFileSaveName, myDownloadResult.srCrawledPageSource);
                }
            }

            HtmlDocument hdDoc = new HtmlDocument();
            hdDoc.LoadHtml(myDownloadResult.srCrawledPageSource);

            var links = hdDoc.DocumentNode.SelectNodes("//a");

            List<string> lstDiscoveredLinks = new List<string>();

            if (links != null)
                foreach (var vrNode in links)
                {
                    if (vrNode.Attributes["href"] != null)
                    {
                        string srNewAbsLink = vrNode.Attributes["href"].Value.ToString();
                        srNewAbsLink = HTTPDownloader.ReturnAbsUrl(srBaseUrl, srNewAbsLink, "toros.edu.tr");
                        if (srNewAbsLink == null)
                            continue;
                        lstDiscoveredLinks.Add(srNewAbsLink);
                        hsDiscoveredLinks.Add(srNewAbsLink);
                        Debug.WriteLine(srNewAbsLink);
                    }
                }

            lstDiscoveredLinks = lstDiscoveredLinks.Distinct().ToList();
            File.AppendAllLines(srLinksDiscoveredPath, lstDiscoveredLinks);
        }
    }
}
