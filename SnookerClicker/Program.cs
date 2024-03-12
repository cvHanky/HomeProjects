using System.Security.Cryptography.X509Certificates;

namespace SnookerClicker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int WindowHeight = Console.WindowHeight;
            int WindowWidth = Console.WindowWidth;

            int Pots = 0;
            int Miss = 0;

            List<Click?> list = new List<Click?>();
            list.Add(null); list.Add(null); list.Add(null); list.Add(null); list.Add(null);

            Console.CursorVisible = false;
            while (true)
            {
                Console.Clear();
                string misspotMessage = $"Pots: {Pots}                   Misses: {Miss}";
                Console.SetCursorPosition(WindowWidth / 2 - 35 / 2, 10);
                Console.Write(misspotMessage);

                float potPercentage = (float)Pots * 100 / ((float)Miss + (float)Pots);
                string potpercentMessage = $"Pot percentage: {potPercentage}%";
                Console.SetCursorPosition(WindowWidth / 2 - 23 / 2, 15);
                Console.Write(potpercentMessage);

                

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.P:
                        Pots++;
                        list.RemoveAt(0);
                        list.Add(new Click("Pot"));
                        break;

                    case ConsoleKey.M:
                        Miss++;
                        list.RemoveAt(0);
                        list.Add(new Click("Miss"));
                        break;

                    case ConsoleKey.U:
                        // Execute undo thing
                        if (list[4] != null && list[4].Purpose == "Pot")
                        {
                            Pots--;
                        }
                        else if (list[4] != null && list[4].Purpose == "Miss")
                        {
                            Miss--;
                        }
                        list.RemoveAt(4);
                        list.Add(null);
                        list[4] = list[3];
                        list[3] = list[2];
                        list[2] = list[1];
                        list[1] = list[0];
                        list[0] = null;
                        break;
                }

            }
        }
    }
}
