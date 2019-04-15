using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace week_11_web_crawler
{
    public static class HTTPDownloader
    {
        public class WebPageDownloadResult
        {
            public HttpStatusCode httpStatusResult;
            public string srCrawledPageSource = "";
            public Exception occuredException = null;
        }

        public static WebPageDownloadResult FuncCrawlGivenURL(string srCrawlURL)
        {
            WebPageDownloadResult tempResult = new WebPageDownloadResult();

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(srCrawlURL);

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    tempResult.httpStatusResult = response.StatusCode;
                    String ver = response.ProtocolVersion.ToString();
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        tempResult.srCrawledPageSource = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception E)
            {
                tempResult.occuredException = E;
            }

            return tempResult;
        }
    }
}
