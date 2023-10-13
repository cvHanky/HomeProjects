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
        public static int Screenwidth { get; set; } = Console.WindowWidth;
        public static int Screenheight { get; set; } = Console.WindowHeight;
        public int SnakeLength { get; set; }
        public int CurrentHighScore { get; set; }


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

            // Load global scoreboard

            // DataHandler scoreHandler = new DataHandler(dataFileName);
            // HighScore[] highScores = scoreHandler.LoadHighScores();
            HighScore[] highScores = new HighScore[] // delete this line once the handler works.
            {
                new HighScore(20, "Hanky"),
                new HighScore(2, "Dingo"),
                new HighScore(55, "Roberozlav"),
                new HighScore(90, "Leif"),
                new HighScore(42069, "Jbro"),
                new HighScore(42, "Tusindben"),
            };
            

            //          #### Initialize game ####
            bool newHighScore = false;
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

                Thread.Sleep(150);  // Framerate for the game (every frame is 200ms long). 

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

                // Add score to list if game over
                if (gameOver && SnakeLength > CurrentHighScore)
                {
                    newHighScore = true;
                    CurrentHighScore = SnakeLength;
                }

                // End of game

            }

            string gameOverMessage = "GAME OVER!";
            string playAgainMessage = "Want to play again? Y/N";
            if (newHighScore)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                string newHighScoreMessage = "New personal high score: " + CurrentHighScore;
                Console.SetCursorPosition(Screenwidth / 2 - newHighScoreMessage.Length / 2, Screenheight / 2 + 2);
                Console.WriteLine(newHighScoreMessage);
                Console.SetCursorPosition(Screenwidth / 2 - gameOverMessage.Length / 2, Screenheight / 2 - 4);
                Console.WriteLine(gameOverMessage);
                Console.SetCursorPosition(Screenwidth / 2 - playAgainMessage.Length / 2, Screenheight / 2 - 2);
                Console.WriteLine(playAgainMessage);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(Screenwidth / 2 - gameOverMessage.Length / 2, Screenheight / 2 - 4);
                Console.WriteLine(gameOverMessage);
                Console.SetCursorPosition(Screenwidth / 2 - playAgainMessage.Length / 2, Screenheight / 2 - 2);
                Console.WriteLine(playAgainMessage);
                Console.ForegroundColor = ConsoleColor.White;
            }

            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Y)
                    playAgain = true;
                else if (keyInfo.Key == ConsoleKey.N)
                    playAgain = false;
            } while (keyInfo.Key != ConsoleKey.Y && keyInfo.Key != ConsoleKey.N);

            if (!playAgain)
            {
                string submitScoreMessage = "Do you want to submit your high score? Y/N";
                string currentHighScoreMessage = "High score: " + CurrentHighScore;
                Console.Clear();
                MakeBorder(true);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(Screenwidth / 2 - submitScoreMessage.Length / 2, Screenheight / 2 - 4);
                Console.WriteLine(submitScoreMessage);
                Console.SetCursorPosition(Screenwidth / 2 - currentHighScoreMessage.Length / 2, Screenheight / 2 - 2);
                Console.WriteLine(currentHighScoreMessage);
                Console.ForegroundColor = ConsoleColor.White;

                ConsoleKeyInfo keyInfo1;
                bool userNameMenu = false;
                do
                {
                    keyInfo1 = Console.ReadKey(true);
                    if (keyInfo1.Key == ConsoleKey.Y)
                    {
                        Console.Clear();
                        MakeBorder(true);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        string insertNameMessage = "Enter your username: ";
                        Console.SetCursorPosition(Screenwidth / 2 - insertNameMessage.Length / 2, Screenheight / 2 - 4);
                        Console.Write(insertNameMessage);
                        string userName = Console.ReadLine();
                        string confirmMessage = "Confirm username? Y/N";
                        Console.SetCursorPosition(Screenwidth / 2 - confirmMessage.Length / 2, Screenheight / 2 - 2);
                        Console.Write(confirmMessage);
                        Console.ForegroundColor = ConsoleColor.White;

                        ConsoleKeyInfo keyInfoConfirmUsername;
                        do
                        {
                            keyInfoConfirmUsername = Console.ReadKey(true);
                            if (keyInfoConfirmUsername.Key == ConsoleKey.Y)
                            {
                                highScores[highScores.Length - 1] = new HighScore(CurrentHighScore, userName);
                                userNameMenu = false;
                            }
                            else if (keyInfoConfirmUsername.Key == ConsoleKey.N)
                            {
                                userNameMenu = true;
                            }

                        } while (keyInfoConfirmUsername.Key != ConsoleKey.Y && keyInfoConfirmUsername.Key != ConsoleKey.N);

                        Console.Clear();
                        MakeBorder(true);
                        Scoreboard.SortScoresDescending(highScores);
                        Scoreboard.ShowScoreboard(highScores);

                    }
                    else if (keyInfo1.Key == ConsoleKey.N)
                    {

                    }

                } while (keyInfo1.Key != ConsoleKey.Y && keyInfo1.Key != ConsoleKey.N && userNameMenu != false);
            }
        }
    }
}
