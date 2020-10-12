using System;
using System.Collections.Generic;
using System.Text;

namespace StringWriter
{
    class LSLink
    {
        public string _id { get; set; }
        public string _rev { get; set; }
        public string _key { get; set; }
        public string _from { get; set; }
        public string _to { get; set; }
        public string LocalRouterID { get; set; }
        public string LocalIGPID { get; set; }
        public string RemoteRouterID { get; set; }
        public string RemoteIGPID { get; set; }
        public string Protocol { get; set; }
        public int ASN { get; set; }
        public string FromInterfaceIP { get; set; }
        public string ToInterfaceIP { get; set; }
        public int IGPMetric { get; set; }
        public int TEMetric { get; set; }
        public long MaxLinkBW { get; set; }
        public int unidir_link_delay { get; set; }
        public int unidir_delay_variation { get; set; }
        public int unidir_packet_loss { get; set; }
        public int packet_loss { get; set; }
        public int unidir_residual_bw { get; set; }
        public int unidir_available_bw { get; set; }
        public int unidir_bw_utilization { get; set; }
        public IList<AdjacencySID> AdjacencySID { get; set; }

    }
}
