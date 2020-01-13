using System;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.Linq;
using Microsoft.VisualBasic.FileIO;

namespace SIPTranslation
{
    class Program
    {
        static void Main(string[] args)
        {

            //fields[0]  =  DisplayName
            //fields[1]  =  Extension
            //fields[2]  =  UserID
            //fields[3]  =  Password
            //fields[4]  =  Server
            //fields[5]  =  Port
            //fields[6]  =  MAC

            //made with love by Ben Cornwell
            //
            //
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Ben's CSV to .cfg (.xml) translation tool.");
            string[] csvString = File.ReadAllLines(@"C:\Temp\CSV_Phone.csv");

            Console.WriteLine(csvString[0]);
            Console.WriteLine("++++++++++++++++");
            Console.WriteLine(csvString[1]);

            string path = @"c:\Temp\CFGs";

            try
            {
                if (Directory.Exists(path))
                {
                    Console.WriteLine("That path exists already.");
                    return;
                }

                DirectoryInfo di = Directory.CreateDirectory(path);
                Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));

            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            finally { }

            foreach (string line in csvString)
            {
                string[] fields = line.Split(',');
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("TEST: 0 = {0}, 1 = {1}, 2 = {2}", fields[0], fields[1], fields[2]);
                int localPort = Convert.ToInt32(fields[1]) - 1000;
                Console.ResetColor();
                var cfg = new XDocument(
                    new XDeclaration("1.0", "UTF-8", "yes"),
                    new XElement("PHONE_CONFIG",
                        new XElement("ALL",
                        new XAttribute("voIpProt.SIP.local.port", localPort.ToString()),
                        new XAttribute("msg.mwi.1.callBack", fields[1]),
                        new XAttribute("msg.mwi.1.callBackMode", "contact"),
                        new XAttribute("msg.mwi.1.subscribe", fields[1]),
                        new XAttribute("reg.1.address", fields[2]),
                        new XAttribute("reg.1.auth.userId", fields[2]),
                        new XAttribute("reg.1.auth.password", fields[3]),
                        new XAttribute("reg.1.displayName", fields[0]),
                        new XAttribute("reg.1.label", fields[1]),
                        new XAttribute("reg.1.callsPerLineKey", "12"),
                        new XAttribute("reg.1.outboundProxy.address", fields[4]),
                        new XAttribute("reg.1.lineKeys", "1"),
                        new XAttribute("reg.1.outboundProxy.port", fields[5]),
                        new XAttribute("reg.1.outboundProxy.transport", "UDPOnly"),
                        new XAttribute("reg.1.server.1.expires", "300"),
                        new XAttribute("reg.1.server.1.address", fields[4]),
                        new XAttribute("reg.1.server.1.port", fields[5]),
                        new XAttribute("reg.1.server.1.transport", "UDPOnly")
                    )
                    )
                    );
                Console.WriteLine(cfg);
                cfg.Save(@"c:\Temp\CFGs\" + fields[6] + "-sip.cfg");
                Console.WriteLine("+==========================+");
                Console.ResetColor();
                //string mac = 
            }
        }
    }
}
