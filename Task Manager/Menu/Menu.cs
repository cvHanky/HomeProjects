using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Manager.Task;

namespace Task_Manager.Menu
{
    public class Menu
    {
        public List<MenuItem> MenuItems { get; } = new List<MenuItem>();
        public string? Title { get; set; }
        public int XStartPos { get; set; }
        public int YStartPos { get; set; }
        public ConsoleColor ArrowColor { get; set; } = ConsoleColor.Yellow;
        private TaskController taskController;
        public Menu(string title, int xStartPos, int yStartPos)
        {
            Title = title;
            XStartPos = xStartPos;
            YStartPos = yStartPos;
            taskController = new TaskController();
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
                item.Write();
                YPos += item.Count + 2;
            }
            string enterMessage = "Press Enter to choose";
            Console.SetCursorPosition(Console.WindowWidth - enterMessage.Length, 0);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(enterMessage);
            string tabMessage = "Press Tab to create new task";
            Console.SetCursorPosition(Console.WindowWidth - tabMessage.Length, 2);
            Console.Write(tabMessage);
        }
        public int MenuArrowMovement()
        {
            int titleCount = 0;
            if (Title != null)
                titleCount += 2;
            int count = 0;
            int totalYPos = YStartPos + count + titleCount;
            bool menuRunning = true;

            Console.SetCursorPosition(XStartPos, totalYPos);
            Console.ForegroundColor = ArrowColor;
            Console.Write("-> ");
            Console.ForegroundColor = ConsoleColor.White;
            MenuItems[0].Write();

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
            if (Title != null)
                titleCount += 2;
            int offsetBefore = 0;
            for (int i = 1; i <= count; i++)
            {
                offsetBefore += MenuItems[i - 1].Count + 2;
            }

            ConsoleKeyInfo arrow = Console.ReadKey(true);
            switch (arrow.Key)
            {
                case ConsoleKey.DownArrow:              // Færdig nu tror jeg?
                    if (count != MenuItems.Count - 1)
                    {
                        int offsetAfterDown = offsetBefore + MenuItems[count].Count + 2;
                        Console.SetCursorPosition(XStartPos, YStartPos + titleCount + offsetBefore);
                        MenuItems[count].Write();
                        Console.SetCursorPosition(XStartPos, YStartPos + titleCount + offsetAfterDown);
                        Console.ForegroundColor = ArrowColor;
                        Console.Write("-> ");
                        Console.ForegroundColor = ConsoleColor.White;
                        MenuItems[count + 1].Write();
                        countAfterRun = count + 1;
                    }
                    else if (count == MenuItems.Count - 1)
                    {
                        Console.SetCursorPosition(XStartPos, YStartPos + titleCount + offsetBefore);
                        MenuItems[count].Write();
                        Console.SetCursorPosition(XStartPos, YStartPos + titleCount);
                        Console.ForegroundColor = ArrowColor;
                        Console.Write("-> ");
                        Console.ForegroundColor = ConsoleColor.White;
                        MenuItems[0].Write();
                        countAfterRun = 0;
                    }
                    break;
                case ConsoleKey.UpArrow:           // Mangler et fix ift. offset.
                    if (count != 0)
                    {
                        int offsetAfterUp = offsetBefore - MenuItems[count - 1].Count - 2;
                        Console.SetCursorPosition(XStartPos, YStartPos + titleCount + offsetBefore);
                        MenuItems[count].Write();
                        Console.SetCursorPosition(XStartPos, YStartPos + titleCount + offsetAfterUp);
                        Console.ForegroundColor = ArrowColor;
                        Console.Write("-> ");
                        Console.ForegroundColor = ConsoleColor.White;
                        MenuItems[count - 1].Write();
                        countAfterRun = count - 1;
                    }
                    else if (count == 0)
                    {
                        int offsetAfterUp = 0;
                        for (int i = 0; i < MenuItems.Count - 1; i++)
                        {
                            offsetAfterUp += MenuItems[i].Count + 2;
                        }
                        Console.SetCursorPosition(XStartPos, YStartPos + titleCount + offsetBefore);
                        MenuItems[count].Write();
                        Console.SetCursorPosition(XStartPos, YStartPos + titleCount + offsetAfterUp);
                        Console.ForegroundColor = ArrowColor;
                        Console.Write("-> ");
                        Console.ForegroundColor = ConsoleColor.White;
                        MenuItems[MenuItems.Count - 1].Write();
                        countAfterRun = MenuItems.Count - 1;
                    }
                    break;
                case ConsoleKey.Enter:
                    countAfterRun = 9999;
                    break;
                case ConsoleKey.Backspace:
                    countAfterRun = -1;
                    break;
                case ConsoleKey.Tab:
                    CreateTaskMenu();
                    break;
                default:
                    break;
            }
            return countAfterRun;
        }
        public void CreateTaskMenu()
        {
            string TaskName;
            Level TaskPriority = Level.low;
            DateTime? TaskDueDate = null;
            string? TaskDescription;

            Console.Clear();
            Console.CursorVisible = true;
            string s = "What is the title of the task?";
            Console.SetCursorPosition((Console.WindowWidth / 2) - (s.Length / 2), Console.WindowHeight / 2 - 5);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(s);
            s = "Press enter to confirm";
            Console.SetCursorPosition((Console.WindowWidth / 2) - (s.Length / 2), 1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(s);
            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2);
            Console.ForegroundColor = ConsoleColor.White;
            TaskName = Console.ReadLine();                // Writing the name of the task
            Console.Clear();
            s = "What priority does the task have?";
            Console.SetCursorPosition((Console.WindowWidth / 2) - (s.Length / 2), Console.WindowHeight / 2 - 5);
            Console.Write(s);
            s = "Press 1, 2, 3 or 4 to choose priority";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition((Console.WindowWidth / 2) - (s.Length / 2), 1);
            Console.Write(s);
            s = "low   medium   high   urgent";
            Console.SetCursorPosition((Console.WindowWidth / 2) - (s.Length / 2), Console.WindowHeight / 2);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(s);
            s = " 1       2       3       4  ";
            Console.SetCursorPosition((Console.WindowWidth / 2) - (s.Length / 2), Console.WindowHeight / 2 + 2);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(s);

            ConsoleKeyInfo keyInfo;                 // Choosing the priority of the task
            do
            {
                keyInfo = Console.ReadKey(true);
            }
            while (keyInfo.Key != ConsoleKey.D1 && keyInfo.Key != ConsoleKey.D2 && keyInfo.Key != ConsoleKey.D3 && keyInfo.Key != ConsoleKey.D4);
            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                    TaskPriority = Level.low;
                    break;
                case ConsoleKey.D2:
                    TaskPriority = Level.medium;
                    break;
                case ConsoleKey.D3:
                    TaskPriority = Level.high;
                    break;
                case ConsoleKey.D4:
                    TaskPriority = Level.urgent;
                    break;
                default:
                    break;
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            s = "Write a description for the task (optional)";
            Console.SetCursorPosition((Console.WindowWidth / 2) - (s.Length / 2), Console.WindowHeight / 2 - 5);
            Console.Write(s);
            s = "Press enter to confirm";
            Console.SetCursorPosition((Console.WindowWidth / 2) - (s.Length / 2), 1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(s);
            Console.SetCursorPosition(20, Console.WindowHeight / 2);
            Console.ForegroundColor = ConsoleColor.White;
            TaskDescription = Console.ReadLine();          // Writing the description (optional)
            if (TaskDescription == "") TaskDescription = null;
            Console.Clear();

            bool addingDays = true;
            string? m = null;
            while (addingDays)
                try
                {
                    s = "                                                                                "; // stupid way to clear input
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 20, Console.WindowHeight / 2);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(s);
                    s = "How many days from today should the task be due?";
                    Console.SetCursorPosition((Console.WindowWidth / 2) - (s.Length / 2), Console.WindowHeight / 2 - 5);
                    Console.Write(s);
                    s = "Press enter to confirm";
                    Console.SetCursorPosition((Console.WindowWidth / 2) - (s.Length / 2), 1);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(s);
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 1, Console.WindowHeight / 2);
                    Console.ForegroundColor = ConsoleColor.White;
                    m = Console.ReadLine();
                    TaskDueDate = DateTime.Today.AddDays(int.Parse(m));   // Setting the due date of the task.
                    addingDays = false;
                }
                catch (FormatException ex)    // Catches if user does not input a number
                {
                    if (m != "")              // Empty input is allowed.
                    {
                        Console.SetCursorPosition(Console.WindowWidth / 2 - (ex.Message.Length/2), Console.WindowHeight / 2 + 3);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(ex.Message);
                    }
                    else if (m == "")
                    {
                        TaskDueDate = null;
                        addingDays = false;
                    }
                }
            taskController.CreateTask(TaskName, TaskDescription, TaskDueDate, TaskPriority);   // Saves the task. 
            LoadMenuItems();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorVisible = false;
            Show();
        }
        public void AddMenuItem(MenuItem item)
        {
            MenuItems.Add(item);
        }
        public void LoadMenuItems()
        {
            List<Task.Task> taskList = taskController.Load();
            MenuItems.Clear();
            foreach (Task.Task task in taskList)
            {
                MenuItem mItem = new MenuItem(task.ToString());
                AddMenuItem(mItem);
            }
        }
    }
}
