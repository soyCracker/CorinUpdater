using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CorinUpdater.Lib.Util
{
    public class HttpUtil
    {
        private static readonly HttpClient httpClient = new HttpClient();

        static HttpUtil()
        {
            httpClient.DefaultRequestHeaders.Add("User-Agent", "CorinUpdater");
        }


        public static async Task<string> Get(string url)
        {
            try
            {               
                HttpResponseMessage result = await httpClient.GetAsync(url);
                if(result.IsSuccessStatusCode)
                {
                    return await result.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "";
        }

        public static async Task<byte[]> DownloadFile(string url)
        {
            try
            {
                HttpResponseMessage result = await httpClient.GetAsync(url);
                if (result.IsSuccessStatusCode)
                {
                    return await result.Content.ReadAsByteArrayAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new byte[0];
        }
    }
}
