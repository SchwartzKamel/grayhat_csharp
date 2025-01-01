using System;
using System.Net;
using System.IO;

namespace ch2_dev_get_fuzzer
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: ch2_dev_get_fuzzer <url>");
                return;
            }

            string url = args[0];
            int index = url.IndexOf("?");
            if (index == -1)
            {
                Console.WriteLine("No parameters found in the URL.");
                return;
            }

            string[] parms = url.Substring(index + 1).Split('&');
            foreach (string parm in parms)
            {
                try
                {
                    // Encode the payloads for safe transmission
                    string xssPayload = Uri.EscapeDataString("fd<xss>sa");
                    string sqlPayload = Uri.EscapeDataString("fd'sa");

                    string xssUrl = url.Replace(parm, parm + xssPayload);
                    string sqlUrl = url.Replace(parm, parm + sqlPayload);

                    // Process XSS URL
                    CheckForVulnerabilities(xssUrl, parm, "<xss>", "Possible XSS point found");

                    // Process SQL Injection URL
                    CheckForVulnerabilities(sqlUrl, parm, "You have an error in your SQL syntax", "SQL Injection point found");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing parameter {parm}: {ex.Message}");
                }
            }
        }

        private static void CheckForVulnerabilities(string url, string parameter, string searchText, string vulnerabilityMessage)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (StreamReader rdr = new StreamReader(response.GetResponseStream()))
                {
                    string responseText = rdr.ReadToEnd();
                    if (responseText.Contains(searchText))
                    {
                        Console.WriteLine($"{vulnerabilityMessage} in parameter: {parameter}");
                    }
                }
            }
            catch (WebException webEx)
            {
                Console.WriteLine($"Error checking URL {url}: {webEx.Message}");

                // Enhanced error logging for server responses
                if (webEx.Response != null)
                {
                    using (StreamReader rdr = new StreamReader(webEx.Response.GetResponseStream()))
                    {
                        Console.WriteLine($"Server response: {rdr.ReadToEnd()}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error checking URL {url}: {ex.Message}");
            }
        }
    }
}
