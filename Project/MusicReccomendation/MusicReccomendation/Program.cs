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
            string dataSetFile = "collection.nml";
            List<Song> playList = CreatePlaylistFromRecommendations(dataSetFile);
            PrintPlayList(playList);
            double recTestScore = TestRecommendation(dataSetFile);
        }

        public static List<Song> CreatePlaylistFromRecommendations(string file)
        {
            RecommendationSystem rc = new RecommendationSystem();
            rc.Load(file);
            List<Song> playList = new List<Song>();
            Song ans = rc.Recommend(playList);
            while (ans != null)
            {
                playList.Add(ans);
                ans = rc.Recommend(playList);
            }
            return playList;
        }

        public static double TestRecommendation(string file)
        {
            RecommendationSystem rc = new RecommendationSystem();
            rc.Load(file);
            List<Song> playList = new List<Song>();
            playList.AddRange(rc.DataSet); //can be changed.. not all the data set..
            return rc.TestRecommendation(playList);
        }

        public static void PrintPlayList(List<Song> playList)
        {

        }
    }
}
