using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicReccomendation
{
    class RecommendationSystem
    {

        private List<Song> dataSet;
        private Parser parser;

        public RecommendationSystem()
        {
            parser = new Parser();

        }
        public void Load(string file)
        {
            dataSet = parser.ParseXML(file);
        }

        public Song Recommend(List<Song> playList)
        {
            //playcount
            if (dataSet == null || dataSet.Count == 0)
                return null;
            Random rnd = new Random();
            List<Song> randomData = dataSet.OrderByDescending(x => (x.playCount * rnd.Next())).Take(1000).Where(x=> !playList.Contains(x)).ToList();
            Song ans = randomData.First();
            if (playList == null || playList.Count == 0)
            {
                return ans;
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

        //we assume that the last song that was played is in the last index int the playlist
        private double calcScore(Song song, List<Song> playList)
        {
            double score = 0;
            int index = 0;
            playList.ForEach(psong =>
            {
                index++;
                double sim = calcSimilarity(song, psong);
                score += (index * sim);
            });
            score = score / playList.Count;
            return score;
        }

        private double calcSimilarity(Song song1, Song song2)
        {
            double bpmSim = (1 - Math.Abs(song1.bpm - song2.bpm)) / 60 ;
            if (bpmSim < 0)
                bpmSim = 0;
            int countSimKey = 0;
            if (song1.artist.Equals(song2.artist))
                countSimKey = 3;
            else
            {
                song1.key.ToCharArray().ToList().ForEach(c =>
                {
                    if (song2.key.Contains(c))
                        countSimKey ++;
                });
            }
            double keySim = countSimKey / 3;
            double artistSim = 0;
            if (song1.artist.Equals(song2.artist))
                artistSim = 0.5;
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

       
    }
}
