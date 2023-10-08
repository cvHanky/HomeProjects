using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_like
{
    public class Game
    {
        public bool playAgain { get; set; }
        public int Screenwidth { get; set; } = Console.WindowWidth;
        public int Screenheight { get; set; } = Console.WindowHeight;
        public int SnakeLength { get; set; }
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
            for (int i = Screenwidth - 1; i > 0; i--)
            {
                Console.SetCursorPosition(i, Screenheight - 1);
                Console.Write("■");
                if (!DoInstant)
                    Thread.Sleep(3);
            }
            for (int i = Screenheight - 1; i > 0; i--)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
                if (!DoInstant)
                    Thread.Sleep(3);
            }
        }

        public void TestMovement(Snake snake)
        {
            //          #### Initialize ####

            Console.CursorVisible = false;
            Random random = new Random();
            snake.XPos = random.Next(1, Screenwidth);
            snake.YPos = random.Next(1, Screenheight);
            Console.SetCursorPosition(snake.XPos, snake.YPos);
            Console.Write("■");

            //            The main movement loop
            while (true)
            {
                ConsoleKeyInfo arrow = Console.ReadKey(true);
                switch (arrow.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (snake.YPos < Screenheight - 2)
                            snake.YPos += 1;
                        Console.Clear();
                        MakeBorder(true);
                        break;
                    case ConsoleKey.UpArrow:
                        if (snake.YPos > 1)
                            snake.YPos -= 1;
                        Console.Clear();
                        MakeBorder(true);
                        break;
                    case ConsoleKey.LeftArrow:
                        if (snake.XPos > 2)
                            snake.XPos -= 2;
                        Console.Clear();
                        MakeBorder(true);
                        break;
                    case ConsoleKey.RightArrow:
                        if (snake.XPos < Screenwidth - 3)
                            snake.XPos += 2;
                        Console.Clear();
                        MakeBorder(true);
                        break;
                    default:
                        break;
                }
                Console.SetCursorPosition(snake.XPos, snake.YPos);
                Console.Write("■");
                Thread.Sleep(10);
            }
        }

        public void ArrowKeys(Snake snake)
        {
            ConsoleKeyInfo arrow = Console.ReadKey(true);
            switch (arrow.Key)
            {
                case ConsoleKey.DownArrow:
                    if (snake.Direction != "UP")
                        snake.Direction = "DOWN";
                    break;
                case ConsoleKey.UpArrow:
                    if (snake.Direction != "DOWN")
                        snake.Direction = "UP";
                    break;
                case ConsoleKey.LeftArrow:
                    if (snake.Direction != "RIGHT")
                        snake.Direction = "LEFT";
                    break;
                case ConsoleKey.RightArrow:
                    if (snake.Direction != "LEFT")
                        snake.Direction = "RIGHT";
                    break;
                default:
                    break;
            }
        }
        public void DirectionalMovement(Snake snake)
        {
            switch (snake.Direction)
            {
                case "UP":
                    if (snake.YPos > 1)
                        snake.YPos -= 1;
                    Console.Clear();
                    MakeBorder(true);
                    break;
                case "DOWN":
                    if (snake.YPos < Screenheight - 2)
                        snake.YPos += 1;
                    Console.Clear();
                    MakeBorder(true);
                    break;
                case "LEFT":
                    if (snake.XPos > 2)
                        snake.XPos -= 2;
                    Console.Clear();
                    MakeBorder(true);
                    break;
                case "RIGHT":
                    if (snake.XPos < Screenwidth - 3)
                        snake.XPos += 2;
                    Console.Clear();
                    MakeBorder(true);
                    break;
                default:
                    break;
            }
        }
        public void RunSnake()
        {
            //          #### Initialize snake ####
            Random random = new Random();
            string[] Directions = new string[] { "UP", "DOWN", "LEFT", "RIGHT" };
            int randomXpos = random.Next(4, Screenwidth / 2);
            int randomYpos = random.Next(4, Screenheight);
            Snake frontSnake = new Snake(randomXpos*2,randomYpos, Directions[random.Next(Directions.Length)]);
            SnakeLength = 1;

            // List of 1 pixel long snakes
            Snake[] snakeList = new Snake[200];
            snakeList[0] = frontSnake;
            
            //          #### Initialize game ####
            bool gameOver = false;
            Console.CursorVisible = false;
            Console.SetCursorPosition(frontSnake.XPos, frontSnake.YPos);
            frontSnake.WriteSnake();


            //          #### Initialize berry ####
            Random randomBerry = new Random();
            randomXpos = randomBerry.Next(1, (Screenwidth-1)/2);
            randomYpos = randomBerry.Next(1, Screenheight-1);
            Berry berry = new Berry(randomXpos*2,randomYpos);

            while (!gameOver)
            {

                Thread.Sleep(100);  // Framerate for the game (every frame is 200ms long). 

                if (Console.KeyAvailable)
                    ArrowKeys(frontSnake);

                // Check for collision with wall
                if (frontSnake.YPos <= 1 && frontSnake.Direction == "UP")
                    gameOver = true;
                else if (frontSnake.YPos >= Screenheight - 2 && frontSnake.Direction == "DOWN")
                    gameOver = true;
                else if (frontSnake.XPos <= 2 && frontSnake.Direction == "LEFT")
                    gameOver = true;
                else if (frontSnake.XPos >= Screenwidth - 2 && frontSnake.Direction == "RIGHT")
                    gameOver = true;
                //          #### Snake Movement ####

                if (SnakeLength > 1)  // Moves every part except the front.
                {
                    for (int i = 1; i < SnakeLength; i++)
                    {
                        snakeList[SnakeLength-i].XPos = snakeList[SnakeLength-i-1].XPos;
                        snakeList[SnakeLength - i].YPos = snakeList[SnakeLength - i - 1].YPos;
                    }
                }

                DirectionalMovement(frontSnake); // Moves only the front. 
                berry.SpawnBerry();

                // Check for self collision
                for (int i = 1; i < SnakeLength; i++)
                {
                    if (frontSnake.XPos == snakeList[i].XPos && frontSnake.YPos == snakeList[i].YPos)
                    {
                        gameOver = true; break;
                    }
                }

                for (int i = 0; i < SnakeLength; i++)
                {
                    Console.SetCursorPosition(snakeList[i].XPos, snakeList[i].YPos);
                    snakeList[i].WriteSnake();
                }

                // Check for berry pick-up
                if (frontSnake.XPos == berry.XPos && frontSnake.YPos == berry.YPos)
                {
                    snakeList[SnakeLength] = new Snake();
                    randomXpos = randomBerry.Next(1, (Screenwidth-1) / 2);
                    randomYpos = randomBerry.Next(1, Screenheight-1);
                    berry.XPos = randomXpos * 2;
                    berry.YPos = randomYpos;
                    SnakeLength++;
                }

                // End of game

            }
            string gameOverMessage = "Game over!";
            string playAgainMessage = "Want to play again? Y/N";

            Console.SetCursorPosition(Screenwidth / 2 - gameOverMessage.Length / 2, Screenheight / 2);
            Console.WriteLine(gameOverMessage);
            Console.SetCursorPosition(Screenwidth / 2 - playAgainMessage.Length / 2, Screenheight / 2 + 1);
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
