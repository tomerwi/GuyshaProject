using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MusicReccomendation
{
    
    class Parser
    {

        XmlReader reader;
        string allText;

        public Parser(string xmlPath)
        {
            readXmlAsString(xmlPath);
           
        }

        

        public void readXmlAsString(string xmlPath)
        {
            allText = System.IO.File.ReadAllText(@xmlPath);
            reader = XmlReader.Create(allText);

        }

        public void parseXML()
        {
            reader.ReadToFollowing("Entry");
        }


            
    }
}
