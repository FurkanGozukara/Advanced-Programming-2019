using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

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
                        tempResult.srCrawledPageSource = WebUtility.HtmlDecode(tempResult.srCrawledPageSource);
                    }
                }
            }
            catch (Exception E)
            {
                tempResult.occuredException = E;
            }

            return tempResult;
        }

        public static string ReturnAbsUrl(string srBaseUrl, string relativeUrl, string srMustHave = "")
        {
            Uri newAbsUrl = null;
            Uri urlbase = new Uri(srBaseUrl);

            Uri.TryCreate(urlbase, relativeUrl, out newAbsUrl);

            if (newAbsUrl == null)
                return null;

            Uri uriResult;
            bool result = Uri.TryCreate(newAbsUrl.ToString(), UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (result == false)
                return null;

            string srFinalUrl = newAbsUrl.ToString().Split('#').First();

            if (srMustHave.Length > 0)
                if (srFinalUrl.Contains(srMustHave) == false)
                    return null;

            return srFinalUrl;
        }

        public class csUrlFails
        {
            public string srUrl = "";
            public int irFailCount = 0;
            public DateTime dtPause = DateTime.UtcNow;
        }

        public static void writeFailedUrlsToFile(Dictionary<string, csUrlFails> dicFailedUrls)
        {
            var obj = Newtonsoft.Json.JsonConvert.SerializeObject(dicFailedUrls);

            File.WriteAllText("failedUrls.txt", obj);
        }

        public static void readFailedUrls(ref Dictionary<string, csUrlFails> dicFailedUrls)
        {
            if (!File.Exists("failedUrls.txt"))
                return;

            dicFailedUrls = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, csUrlFails>>(File.ReadAllText("failedUrls.txt"));
        }

        public static string func_GenerateURLHash(this string srUrl)
        {
            return Cryptology.ComputeSha256HashFromString(srUrl);
        }
    }
}
