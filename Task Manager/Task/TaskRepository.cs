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
        public string DataFileName { get; set; }

        public TaskRepository(string dataFileName)
        {
            DataFileName = dataFileName;
        }
        public TaskRepository() : this("TaskRepo.txt") { }

        //   ####   CRUD   ####

        public void AddTask(Task task)
        {
            tasks.Add(task);
        }
        public void RemoveTask(Task task)
        {
            tasks.Remove(task);
        }
        public Task? GetTask(string searchID)
        {
            Task? task = null;
            foreach (Task t in tasks)
            {
                if (searchID.ToUpper() == t.Name.ToUpper())
                    task = t;
            }
            //if (task == null)
            //    throw new Exception("No task with that name was found.");
            return task;
        }
        public void Save()
        {
            StreamWriter sw = new StreamWriter(DataFileName);
            foreach (Task t in tasks)
            {
                sw.WriteLine(t.FullToString());
            }
            sw.Close();
        }
        public void Load()
        {
            StreamReader sr = new StreamReader(DataFileName);
            string[] lines = sr.ReadToEnd().Split("\r\n");
            if (lines.Length > 0)
            {
                foreach (string line in lines)
                {
                    if (line.Length > 0)
                    {
                        string?[] data = line.Split(";");
                        Task t;
                        if (data[1] == "" && data[2] == "")
                        {
                            t = new Task(data[0], Task.StringToLevel(data[3]));
                            tasks.Add(t);
                        }
                        else if (data[1] == "" && data[2] != "")
                        {
                            t = new Task(data[0], DateTime.Parse(data[2]), Task.StringToLevel(data[3]));
                            tasks.Add(t);
                        }
                        else if (data[1] != "" && data[2] == "")
                        {
                            t = new Task(data[0], data[1], Task.StringToLevel(data[3]));
                            tasks.Add(t);
                        }
                        else if (data[1] != "" && data[2] != "")
                        {
                            t = new Task(data[0], data[1], DateTime.Parse(data[2]), Task.StringToLevel(data[3]));
                            tasks.Add(t);
                        }
                        else
                            throw new Exception("gg");

                    }
                }
            }
            else
                throw new Exception("File does contain any tasks");
            sr.Close();
        }
    }
}
