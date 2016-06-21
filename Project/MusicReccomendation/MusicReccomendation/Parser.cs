using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace MusicReccomendation
{
    
    class Parser
    {

        XmlReader reader;
        string allText;

        public Parser()
        {
            
           
        }

        

        public void readXmlAsString(string xmlPath)
        {
            allText = System.IO.File.ReadAllText(@xmlPath);
            reader = XmlReader.Create(allText);

        }

        public void parseXML(string path)
        {
            //reader.ReadToFollowing("Entry");
            XDocument doc = XDocument.Load(path);

            List<Song> songs = doc.Root.Element("COLLECTION")
                              .Elements("ENTRY")
                              .Select(x => createSongFromElement(x))
                              .ToList();
            
            Console.Write(songs);
        }

        private Song createSongFromElement(XElement x)
        {
            Song song = new Song();
            //id??
            song.title = (string)x.Attribute("TITLE");
            song.artist = (string)x.Attribute("ARTIST");
            string strgenre = (string)x.Element("INFO").Attribute("GENRE");
            char[] dels = new char[2];
            dels[0] = '/';
            dels[1] = ' ';
            song.genre = strgenre.Split(dels, StringSplitOptions.RemoveEmptyEntries).ToList();
            string scount = (string)x.Element("INFO").Attribute("PLAYCOUNT");
            int count = 0;
            if (scount != null)
                Int32.TryParse(scount, out count);
            song.playCount = count;
            string tempo = (string)x.Element("TEMPO").Attribute("BPM")
            float bpm = 90.0;
            if (tempo != null)
                float.TryParse(tempo, out bpm);
            song.bpm = bpm;
            return song;
        }


            
    }
}
