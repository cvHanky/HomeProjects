using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager.Task
{
    public class TaskController
    {
        private TaskRepository taskRepo;
        public TaskController()
        {
            taskRepo = new TaskRepository();
        }
        public List<Task> Load()
        {
            return taskRepo.Load();
        }
        public void CreateTask(string name, string? description, DateTime? dueDate, Level priority)
        {
            taskRepo.CreateTask(name, description, dueDate, priority);
        }
    }
}
