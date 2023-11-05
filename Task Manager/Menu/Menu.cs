using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager.Menu
{
    public class Menu
    {
        public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
        public string? Title { get; set; }
        public int XStartPos { get; set; }
        public int YStartPos { get; set; }
        public Menu(string title, int xStartPos, int yStartPos)
        {
            Title = title;
            XStartPos = xStartPos;
            YStartPos = yStartPos;
        }
        public Menu(int xStartPos, int yStartPos) : this(null, xStartPos, yStartPos) { }
        public void Show()
        {
            int XPos = XStartPos;
            int YPos = YStartPos;
            Console.SetCursorPosition(XPos, YPos);
            if (Title != null)
            {
                Console.WriteLine(Title);
                YPos += 2;
            }
            foreach (MenuItem item in MenuItems)
            {
                Console.SetCursorPosition(XPos, YPos);
                Console.WriteLine(item.Content);
                YPos++;
            }
            string enterMessage = "Press Enter to choose";
            Console.SetCursorPosition(Console.WindowWidth - enterMessage.Length, 1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(enterMessage);
        }
        public int MenuArrowMovement()
        {
            int titleCount = 0;
            if (Title != null) titleCount += 2;
            int count = 0;
            int totalYPos = YStartPos + count + titleCount;
            bool menuRunning = true;

            Console.SetCursorPosition(XStartPos, totalYPos);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("-> ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(MenuItems[0].Content);

            while (menuRunning)
            {
                if (Console.KeyAvailable)
                {
                    int temp = ArrowKeys(count);
                    if (temp == 9999)             // Temp will be set to 9999 if Enter is pressed.
                        menuRunning = false;
                    else if (temp == -1)
                    {
                        count = temp;
                        menuRunning = false;
                    }
                    else
                        count = temp;
                }
            }
            return count;

            // Implement what happens when a menu choice is picked.
        }
        public int ArrowKeys(int count)
        {
            int countAfterRun = count;
            int titleCount = 0;
            if (Title != null) titleCount += 2;
            ConsoleKeyInfo arrow = Console.ReadKey(true);
            switch (arrow.Key)
            {
                case ConsoleKey.DownArrow:
                    if (count != MenuItems.Count-1)
                    {
                        Console.SetCursorPosition(XStartPos, YStartPos + titleCount + count);
                        Console.Write($"{MenuItems[count].Content}   ");
                        Console.SetCursorPosition(XStartPos, YStartPos + titleCount + count + 1);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("-> ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(MenuItems[count + 1].Content);
                        countAfterRun = count + 1;
                    }
                    else if (count == MenuItems.Count-1)
                    {
                        Console.SetCursorPosition(XStartPos, YStartPos + titleCount + count);
                        Console.Write($"{MenuItems[count].Content}   ");
                        Console.SetCursorPosition(XStartPos, YStartPos + titleCount);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("-> ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(MenuItems[0].Content);
                        countAfterRun = 0;
                    }
                    break;
                case ConsoleKey.UpArrow:
                    if (count != 0)
                    {
                        Console.SetCursorPosition(XStartPos, YStartPos + titleCount + count);
                        Console.Write($"{MenuItems[count].Content}   ");
                        Console.SetCursorPosition(XStartPos, YStartPos + titleCount + count - 1);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("-> ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(MenuItems[count - 1].Content);
                        countAfterRun = count - 1;
                    }
                    else if (count == 0)
                    {
                        Console.SetCursorPosition(XStartPos, YStartPos + titleCount + count);
                        Console.Write($"{MenuItems[count].Content}   ");
                        Console.SetCursorPosition(XStartPos, YStartPos + titleCount + MenuItems.Count - 1);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("-> ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(MenuItems[MenuItems.Count - 1].Content);
                        countAfterRun = MenuItems.Count - 1;
                    }
                    break;
                case ConsoleKey.Enter:
                    countAfterRun = 9999;
                    break;
                case ConsoleKey.Backspace:
                    countAfterRun = -1;
                    break;
                default:
                    break;
            }
            return countAfterRun;
        }
        public void MenuChoice(int count)
        {
            count--;
        }
    }
}
