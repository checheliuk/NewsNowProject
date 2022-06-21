using NewsNowProject.Domain.ViewModel;
using System;
using System.Net.Http;
using System.Web.Script.Serialization;

namespace NewsNowProject.ProxyListApi.Core
{
    public class ProxyListWorkers
    {
        private string Url { get; set; } = "https://api.getproxylist.com/proxy?protocol[]=http";

        public ProxyListViewModel GetResponse()
        {
            using (var client = new HttpClient())
            {
                var uri = new Uri(Url);
                var response = client.GetAsync(uri).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    var jsHelper = new JavaScriptSerializer();

                    return jsHelper.Deserialize<ProxyListViewModel>(content); ;
                }
                else
                {
                    throw new Exception("ProxyListApi failed with status code: " + response.StatusCode);
                }
            }
        }
    }
}
