using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_like
{
    public class Berry
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
        public Berry(int xPos, int yPos)
        {
            XPos = xPos;
            YPos = yPos;
        }
        public void SpawnBerry()
        {
            Console.SetCursorPosition(XPos, YPos);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }
        public override string ToString()
        {
            return "■";
        }
    }
}
