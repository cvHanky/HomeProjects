using System.Threading.Tasks;
using Task_Manager.Menu;
using Task_Manager.Task;

namespace Task_Manager.Task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TaskRepository tr1;
            TaskRepository tr2;
            Menu.Menu testMenu;
            Task t1, t2, t3, t4;

            tr1 = new TaskRepository();
            tr2 = new TaskRepository();

            testMenu = new Menu.Menu(0,0);

            t1 = new Task(name: "Get a haircut", Level.medium);
            t2 = new Task(name: "Buy gift for dad", DateTime.Today.AddDays(24), Level.high);
            t3 = new Task(name: "Get air blower", description: "found on ProShop.com for 300DKK", Level.low);
            t4 = new Task(name: "Drink water", description: "please drink water", DateTime.Now, Level.urgent);
            tr1.AddTask(t1);
            tr1.AddTask(t2);
            tr1.AddTask(t3);
            tr1.AddTask(t4);

            foreach (Task t in tr1.tasks)
            {
                MenuItem mItem = new MenuItem(t.ToString());
                testMenu.AddMenuItem(mItem);
            }
            testMenu.Show();
            testMenu.MenuArrowMovement();

            //Menu.Menu testMenu = new Menu.Menu("Titlen på denne menu", 0, 0);
            //List<Menu.MenuItem> menuItems = new List<Menu.MenuItem>();
            //Menu.MenuItem a = new Menu.MenuItem("Testing testing 123");
            //Menu.MenuItem b = new Menu.MenuItem("123 bing bong");
            //Menu.MenuItem c = new Menu.MenuItem("Hvor er Adam?");

            //Menu.Menu aMenu = new Menu.Menu("Titlen på A menuen", 0, 0);
            //List<Menu.MenuItem> aMenuItems = new List<Menu.MenuItem>();
            //Menu.MenuItem aa = new Menu.MenuItem("Gør dette");
            //Menu.MenuItem bb = new Menu.MenuItem("Gør dutte");
            //Menu.MenuItem cc = new Menu.MenuItem("sunppg");

            //Menu.Menu bMenu = new Menu.Menu("Titlen på B menuen", 0, 0);
            //List<Menu.MenuItem> bMenuItems = new List<Menu.MenuItem>();
            //Menu.MenuItem aaa = new Menu.MenuItem("Gør dette dette");
            //Menu.MenuItem bbb = new Menu.MenuItem("Gør dutte dutte");
            //Menu.MenuItem ccc = new Menu.MenuItem("sunppg 123123");

            //Menu.Menu cMenu = new Menu.Menu("Titlen på C menuen", 0, 0);
            //List<Menu.MenuItem> cMenuItems = new List<Menu.MenuItem>();
            //Menu.MenuItem aaaa = new Menu.MenuItem("Gør dette");
            //Menu.MenuItem bbbb = new Menu.MenuItem("Gør dutte");
            //Menu.MenuItem cccc = new Menu.MenuItem("sunppg");

            //menuItems.Add(a);
            //menuItems.Add(b);
            //menuItems.Add(c);
            //aMenuItems.Add(aa);
            //aMenuItems.Add(bb);
            //aMenuItems.Add(cc);
            //bMenuItems.Add(aaa);
            //bMenuItems.Add(bbb);
            //bMenuItems.Add(ccc);
            //cMenuItems.Add(aaaa);
            //cMenuItems.Add(bbbb);
            //cMenuItems.Add(cccc);

            //testMenu.MenuItems = menuItems;
            //aMenu.MenuItems = aMenuItems;
            //bMenu.MenuItems = bMenuItems;
            //cMenu.MenuItems = cMenuItems;

            //int choice;
            //do
            //{
            //    Console.Clear();
            //    testMenu.Show();
            //    choice = testMenu.MenuArrowMovement();
            //    if (choice == -1)
            //        choice = 9999;
            //    Console.Clear();
            //    switch (choice)
            //    {
            //        case 0:

            //            aMenu.Show();
            //            choice = aMenu.MenuArrowMovement();
            //            if (choice == -1)
            //                continue;
            //            break;
            //        case 1:
            //            bMenu.Show();
            //            choice = bMenu.MenuArrowMovement();
            //            if (choice == -1)
            //                continue;
            //            break;
            //        case 2:
            //            cMenu.Show();
            //            choice = cMenu.MenuArrowMovement();
            //            if (choice == -1)
            //                continue;
            //            break;
            //        default:
            //            break;
            //    }
            //} while (choice != 9999);
        }
    }
}