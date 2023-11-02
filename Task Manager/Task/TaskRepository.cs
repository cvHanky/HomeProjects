using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager.Task
{
    public class TaskRepository
    {
        private List<Task> tasks = new List<Task>();   

        //   ####   CRUD   ####

        public void AddTask(Task task)
        {
            tasks.Add(task);
        }
        public void RemoveTask(Task task)
        {
            tasks.Remove(task);
        }
        public void ReadTask(Task task)
        {

        }
    }
}
