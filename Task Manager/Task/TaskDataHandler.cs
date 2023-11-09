using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager.Task
{
    public class TaskDataHandler
    {
        public string DataFileName { get; set; }
        private List<Task> copyofTasks;
        public TaskDataHandler(string dataFileName, List<Task> copyofTasks)
        {
            DataFileName = dataFileName;
            this.copyofTasks = copyofTasks;
        }
        public TaskDataHandler(List<Task> copyofTasks) : this("TaskRepo.txt", copyofTasks) { }
        public void Save()
        {
            StreamWriter sw = new StreamWriter(DataFileName);
            foreach (Task t in copyofTasks)
            {
                sw.WriteLine(t.FullToString());
            }
            sw.Close();
        }
        public List<Task> Load()
        {
            copyofTasks.Clear();
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
                            copyofTasks.Add(t);
                        }
                        else if (data[1] == "" && data[2] != "")
                        {
                            t = new Task(data[0], DateTime.Parse(data[2]), Task.StringToLevel(data[3]));
                            copyofTasks.Add(t);
                        }
                        else if (data[1] != "" && data[2] == "")
                        {
                            t = new Task(data[0], data[1], Task.StringToLevel(data[3]));
                            copyofTasks.Add(t);
                        }
                        else if (data[1] != "" && data[2] != "")
                        {
                            t = new Task(data[0], data[1], DateTime.Parse(data[2]), Task.StringToLevel(data[3]));
                            copyofTasks.Add(t);
                        }
                    }
                }
            }
            sr.Close();
            return copyofTasks;
        }
    }
}
