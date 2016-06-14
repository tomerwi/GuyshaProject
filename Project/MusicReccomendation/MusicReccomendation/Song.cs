using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicReccomendation
{
    class Song
    {
        public enum Key { };
        private string id;
        public float bpm;
        public string title;
        public  string artist;
        public List<string> genre; //hilla: change to list
        public int playCount;
        private Key key;


    }
}
