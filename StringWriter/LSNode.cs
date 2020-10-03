using System;
using System.Collections.Generic;
using System.Text;

namespace StringWriter
{
    class LSNode
    {
        public string _id { get; set; }
        public string _rev { get; set; }
        public string _key { get; set; }
        public string Name { get; set; }
        public string RouterID { get; set; }
        public int ASN { get; set; }
        public int SRGBStart { get; set; }
        public int SRGBRange { get; set; }
        public int SRCapabilityFlags { get; set; }
        public string IGPID { get; set; }
        public string NodeMaxSIDDepth { get; set; }
        public string AreaID { get; set; }
        public string Protocol { get; set; }
    }
}
