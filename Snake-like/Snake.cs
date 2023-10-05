using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_like
{
    public class Snake
    {
        public int XPos { get; set; }
        public int Ypos { get; set; }
        public int Length { get; set; }
        public int Screenwidth { get; set; } = Console.WindowWidth;
        public int Screenheight { get; set; } = Console.WindowHeight;

        public Snake()                // Constructor for the snake. 
        {
            XPos = Screenwidth / 2;
            Ypos = Screenheight / 2;
            Length = 3; 
        }

        public void MakeBorder()     // Makes the border of the map in a fun way. (5 ms interval)
        {
            for (int i = 0; i < Screenwidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
                Thread.Sleep(5);
            }
            for (int i = 0; i < Screenheight; i++)
            {
                Console.SetCursorPosition(Screenwidth - 1, i);
                Console.Write("■");
                Thread.Sleep(5);
            }
            for (int i = Screenwidth-1; i > 0; i--)
            {
                Console.SetCursorPosition(i, Screenheight - 1);
                Console.Write("■");
                Thread.Sleep(5);
            }
            for (int i = Screenheight-1; i > 0; i--)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
                Thread.Sleep(5);
            }
        }
    }
}
