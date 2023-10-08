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


        public Snake(int xPos, int yPos, string direction)                // Constructor for the snake. 
        {
            XPos = xPos;
            YPos = yPos;
            Direction = direction;
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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(ToString());
            Console.ForegroundColor= ConsoleColor.White;
        }
    }
}
