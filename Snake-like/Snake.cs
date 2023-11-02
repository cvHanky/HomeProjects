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
        public ConsoleColor ColorOfSnake { get; set; } = ConsoleColor.Red;
        public ConsoleColor[] Colors { get; set; }
        public Random random { get; set; }


        public Snake(int xPos, int yPos, string direction)                // Constructor for the snake. 
        {
            XPos = xPos;
            YPos = yPos;
            Direction = direction;
            Colors = new ConsoleColor[] {
            ConsoleColor.Red, ConsoleColor.DarkBlue, ConsoleColor.Blue, ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.DarkMagenta, ConsoleColor.White, ConsoleColor.Magenta, ConsoleColor.DarkYellow, ConsoleColor.DarkRed
            };
            random = new Random();
        }
        public Snake()
        {
            
        }
        public override string ToString()
        {
            return "■";
        }
        public void WriteSnake()
        {
            Console.ForegroundColor = ColorOfSnake;
            Console.Write(ToString());
            Console.ForegroundColor= ConsoleColor.White;
        }
        public void RandomizeSnakeColor()
        {
            ColorOfSnake = Colors[random.Next(Colors.Length)];
        }
        public static void DeleteTail()
        {
            Console.Write("");
        }
    }
}
