using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager.Task
{
    public class TaskRepository
    {
        public List<Task> tasks { get; set; }
        private TaskDataHandler tdh;
        public TaskRepository()
        {
            tasks = new List<Task>();
        }

        //   ####   CRUD   ####
        public void CreateTask(string name, string? description, DateTime? dueDate, Level priority)
        {
            Task t = new Task(name, description, dueDate, priority);
            AddTask(t);
            Save();
        }
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
        public void Save()                              // Calls datahandler to properly save the current tasks in the repo.
        {
            tdh = new TaskDataHandler(tasks);
            tdh.Save();
        }
        public List<Task> Load()                              // Calls datahandler to load the currently saved tasks.
        {
            tdh = new TaskDataHandler(tasks);
            tasks = tdh.Load();
            return tasks;
        }
    }
}
