using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace obit_manager_api
{
    namespace core
    {
        public static class WebUtils
        {
            public static async Task DownloadAsync(string url, string filename, string webProxyAddress = "")
            {
                // Check the url
                if (url == null)
                    throw new ArgumentNullException("Please specify the request URI");

                Uri requestUri = new Uri(url);

                // Check the file name
                if (filename == null)
                    throw new ArgumentNullException("Please specify a filename.");

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
}

