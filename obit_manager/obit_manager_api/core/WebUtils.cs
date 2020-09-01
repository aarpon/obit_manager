using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace obit_manager_api.core
{
    public static class WebUtils
    {
        /// <summary>
        /// Downloads a file from given URL to local file.
        /// </summary>
        /// 
        /// The function checks for the existance of the target file before attempting download.
        /// If the target file already exists, the function returns immediately. This is done so
        /// that all downloads can be started in parallel and *awaited* without the complication
        /// of managing varying subsets of downloads to be queued and without any performance 
        /// penalty.
        /// 
        /// <param name="url">URL of the file to download.</param>
        /// <param name="filename">Full path to the local file where the stream is to be saved.</param>
        /// <param name="webProxyAddress">If needed, full address (with port) of the proxy server to use.</param>
        /// <returns></returns>
        public static async Task DownloadAsync(string url, string filename, string webProxyAddress = "")
        {
            // Check the url
            if (url == null)
            {
                throw new ArgumentNullException("Please specify the request URI");
            }

            Uri requestUri = new Uri(url);

            // Check the file name
            if (filename == null)
            {
                throw new ArgumentNullException("Please specify a filename.");
            }

            // Does the target file already exist?
            if (File.Exists(filename))
            {
                return;
            }

            // Set a proxy if needed
            // @TODO: Properly manage the credentials
            Lazy<IWebProxy> proxy = new Lazy<IWebProxy>(
                () => string.IsNullOrEmpty(webProxyAddress) ?
                null :
                new WebProxy { Address = new Uri(webProxyAddress), UseDefaultCredentials = true });

            // Set the proxy, if it is configured
            if (proxy != null)
            {
                WebRequest.DefaultWebProxy = proxy.Value;
            }

            // Relax the security a bit
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 |
                SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            // Download the file and save it to disk
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, requestUri))
                {
                    using (
                        Stream contentStream = await (await httpClient.SendAsync(request)).Content.ReadAsStreamAsync(),
                        stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None, 4194304, true))
                    {
                        await contentStream.CopyToAsync(stream);
                    }
                }
            }
        }
    }
}

