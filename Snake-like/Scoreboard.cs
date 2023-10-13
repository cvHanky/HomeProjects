using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_like
{
    public class Scoreboard
    {
        public static void SortScoresDescending(HighScore[] highScores)
        {
            HighScore temp;

            for (int i = highScores.Length - 1; i >= 0; i--)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    if (highScores[i].Score > highScores[j].Score)
                    {
                        temp = highScores[i];
                        highScores[i] = highScores[j];
                        highScores[j] = temp;
                    }
                }
            }
        }
        public static void ShowScoreboard(HighScore[] highScores)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < highScores.Length;i++)
            {
                Console.SetCursorPosition(Game.Screenwidth / 2 - highScores[i].ToString().Length / 2, Game.Screenheight / 2 - 6 + 2*i);
                Console.Write(highScores[i].ToString());
                if (i >= 10)
                    break;
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
