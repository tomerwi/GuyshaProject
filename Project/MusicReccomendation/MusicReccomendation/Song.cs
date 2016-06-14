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
        private float bpm;
        private string title;
        private string artist;
        private List<string> genre;
        private int playCount;
        private Key key;


    }
}
