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
        public string Direction { get; set; }
        public bool playAgain { get; set; }
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
                    Thread.Sleep(3);
            }
            for (int i = 0; i < Screenheight; i++)
            {
                Console.SetCursorPosition(Screenwidth - 1, i);
                Console.Write("■");
                if (!DoInstant)
                    Thread.Sleep(3);
            }
            for (int i = Screenwidth-1; i > 0; i--)
            {
                Console.SetCursorPosition(i, Screenheight - 1);
                Console.Write("■");
                if (!DoInstant)
                    Thread.Sleep(3);
            }
            for (int i = Screenheight-1; i > 0; i--)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
                if (!DoInstant)
                    Thread.Sleep(3);
            }
        }

        public void TestMovement()
        {
            //          #### Initialize ####

            Console.CursorVisible = false;
            Random random = new Random();
            XPos = random.Next(1, Screenwidth);
            YPos = random.Next(1, Screenheight);
            Console.SetCursorPosition(XPos,YPos);
            Console.Write("■");

            //            The main movement loop
            while(true)
            {
                ConsoleKeyInfo arrow = Console.ReadKey(true);
                switch (arrow.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (YPos < Screenheight - 2)
                            YPos += 1;
                        Console.Clear();
                        MakeBorder(true);
                        break;
                    case ConsoleKey.UpArrow:
                        if (YPos > 1)
                            YPos -= 1;
                        Console.Clear();
                        MakeBorder(true);
                        break;
                    case ConsoleKey.LeftArrow:
                        if (XPos > 2)
                            XPos -= 2;
                        Console.Clear();
                        MakeBorder(true);
                        break;
                    case ConsoleKey.RightArrow:
                        if (XPos < Screenwidth - 3)
                            XPos += 2;
                        Console.Clear();
                        MakeBorder(true);
                        break;
                    default:
                        break;
                }
                Console.SetCursorPosition(XPos, YPos);
                Console.Write("■");
                Thread.Sleep(10);
            }
        }

        public void ArrowKeys()
        {
            ConsoleKeyInfo arrow = Console.ReadKey(true);
            switch (arrow.Key)
            {
                case ConsoleKey.DownArrow:
                    if (Direction != "UP")
                        Direction = "DOWN";
                    break;
                case ConsoleKey.UpArrow:
                    if (Direction != "DOWN")
                        Direction = "UP";
                    break;
                case ConsoleKey.LeftArrow:
                    if (Direction != "RIGHT")
                        Direction = "LEFT";
                    break;
                case ConsoleKey.RightArrow:
                    if (Direction != "LEFT")
                        Direction = "RIGHT";
                    break;
                default:
                    break;
            }
        }
        public void RunSnake()
        {
            //          #### Initialize ####

            
            bool gameOver = false;
            Console.CursorVisible = false;
            Random random = new Random();
            XPos = random.Next(1, Screenwidth);
            YPos = random.Next(1, Screenheight);
            Console.SetCursorPosition(XPos, YPos);
            Console.Write("■");
            string[] Directions = new string[] { "UP", "DOWN", "LEFT", "RIGHT" };
            Direction = Directions[random.Next(Directions.Length)];

            while (!gameOver)
            {
                Thread.Sleep(200);
                if (Console.KeyAvailable)
                    ArrowKeys();
                //          #### Snake Movement ####

                switch (Direction)
                {
                    case "UP":
                        if (YPos > 1)
                            YPos -= 1;
                        else if (YPos <= 1)
                            gameOver = true;
                        Console.Clear();
                        MakeBorder(true);
                        break;
                    case "DOWN":
                        if (YPos < Screenheight - 2)
                            YPos += 1;
                        else if (YPos >= Screenheight - 2)
                            gameOver = true;
                        Console.Clear();
                        MakeBorder(true);
                        break;
                    case "LEFT":
                        if (XPos > 2)
                            XPos -= 2;
                        else if (XPos <= 2)
                            gameOver = true;
                        Console.Clear();
                        MakeBorder(true);
                        break;
                    case "RIGHT":
                        if (XPos < Screenwidth - 3)
                            XPos += 2;
                        else if (XPos >= Screenwidth - 3)
                            gameOver = true;
                        Console.Clear();
                        MakeBorder(true);
                        break;
                    default:
                        break;
                }
                Console.SetCursorPosition(XPos, YPos);
                Console.Write("■");

            }
            string gameOverMessage = "Game over!";
            string playAgainMessage = "Want to play again? Y/N";

            Console.SetCursorPosition(Screenwidth / 2 - gameOverMessage.Length/2,Screenheight/2);
            Console.WriteLine(gameOverMessage);
            Console.SetCursorPosition(Screenwidth / 2 - playAgainMessage.Length/2, Screenheight / 2+1);
            Console.WriteLine(playAgainMessage);
            
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            do
            {
                if (keyInfo.Key == ConsoleKey.Y)
                    playAgain = true;
                else if (keyInfo.Key == ConsoleKey.N)
                    playAgain = false;
            } while (keyInfo.Key != ConsoleKey.Y && keyInfo.Key != ConsoleKey.N);
        }
    }
}
