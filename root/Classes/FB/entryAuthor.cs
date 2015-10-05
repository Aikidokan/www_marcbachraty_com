using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcBachraty.Classes.FB
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class entryAuthor
    {

        private string nameField;

        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }
}
