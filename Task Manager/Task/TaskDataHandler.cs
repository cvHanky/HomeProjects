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
        public TaskDataHandler(string dataFileName)
        {
            DataFileName = dataFileName;
        }
        public TaskDataHandler() : this("TaskRepo.txt") { }
        public void Save(List<Task> tasks)
        {
            StreamWriter sw = new StreamWriter(DataFileName);
            foreach (Task t in tasks)
            {
                sw.WriteLine(t.FullToString());
            }
            sw.Close();
        }

        public List<Task> Load()
        {
            List<Task> tasks = new List<Task>();
            StreamReader sr = new StreamReader(DataFileName);
            string[] lines = sr.ReadToEnd().Split('\n');
            if (lines.Length > 0)
            {
                foreach (string line in lines)
                {
                    string?[] data = line.Split(";");
                    DateTime? data2 = null;
                    if (data[1] == "")
                        data[1] = null;
                    if (data[2] != "")
                        data2 = DateTime.Parse(data[2]);
                    Task t = new Task(name: data[0], description: data[1], dueDate: data2, priority: Task.StringToLevel(data[3]));
                    tasks.Add(t);
                }
            }
            else
                throw new Exception("File does contain any tasks");
            sr.Close();
            return tasks;
        }

    }
}
