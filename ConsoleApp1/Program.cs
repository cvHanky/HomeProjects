using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Console.WindowHeight = 16;
        Console.WindowWidth = 32;
        int screenwidth = Console.WindowWidth;
        int screenheight = Console.WindowHeight;
        Random randomnummer = new Random();
        int score = 5;
        int gameover = 0;
        pixel hoofd = new pixel();
        hoofd.xpos = screenwidth / 2;
        hoofd.ypos = screenheight / 2;
        hoofd.schermkleur = ConsoleColor.Red;
        string movement = "RIGHT";
        int xsnelheid = 1;
        int ysnelheid = 0;
        int berryx = randomnummer.Next(1, screenwidth);
        int berryy = randomnummer.Next(1, screenheight);
        DateTime tijd = DateTime.Now;
        DateTime tijd2 = DateTime.Now;
        string buttonpressed = "no";
        List<int> xposlijf = new List<int>();
        List<int> yposlijf = new List<int>();

        // Main game loop
        while (true)
        {
            Console.Clear();
            if (hoofd.xpos == screenwidth - 1 || hoofd.xpos == 0 || hoofd.ypos == screenheight - 1 || hoofd.ypos == 0)
            {
                gameover = 1;
            }
            for (int i = 0; i < screenwidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
            }
            for (int i = 0; i < screenwidth; i++)
            {
                Console.SetCursorPosition(i, screenheight - 1);
                Console.Write("■");
            }
            for (int i = 0; i < screenheight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
            }
            for (int i = 0; i < screenheight; i++)
            {
                Console.SetCursorPosition(screenwidth - 1, i);
                Console.Write("■");
            }
            Console.ForegroundColor = ConsoleColor.Green;
            if (berryx == hoofd.xpos && berryy == hoofd.ypos)
            {
                score++;
                berryx = randomnummer.Next(1, screenwidth);
                berryy = randomnummer.Next(1, screenheight);
            }
            else
            {
                if (xposlijf.Count > 0)
                {
                    for (int i = 0; i < xposlijf.Count; i++)
                    {
                        Console.SetCursorPosition(xposlijf[i], yposlijf[i]);
                        Console.Write("■");
                        xposlijf.Insert(i, hoofd.xpos);
                        yposlijf.Insert(i, hoofd.ypos);
                        xposlijf.RemoveAt(xposlijf.Count - 1);
                        yposlijf.RemoveAt(yposlijf.Count - 1);
                    }
                }
            }
            if (gameover == 1)
            {
                Console.SetCursorPosition(screenwidth / 5, screenheight / 2);
                Console.WriteLine("Game over, Score: " + score);
                Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 1);
                Environment.Exit(0);
            }
            Console.SetCursorPosition(hoofd.xpos, hoofd.ypos);
            Console.ForegroundColor = hoofd.schermkleur;
            Console.Write("■");
            tijd = DateTime.Now;
            buttonpressed = "no";
            while (true)
            {
                tijd2 = DateTime.Now;
                if (tijd2.Subtract(tijd).TotalMilliseconds > 500) { break; }
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo toets = Console.ReadKey(true);
                    //Console.WriteLine(toets.Key);
                    //bewegen
                    switch (toets.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (movement != "DOWN")
                            {
                                xsnelheid = 0;
                                ysnelheid = -1;
                                movement = "UP";
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            if (movement != "UP")
                            {
                                xsnelheid = 0;
                                ysnelheid = 1;
                                movement = "DOWN";
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            if (movement != "RIGHT")
                            {
                                xsnelheid = -1;
                                ysnelheid = 0;
                                movement = "LEFT";
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            if (movement != "LEFT")
                            {
                                xsnelheid = 1;
                                ysnelheid = 0;
                                movement = "RIGHT";
                            }
                            break;
                    }
                }
            }
            hoofd.xpos += xsnelheid;
            hoofd.ypos += ysnelheid;
        }
    }

    public class pixel
    {
        public int xpos { get; set; }
        public int ypos { get; set; }
        public ConsoleColor schermkleur { get; set; }
    }
}