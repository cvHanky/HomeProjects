using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_like
{
    public class HighScore
    {
        public int Score { get; set; }
        public string PlayerName { get; set; }
        
        public HighScore(int score,string playerName)
        {
            Score = score;
            PlayerName = playerName;
        }
        public string MakeTitle()
        {
            string title = Score + ";" + PlayerName + ";";
            return title;
        }

        public override string ToString()
        {
            return PlayerName + " (" + Score + ")";
        }
    }
}
