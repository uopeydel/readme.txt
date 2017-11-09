using SBXMLCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class TagAttributeModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class XMLTagModel
    {
        public string Index { get; set; }
        public string NameSpace { get; set; }
        public string Name { get; set; }
        public List<TagAttributeModel> Attribute { get; set; }
        public string InnerText { get; set; }
        public bool IsLeaf { get; set; }

        public TElXMLDOMElement XmlElement { get; set; }
    }


    public class ETaxModels
    {
        public List<XMLTagModel> Data { get; set; }
    }
}
