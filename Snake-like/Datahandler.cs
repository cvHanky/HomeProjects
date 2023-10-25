using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_like
{
    
    public class DataHandler
    {
        public string DataFileName { get; set; }
        public DataHandler(string dataFileName)
        {
            DataFileName = dataFileName;
        }

        public void SaveHighScores(HighScore[] highScores)
        {
            StreamWriter sw = new StreamWriter(DataFileName);
            for (int i = 0; i < highScores.Length; i++)
            {
                sw.WriteLine(highScores[i].MakeTitle());
            }
            sw.Close();
        }
        public HighScore[] LoadHighScores()
        {
            StreamReader sr = new StreamReader(DataFileName);
            string[] separatedLines = sr.ReadToEnd().Split("\n");
            HighScore[] highScores = new HighScore[separatedLines.Length];

            int count = 0;
            while (count < highScores.Length - 1)
            {
                string line = separatedLines[count];
                string[] scoreData = new string[3];
                scoreData = line.Split(";");

                HighScore hs = new HighScore(int.Parse(scoreData[0]), scoreData[1]);
                highScores[count] = hs;
                count++;
            }
            sr.Close();
            return highScores;
        }
    }
}
