﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MusicReccomendation
{
    class RecommendationSystem
    {

        public List<Song> DataSet;
        private Parser parser;

        public RecommendationSystem()
        {
            parser = new Parser();

        }
        public void Load(string file)
        {
            DataSet = parser.ParseXML(file);
        }

        //This function gets the current playlist, and returns the next song that should be played
        public Song Recommend(List<Song> playList)
        {
            
            if (DataSet == null || DataSet.Count == 0)
                return null;
            Random rnd = new Random();
            int limit = 1000; 
            List<Song> randomData = DataSet.Where(x => x.playCount > 2).ToList(); //taking only the songs which were played more then twice
            if (randomData.Count < limit)
                limit = randomData.Count;
            randomData = randomData.OrderByDescending(x => (x.playCount * rnd.Next())).Take(limit).Where(x=> !playList.Contains(x)).ToList(); //dont take the songs that were already played in the current playlist
            Song ans = randomData.First();
            if (playList == null || playList.Count == 0)
            {
                return ans; //choose random song to begin the playlist with
            }
            double maxScore = calcScore(ans,playList); 
            randomData.ForEach(song =>
            {
                double score = calcScore(song, playList);
                if(score > maxScore)
                {
                    maxScore = score;
                    ans = song;
                }
            });
            return ans;
        }

        public Song RecommendRandom(List<Song> playList)
        {
            Random rnd = new Random();
            return DataSet.Where(x=>!playList.Contains(x)).OrderBy(x => rnd.Next()).ToList().First();
        }

        //we assume that the last song that was played is in the last index int the playlist
        private double calcScore(Song song, List<Song> playList)
        {
            double score = 0;
            int index = 0;
            playList.ForEach(psong =>
            {
                index++;
                double sim = calcSimilarity(song, psong);
                score += (index * sim); //give more weight to songs that was recenlty played
            });
            score = score / playList.Count; 
            return score;
        }

        //Calculate the similarity between songs, based on BPM, Genre, Key and Title.
        private double calcSimilarity(Song song1, Song song2)
        {
            //BPM Similarity (Maximun value - 1 )
            double bpmSim = 1 - (Math.Abs(song1.bpm - song2.bpm)/ 60 ) ;
            if (bpmSim < 0)
                bpmSim = 0;

            //Key Similarity (Maximum value - 1)
            double keySim = 0;
            if (song1.key.Equals(song2.key))
                keySim = 1;
            else
            {
                int countSimKey = 0;
                song1.key.ToCharArray().ToList().ForEach(c =>
                {
                    if (song2.key.Contains(c))
                        countSimKey ++;
                });
                int maxLength = Math.Max(song1.key.Length, song2.key.Length);
                keySim = countSimKey / maxLength;
            }

            //Artist Similarity (Maximun value - 0.5)
            double artistSim = 0;
            if (song1.artist.Equals(song2.artist))
                artistSim = 0.5;

            //Genre Similarity (Maximum value - 1
            int countSimGenre = 0;
            if(song1.genre.Count !=0 && song2.genre.Count != 0)
            {
                song1.genre.ForEach(g =>
                {
                    if (song2.genre.Contains(g))
                        countSimGenre++;
                });
            }
            double genreSim = 0;
            if (countSimGenre != 0)
            {
                genreSim = countSimGenre / (song1.genre.Count + song2.genre.Count - countSimGenre);
            }

            return genreSim + artistSim + bpmSim + keySim;
        }
      
        public double TestRecommendation(List<Song> playList)
        {
            List<Song> currPlayList = new List<Song>();
            double avgSim = 0;
            double maxSim = 3.5;
            //change to csv
            CSVExport csvExport = new CSVExport();
            csvExport.AddRow();
            csvExport["original"] = playList.First().PrintString();
            csvExport["recommended"] = "";
            csvExport["recommended by random"] = "";
            csvExport["similarity"] = 0;
            csvExport["similarity to random"] = 0;
            foreach (Song song in playList)
            {
                int index = playList.IndexOf(song);
                if (index == playList.Count - 1)
                    break;
                currPlayList.Add(song);
                Song nextSong = playList[index + 1];
                Song recommendedSong = Recommend(currPlayList);
                Song recommendedRandom = RecommendRandom(currPlayList);
                double sim = calcSimilarity(recommendedSong, nextSong);
                double randSim = calcSimilarity(recommendedRandom, nextSong);
               // randSim = randSim / maxSim;
               // sim = sim / maxSim;
                csvExport.AddRow();
                csvExport["original"] = nextSong.PrintString();
                csvExport["recommended"] = recommendedSong.PrintString();
                csvExport["recommended by random"] = recommendedRandom.PrintString();
                csvExport["similarity"] = sim;
                csvExport["similarity to random"] = randSim;
                avgSim += sim; 
            }
            avgSim = avgSim / (playList.Count - 1);
            csvExport.ExportToFile("testResults" + ".csv");
            return avgSim;
        }

    }
}
