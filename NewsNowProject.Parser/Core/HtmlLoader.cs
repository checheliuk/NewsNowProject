using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace NewsNowProject.Parser.Core
{
    public class HtmlLoader
    {
        readonly HttpClient client;
        readonly string url;

        public HtmlLoader(string url)
        {
            client = new HttpClient();
            this.url = $"{url}";
        }

        public HtmlLoader(IParserSettings settings)
        {
            client = new HttpClient();
            url = $"{settings.Url}";
        }

        public HtmlLoader(IParserSettings settings, string proxyAddress)
        {
            var httpClientHandler = new HttpClientHandler
            {
                Proxy = new WebProxy(proxyAddress, false),
                UseProxy = true
            };

            client = new HttpClient(httpClientHandler);
            url = $"{settings.Url}";
        }

        public async Task<string> GetSource()
        {
            // for vlasno.info
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36 Edge/15.15063");

            var response = await client.GetAsync(url);
            string source = null;

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                if(response.Content.Headers.ContentType.CharSet?.Trim().ToLower() == "cp1251")
                    response.Content.Headers.ContentType.CharSet = "windows-1251";

                source = await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception(url+ " failed with status code: " + response.StatusCode);
            }

            return source;
        }
    }
}