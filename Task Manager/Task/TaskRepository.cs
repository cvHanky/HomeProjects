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
        private TaskDataHandler tdh;

        //   ####   CRUD   ####

        public void AddTask(Task task)                  // CREATE
        {
            tasks.Add(task);
        }
        public Task? GetTask(string searchID)           // READ
        {
            Task? task = null;
            foreach (Task t in tasks)
            {
                if (searchID.ToUpper() == t.Name.ToUpper())
                    task = t;
            }
            if (task == null)
                throw new Exception("No task with that name was found.");
            return task;
        }
        public void UpdateTask(Task task, string name)  // UPDATE
        {
            task.Name = name;
        }
        public void UpdateTask(string description, Task task)   // update overloads
        {
            task.Description = description;
        }
        public void UpdateTask(Task task, DateTime dueDate)
        {
            task.DueDate = dueDate;
        }
        public void UpdateTask(Task task, Level priority)
        {
            task.Priority = priority;
        }
        public void RemoveTask(Task task)               // DELETE
        {
            tasks.Remove(task);
        }
        public void Save()                              // Calls datahandler to properly save.
        {
            tdh = new TaskDataHandler(tasks);
            tdh.Save();
        }
        public void Load()
        {
            tdh = new TaskDataHandler(tasks);
            tasks = tdh.Load();
        }
    }
}
