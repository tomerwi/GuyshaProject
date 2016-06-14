using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MusicReccomendation
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser p = new Parser();
            p.parseXML("C:\\Users\\Tomer\\Documents\\GitHub\\GuyshaProject\\Dataset\\collection.nml");

        }
    }
}
