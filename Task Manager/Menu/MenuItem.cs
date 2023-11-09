using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Manager.Task;

namespace Task_Manager.Menu
{
    public class MenuItem
    {
        public string Content { get; set; }
        public int Count { get; set; }         // Count returns
        public MenuItem(string content)
        {
            string[] list = content.Split("\n");
            list[0] += "   ";
            foreach (string s in list)
            {
                Content += $"{s}\n";
            }
            Count = list.Length;
        }
    }
}
