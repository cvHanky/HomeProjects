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
        public MenuItem(string content)
        {
            Content = content;
        }
        public static void CRUDMenu()
        {
            // do something
        }
    }
}
