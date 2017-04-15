using fidoBackend.Models;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace fidoBackend.Services
{
    public class BaseService
    {
        public static MobileServiceClient MobileService =
  new MobileServiceClient(
      "https://fidocore.azurewebsites.net"
  );

        public static async Task<Status> HttpGetOperation(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://fidocore.azurewebsites.net/api/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var Status = new Status();
                        Status.data = json;
                        return Status;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
