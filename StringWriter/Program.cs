using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace StringWriter
{
    class Program
    {
        //ADJUST FOR DIFFERENT TOPOLOGIES
        const int NumberOfNodes = 100;
        const int NumberOfIntraRegionalLinks = 50;
        const int NumberOfInterRegionalLinks = 3;
        const string CollectionOfNodeName = "LSNodeMocked100/";
        static readonly string[] regions = { "Zuerich", "Bern", "Genf" };


        static readonly Dictionary<string, List<LSNode>> RegionToNodes = new Dictionary<string, List<LSNode>>();
        static int idCounter = 1;
        static int ipCounter = 1;

        static List<LSNode> nodeList = new List<LSNode>();
        static List<LSLink> linkList = new List<LSLink>();

        static void Main(string[] args)
        {            
            foreach (var region in regions)
            {
                RegionToNodes.Add(region, new List<LSNode>());
            }

            GenerateNodes();
            GenerateIntraRegionalLinks();
            GenerateInterRegionalLinks();

            var jsonStringSerializeMockedNodes = JsonSerializer.Serialize(nodeList);
            File.WriteAllText("C:\\Users\\dilli\\Desktop\\LSNode_Mocked.json", jsonStringSerializeMockedNodes);

            var jsonStringSerializeMockedLinks = JsonSerializer.Serialize(linkList);
            File.WriteAllText("C:\\Users\\dilli\\Desktop\\LSLink_Mocked.json", jsonStringSerializeMockedLinks);
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
                node.LoopId = i;
                node.RegionCustom = regions[i%regions.Length];

                List<LSNode> regionsList;
                if (!RegionToNodes.TryGetValue(regions[i % regions.Length], out regionsList))
                {
                    Console.WriteLine("Error");
                }

                nodeList.Add(node);
                regionsList.Add(node);
            }
        }

        private static void CreateLink(int from, int to, int fromIp, int toIp)
        {
            LSLink link = new LSLink();
            link._id = idCounter.ToString();
            link._key = idCounter.ToString();
            link._from = CollectionOfNodeName + from + "." + from + "." + from + "." + from;
            link._to = CollectionOfNodeName + to + "." + to + "." + to + "." + to;
            link.LocalRouterID = from + "." + from + "." + from + "." + from;
            link.RemoteRouterID = to + "." + to + "." + to + "." + to;
            link.FromInterfaceIP = fromIp.ToString();
            link.ToInterfaceIP = toIp.ToString();
            link.Protocol = "IS-IS Level 2";
            link.ASN = 64075;
            link.TEMetric = 1;
            link.MaxLinkBW = 1290693416;
            GenerateRandomLinkValues(link);
            idCounter++;           
            linkList.Add(link);
        }

        private static void GenerateInterRegionalLinks()
        {
            for (int i = 0; i < RegionToNodes.Count; i++)
            {
                
                for (int j = i+1; j < RegionToNodes.Count; j++)
                {
                    for (int k = 0; k < NumberOfInterRegionalLinks; k++)
                    {
                        var from = RegionToNodes.Values.ElementAt(i)[k].LoopId;
                        var to = RegionToNodes.Values.ElementAt(j)[k].LoopId;
                        CreateLink(from, to, ipCounter, ipCounter+1);
                        CreateLink(to, from, ipCounter+1, ipCounter);
                        ipCounter += 2;
                    }
                }
            }
        }

        private static void GenerateIntraRegionalLinks()
        {
            foreach (string region in regions)
            {
                int linkCounter = NumberOfIntraRegionalLinks;
                List<LSNode> regionNodes;
                if (!RegionToNodes.TryGetValue(region, out regionNodes)){
                    Console.WriteLine("Error");
                }

                for (int i = 0; i < regionNodes.Count; i++)
                {
                    int from = regionNodes[i].LoopId;
                    var to = regionNodes[(i+1) % regionNodes.Count].LoopId;

                    CreateLink(from, to, ipCounter, ipCounter+1);
                    CreateLink(to, from, ipCounter+1, ipCounter);
                    ipCounter += 2;
                    linkCounter--;
                }

                //random links
                for (int i = linkCounter; i > 0; i--)
                {
                    var r = new Random();
                    var fromRandom = r.Next(0, regionNodes.Count);
                    var toRandom = r.Next(0, regionNodes.Count);

                    while (fromRandom == toRandom)
                    {
                        toRandom = r.Next(0, regionNodes.Count);
                    }

                    int from = regionNodes[fromRandom].LoopId;
                    var to = regionNodes[toRandom].LoopId;

                    CreateLink(from, to, ipCounter, ipCounter+1);
                    CreateLink(to, from, ipCounter+1, ipCounter);
                    ipCounter+=2;

                    linkCounter--;
                }
            }
        }

        private static int GenerateRandomIGPMetric()
        {
            Random r = new Random();
            return r.Next(1, 10);
        }

        private static void GenerateRandomLinkValues(LSLink link)
        {
            link.IGPMetric = GenerateRandomIGPMetric();
            link.unidir_available_bw = GenerateRandomValue();
            link.unidir_bw_utilization = GenerateRandomValue();
            link.unidir_delay_variation = GenerateRandomValue();
            link.unidir_link_delay = GenerateRandomValue();
            link.unidir_packet_loss = GenerateRandomValue();
            link.unidir_residual_bw = GenerateRandomValue();
        }
    }
}
