using Task_Manager.Task;

namespace Task_Manager_UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        TaskRepository tr1;
        TaskRepository tr2;

        Task_Manager.Task.Task t1, t2, t3, t4;      // Unfortunate collision with System.Threading.Tasks.Task, forcing the 
                                                    // specification of which namespace to draw the "Task" object from. 
                                                    

        [TestInitialize]
        public void Init()
        {
            tr1 = new TaskRepository();
            tr2 = new TaskRepository();

            t1 = new Task_Manager.Task.Task(name: "Get a haircut", Level.medium);
            t2 = new Task_Manager.Task.Task(name: "Buy gift for dad", DateTime.Today.AddDays(24), Level.high);
            t3 = new Task_Manager.Task.Task(name: "Get air blower", description: "found on ProShop.com for 300DKK", Level.low);
            t4 = new Task_Manager.Task.Task(name: "Drink water", description: "please drink water", DateTime.Now, Level.urgent);

            tr1.AddTask(t1);
            tr1.AddTask(t2);
            tr1.AddTask(t3);
            tr1.AddTask(t4);
        }

        [TestMethod]
        public void TestSave()
        {
            tr1.Save();
            Assert.AreEqual(true, File.Exists("TaskRepo.txt"));
        }
        [TestMethod]
        public void TestLoad()
        {
            tr1.Save();
            tr2.Load();

            Assert.AreEqual(t1.ToString(), tr2.GetTask("Get a haircut").ToString());
            Assert.AreEqual(t2.ToString(), tr2.GetTask("Buy gift for dad").ToString());
            Assert.AreEqual(t3.ToString(), tr2.GetTask("Get air blower").ToString());
            Assert.AreEqual(t4.ToString(), tr2.GetTask("Drink water").ToString());
        }
    }
}