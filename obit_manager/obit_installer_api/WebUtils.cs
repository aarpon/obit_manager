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
    public static class WebUtils
    {

        public static async Task DownloadAsync(string url, string filename, string webProxyAddress = "")
        {
            if (url == null)
                throw new ArgumentNullException("Please specify the request URI");

            Uri requestUri = new Uri(url);

            if (filename == null)
                throw new ArgumentNullException("Please specify a filename.");

            Lazy<IWebProxy> proxy = new Lazy<IWebProxy>(
                () => string.IsNullOrEmpty(webProxyAddress) ?
                null :
                new WebProxy { Address = new Uri(webProxyAddress), UseDefaultCredentials = true });

            if (proxy != null)
            {
                WebRequest.DefaultWebProxy = proxy.Value;
            }

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

