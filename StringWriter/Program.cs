using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

namespace StringWriter
{
    class Program
    {
        //Adjust Numbers
        const int NumberOfNodes = 100;
        const int NumberOfLinks = 150;

        static List<LSNode> nodeList = new List<LSNode>();
        static List<LSLink> linkList = new List<LSLink>();

        static void Main(string[] args)
        {
            //Create LSLink Objects from JSON, change the attributes and write them to JSON again
            //string fileName = "C:\\Users\\dilli\\Desktop\\LSLink_KOMPLETT.json";
            //string jsonString = File.ReadAllText(fileName);
            //string jsonStringSerialize;
            //var list = JsonSerializer.Deserialize<List<LSLink>>(jsonString);

            //foreach (LSLink link in list)
            //{
            //    RandomizeLinkCounters(link);
            //}

            ////TODO: serialize to JSON file
            //jsonStringSerialize = JsonSerializer.Serialize(list);
            //File.WriteAllText("C:\\Users\\dilli\\Desktop\\LSLink_adjusted.json", jsonStringSerialize);
            //Console.WriteLine("Finished");


            //UNCOMMENT TO CREATE MOCKED STUFF FOR SCALABILITY TESTS 

            //Creat MockedNodes and MockedLinks
            GenerateNodes();
            GenerateLinks();

            //Write JSON for MockedNodes
            var jsonStringSerializeMockedNodes = JsonSerializer.Serialize(nodeList);
            File.WriteAllText("C:\\Users\\dilli\\Desktop\\LSNode_Mocked.json", jsonStringSerializeMockedNodes);

            //Write JSON for MockedLinks
            var jsonStringSerializeMockedLinks = JsonSerializer.Serialize(linkList);
            File.WriteAllText("C:\\Users\\dilli\\Desktop\\LSLink_Mocked.json", jsonStringSerializeMockedLinks);
        }

        private static void RandomizeLinkCounters(LSLink link)
        {
            link.unidir_available_bw = GenerateRandomValue();
            link.unidir_bw_utilization = GenerateRandomValue();
            link.unidir_delay_variation = GenerateRandomValue();
            link.unidir_link_delay = GenerateRandomValue();
            link.unidir_packet_loss = GenerateRandomValue();
            link.unidir_residual_bw = GenerateRandomValue();
        }

        private static int GenerateRandomValue()
        {
            Random r = new Random();

            if (r.NextDouble() < 0.65)
            {
                return 0;
            }
            return r.Next(0, 10);
        }

        private static void GenerateNodes()
        {
            for (int i = 1; i <= NumberOfNodes; i++)
            {
                var node = new LSNode();

                //SET PROPERTIES
                node._id = i + "." + i + "." + i + "." + i;
                node._key = i + "." + i + "." + i + "." + i;
                node.Name = "XR-" + i;
                node.RouterID = i + "." + i + "." + i + "." + i;
                node.ASN = 64075;
                node.SRGBStart = 16000;
                node.SRGBRange = 8000;
                node.SRCapabilityFlags = 128;
                node.IGPID = "0000.0000." + i;
                node.NodeMaxSIDDepth = "1:10";
                node.AreaID = "49.0001";
                node.Protocol = "IS-IS Level 2";

                nodeList.Add(node);
            }
        }

        private static void GenerateLinks()
        {
            Random r = new Random();
            int idCounter = 1; //goes till NumberOfNode * 2

            //Loop over NumberOfNodes (amount of physical links)
            for (int i = 0; i < NumberOfLinks; i++)
            {
                //Generate first unidir link of physical link 1
                var link1 = new LSLink();
                var from1 = r.Next(1, NumberOfNodes);
                var to1 = r.Next(1, NumberOfNodes);
                link1._id = idCounter.ToString();
                link1._key = idCounter.ToString();
                link1.LocalRouterID = from1 + "." + from1 + "." + from1 + "." + from1;
                link1.RemoteRouterID = to1 + "." + to1 + "." + to1 + "." + to1;
                link1.Protocol = "IS-IS Level 2";
                link1.ASN = 64075;
                link1.TEMetric = 1;
                link1.MaxLinkBW = 1290693416;
                GenerateRandomLinkValues(link1);

                idCounter++;

                //Create second unidir link of physical link 1
                var link2 = new LSLink();
                var from2 = to1;
                var to2 = from1;
                link2._id = "" + idCounter + "";
                link2._key = "" + idCounter + ""; ;
                link2.LocalRouterID = from2 + "." + from2 + "." + from2 + "." + from2;
                link2.RemoteRouterID = to2 + "." + to2 + "." + to2 + "." + to2;
                link2.Protocol = "IS-IS Level 2";
                link2.ASN = 64075;
                link2.TEMetric = 1;
                link2.MaxLinkBW = 1290693416;
                GenerateRandomLinkValues(link2);

                linkList.Add(link1);
                linkList.Add(link2);

                idCounter++;
            }
        }

        private static void GenerateRandomLinkValues(LSLink link)
        {
            link.unidir_available_bw = GenerateRandomValue();
            link.unidir_bw_utilization = GenerateRandomValue();
            link.unidir_delay_variation = GenerateRandomValue();
            link.unidir_link_delay = GenerateRandomValue();
            link.unidir_packet_loss = GenerateRandomValue();
            link.unidir_residual_bw = GenerateRandomValue();
        }
    }
}
