using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicReccomendation
{
    class Song
    {
        //public int id;
        public double bpm;
        public string title;
        public string artist;
        public List<string> genre; //hilla: change to list
        public int playCount;
        public string key;

        public override bool Equals(object obj)
        {
            if (obj is Song)
            {
                Song song = (Song)obj;
                if (title.Equals(song.title) && artist.Equals(song.artist))
                    return true;
            }
            return base.Equals(obj);
        }
        
        public string PrintString()
        {
            return title + " - " + artist;
        }
    }
}
