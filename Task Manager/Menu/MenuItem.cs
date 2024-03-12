using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager.Menu
{
    public class MenuItem
    {
        public string Content { get; set; }
        public int Count { get; set; }         // Count returns
        public MenuItem(string content)
        {
            Content = content;
            Count = Content.Split("\n").Length;
        }
        public void Write()
        {
            string[] list = Content.Split("\n");
            list[0] += "   ";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(list[0]);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(list[1]);
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 2; i < list.Length; i++)
            {
                Console.WriteLine(list[i]);
            }
        }
    }
}
