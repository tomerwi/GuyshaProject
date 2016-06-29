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

        // XmlReader reader;
        // string allText;
       // private int idCounter;
        public Parser()
        {
            //idCounter = 0;
           
        }

        

      /*  public void readXmlAsString(string xmlPath)
        {
            allText = System.IO.File.ReadAllText(@xmlPath);
            reader = XmlReader.Create(allText);

        }*/

        public List<Song> ParseXML(string path)
        {
            //reader.ReadToFollowing("Entry");
            XDocument doc = XDocument.Load(path);

            List<Song> songs = doc.Root.Element("COLLECTION")
                              .Elements("ENTRY")
                              .Select(x => createSongFromElement(x)).Where(x=> x!=null)
                              .ToList();
            
            return songs;
        }

        private Song createSongFromElement(XElement x)
        {
            //idCounter++;
            Song song = new Song();
            //song.id = idCounter;
            XElement info = x.Element("INFO");
            song.key = (string)info.Attribute("KEY");
            if (song.key == null || song.key.Length > 3)
                song.key = "C"; //!!!!!!!!
            song.title = (string)x.Attribute("TITLE");
            if (song.title == null)
                return null;
            song.artist = (string)x.Attribute("ARTIST");
            if (song.artist == null)
                return null;
            string strgenre = (string)info.Attribute("GENRE");
            if (strgenre == null)
                song.genre = new List<string>();
            else
            {
                char[] dels = new char[2];
                dels[0] = '/';
                dels[1] = ' ';
                song.genre = strgenre.Split(dels, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            string scount = (string)info.Attribute("PLAYCOUNT");
            int count = 0;
            if (scount != null)
                Int32.TryParse(scount, out count);
            song.playCount = count;
            XElement tempoElement = x.Element("TEMPO");
            double bpm = 90.0;
            if (tempoElement != null)
            {
                string tempo = (string)tempoElement.Attribute("BPM");
                if (tempo != null)
                    double.TryParse(tempo, out bpm);
            }
            song.bpm = bpm;
            return song;
        }


            
    }
}
