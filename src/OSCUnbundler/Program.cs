using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using OSCforPCL;

namespace OSCUnbundler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            JObject settings = JObject.Parse(File.ReadAllText(@"..\..\settings.json"));
            int listenPort = settings["listenPort"].Value<int>();
            string destination = settings["destination"].Value<string>();
            int destinationPort = settings["destinationPort"].Value<int>();
            bool verbose = settings["verbose"].Value<bool>();

            OSCServer server = new OSCServer(listenPort);
            OSCClient client = new OSCClient(destination, destinationPort);
            
            server.DefaultOnMessageReceived += (sender, messageArgs) =>
            {
                client.Send(messageArgs.Message);
                if(verbose)
                {
                    Console.WriteLine(messageArgs.Message.ToString());
                }
            };

            Console.WriteLine("Listening on port " + listenPort + ", sending to " + destination + " on port " + destinationPort);
            while(true)
            {
                //do nothing
            };
        }
    }
}
