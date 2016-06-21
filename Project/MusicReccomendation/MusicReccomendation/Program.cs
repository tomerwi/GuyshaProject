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
            RecommendationSystem rc = new RecommendationSystem();
            rc.Load("collection.nml");
            List<Song> playList = new List<Song>();
            Song ans = rc.Recommend(playList);
            while (ans != null)
            {
                playList.Add(ans);
                ans = rc.Recommend(playList);
            }
        }
    }
}
