﻿using System;
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
            string testSetFile = "testCollection.nml";
            //List<Song> playList = CreatePlaylistFromRecommendations(dataSetFile);
            //PrintPlayList(playList);
            double recTestScore = TestRecommendation(dataSetFile, testSetFile);
        }

        public static List<Song> CreatePlaylistFromRecommendations(string dataSetFile)
        {
            RecommendationSystem rc = new RecommendationSystem();
            rc.Load(dataSetFile);
            List<Song> playList = new List<Song>();
            Song ans = rc.Recommend(playList);
            while (ans != null)
            {
                playList.Add(ans);
                ans = rc.Recommend(playList);
            }
            return playList;
        }

        public static double TestRecommendation(string dataSetFile, string testSetFile)
        {
            RecommendationSystem rc = new RecommendationSystem();
            rc.Load(dataSetFile);
            Parser parser = new Parser();
            List<Song> playList = parser.ParseXML(testSetFile);
            return rc.TestRecommendation(playList);
        }

        public static void PrintPlayList(List<Song> playList)
        {

        }
    }
}
