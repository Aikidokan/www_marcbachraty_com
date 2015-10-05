using System.Xml.Serialization;

namespace MarcBachraty.Classes.FB
{
    [XmlType(AnonymousType = true)]
    public class entryLink
    {
        /// <remarks />
        [XmlAttribute]
        public string rel { get; set; }

        /// <remarks />
        [XmlAttribute]
        public string type { get; set; }

        /// <remarks />
        [XmlAttribute]
        public string href { get; set; }
        public string target { get; set; }
    }
}