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
            if (!url.Contains("?"))
            {
                Console.WriteLine("No query parameters found in the URL.");
                return;
            }

            // Extract query parameters
            int queryIndex = url.IndexOf("?");
            string baseUrl = url.Substring(0, queryIndex);
            string[] parameters = url.Substring(queryIndex + 1).Split('&');

            foreach (string parameter in parameters)
            {
                try
                {
                    // Generate fuzzed URLs
                    string xssPayload = Uri.EscapeDataString("fd<xss>sa");
                    string sqlPayload = Uri.EscapeDataString("fd'sa");

                    string xssUrl = GenerateFuzzedUrl(baseUrl, parameters, parameter, xssPayload);
                    string sqlUrl = GenerateFuzzedUrl(baseUrl, parameters, parameter, sqlPayload);

                    // Log fuzzed URLs
                    Console.WriteLine("Fuzzing URLs:");
                    Console.WriteLine($"XSS URL: {xssUrl}");
                    Console.WriteLine($"SQL URL: {sqlUrl}");

                    // Test for vulnerabilities
                    TestUrlForVulnerabilities(xssUrl, parameter, "<xss>", "Possible XSS vulnerability detected");
                    TestUrlForVulnerabilities(sqlUrl, parameter, "error in your SQL syntax", "Possible SQL Injection vulnerability detected");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing parameter {parameter}: {ex.Message}");
                }
            }
        }

        private static string GenerateFuzzedUrl(string baseUrl, string[] parameters, string targetParameter, string payload)
        {
            string fuzzedParameter = targetParameter + payload;
            string[] updatedParameters = Array.ConvertAll(parameters, param => param == targetParameter ? fuzzedParameter : param);
            return $"{baseUrl}?{string.Join("&", updatedParameters)}";
        }

        private static void TestUrlForVulnerabilities(string url, string parameter, string searchText, string vulnerabilityMessage)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string responseText = reader.ReadToEnd();
                    if (responseText.Contains(searchText))
                    {
                        Console.WriteLine($"{vulnerabilityMessage} in parameter: {parameter}");
                    }
                }
            }
            catch (WebException webEx)
            {
                Console.WriteLine($"Error testing URL {url}: {webEx.Message}");
                LogServerResponse(webEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error testing URL {url}: {ex.Message}");
            }
        }

        private static void LogServerResponse(WebException webEx)
        {
            if (webEx.Response != null)
            {
                using (StreamReader reader = new StreamReader(webEx.Response.GetResponseStream()))
                {
                    string response = reader.ReadToEnd();
                    Console.WriteLine("Server response:");
                    Console.WriteLine(response);
                }
            }
        }
    }
}
