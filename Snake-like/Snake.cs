using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Snake_like
{
    public class Snake
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int Length { get; set; }
        public int Screenwidth { get; set; } = Console.WindowWidth;
        public int Screenheight { get; set; } = Console.WindowHeight;

        public Snake()                // Constructor for the snake. 
        {
            XPos = Screenwidth / 2;
            YPos = Screenheight / 2;
            Length = 3; 
        }

        public void MakeBorder(bool DoInstant)      // Makes the border of the map in a fun way. (5 ms interval)
        {
            Console.CursorVisible = false;
            if (!DoInstant)
            {
                string loading = "Game is loading...";
                Console.SetCursorPosition(Screenwidth / 2 - loading.Length, Screenheight / 2);
                Console.Write(loading);
            }
            for (int i = 0; i < Screenwidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
                if (!DoInstant)
                    Thread.Sleep(5);
            }
            for (int i = 0; i < Screenheight; i++)
            {
                Console.SetCursorPosition(Screenwidth - 1, i);
                Console.Write("■");
                if (!DoInstant)
                    Thread.Sleep(5);
            }
            for (int i = Screenwidth-1; i > 0; i--)
            {
                Console.SetCursorPosition(i, Screenheight - 1);
                Console.Write("■");
                if (!DoInstant)
                    Thread.Sleep(5);
            }
            for (int i = Screenheight-1; i > 0; i--)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
                if (!DoInstant)
                    Thread.Sleep(5);
            }
        }

        public void TestMovement()
        {
            //          #### Initialize ####

            Console.CursorVisible = false;
            Random random = new Random();
            XPos = random.Next(1, Screenwidth);
            YPos = random.Next(1, Screenheight);

            //          #### Skrt ####

            Console.SetCursorPosition(XPos,YPos);
            Console.Write("■");

            //            The main movement loop
            while(true)
            {
                ConsoleKeyInfo arrow = Console.ReadKey(true);
                switch (arrow.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (YPos < Screenheight - 1)
                            YPos += 1;
                        //Console.Clear();
                        //MakeBorder(true);
                        break;
                    case ConsoleKey.UpArrow:
                        if (YPos > 1)
                            YPos -= 1;
                        //Console.Clear();
                        //MakeBorder(true);
                        break;
                    case ConsoleKey.LeftArrow:
                        if (XPos > 1)
                            XPos -= 2;
                        //Console.Clear();
                        //MakeBorder(true);
                        break;
                    case ConsoleKey.RightArrow:
                        if (XPos < Screenwidth-1)
                            XPos += 2;
                        //Console.Clear();
                        //MakeBorder(true);
                        break;
                    default:
                        break;
                }
                Console.SetCursorPosition(XPos, YPos);
                Console.Write("■");
                Thread.Sleep(5);
            }
        }
    }
}
