using System;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ch2_sqli_fuzzer
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0 || !File.Exists(args[0]))
            {
                Console.WriteLine("Usage: <program> <path_to_request_file>");
                return;
            }

            string[] requestLines = File.ReadAllLines(args[0]);
            string[] parms = requestLines[requestLines.Length - 1].Split('&');
            string host = string.Empty;
            StringBuilder requestBuilder = new StringBuilder();

            foreach (string ln in requestLines)
            {
                if (ln.StartsWith("Host:"))
                    host = ln.Split(' ')[1].Replace("\r", string.Empty);
                requestBuilder.Append(ln + "\n");
            }

            if (string.IsNullOrEmpty(host))
            {
                Console.WriteLine("Host header is missing or invalid in the request file.");
                return;
            }

            IPAddress[] addresses = Dns.GetHostAddresses(host);
            if (addresses.Length == 0)
            {
                Console.WriteLine("Failed to resolve host to an IP address.");
                return;
            }
            IPEndPoint rhost = new IPEndPoint(addresses[0], 80);

            string request = requestBuilder.ToString() + "\r\n";

            foreach (string parm in parms)
            {
                if (!parm.Contains("="))
                {
                    Console.WriteLine("Skipping malformed parameter: " + parm);
                    continue;
                }

                string val = parm.Split('=')[1];
                string req = request.Replace("=" + val, "=" + val + "'");

                using (Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    try
                    {
                        sock.Connect(rhost);

                        byte[] reqBytes = Encoding.ASCII.GetBytes(req);
                        sock.Send(reqBytes);

                        StringBuilder responseBuilder = new StringBuilder();
                        byte[] buf = new byte[1024];
                        int bytesReceived;
                        do
                        {
                            bytesReceived = sock.Receive(buf);
                            responseBuilder.Append(Encoding.ASCII.GetString(buf, 0, bytesReceived));
                        } while (bytesReceived > 0);

                        string response = responseBuilder.ToString();

                        if (response.Contains("error in your SQL syntax"))
                            Console.WriteLine("Parameter " + parm + " seems vulnerable to SQL injection with value: " + val + "'");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error during socket operation: " + ex.Message);
                    }
                }
            }
        }
    }
}
