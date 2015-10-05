using System;
using System.Xml.Serialization;

namespace MarcBachraty.Classes.FB
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class entry
    {
        /// <remarks />
        public string id { get; set; }

        /// <remarks />
        public entryLink link { get; set; }

        /// <remarks />
        public string published { get; set; }

        /// <remarks />
        public string updated { get; set; }

        /// <remarks />
        public entryContent content { get; set; }

        /// <remarks />
        public string title { get; set; }

        /// <remarks />
        public entryAuthor author { get; set; }
    }

    public class BannerEvent:entry
    {
        public string StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string SubHeader { get; set; }
        public string Country { get; set; }


    }

    public class BannerItem:BannerEvent
    {
        public string MediaUrl { get; set; } 
        public string TypeOfContent { get; set; } 
      
    }



}