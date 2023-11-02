using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager.Task
{
    public class Task
    {
        public string Name { get; set; }         // Title of the task.
        public string? Description { get; set; } // Optional description of the task.
        public DateTime? DueDate { get; set; }   // Optional due date of the task.
        public Level Priority { get; set; }      // Priority of the task

        public Task(string name,string? description, DateTime? dueDate, Level priority)   // Main constructor
        {
            Name = name;
            Description = description;
            DueDate = dueDate;
            Priority = priority;
        }
        public Task(string name, string? description, Level priority) : this(name,description, null, priority) { }  // Chained constructors.
        public Task(string name, DateTime? dueDate, Level priority) : this(name, null, dueDate, priority) { }
        public Task(string name, Level priority) : this(name, null, null, priority) { }

        public override string ToString()
        {
            if (DueDate != null && Description == null)
            {
                return $"{Name}: PRIORITY {Priority}\nDue date: {DueDate.ToString()}";
            }
            else if (DueDate == null && Description == null)
            {
                return $"{Name}: PRIORITY {Priority}";
            }
            else if (DueDate != null && Description != null)
            {
                return $"{Name}: PRIORITY {Priority}\n{Description}\nDue date: {DueDate.ToString()}";
            }
            else return "";
        }
        public string FullToString()    // Used when saving the data.
        {
            return $"{Name};{Description};{DueDate};{Priority}";
        }
        public static Level StringToLevel(string s)
        {
            switch (s.ToUpper())
            {
                case "LOW":
                    return Level.low;
                case "MEDIUM":
                    return Level.medium;
                case "HIGH":
                    return Level.high;
                case "URGENT":
                    return Level.urgent;
                default:
                    throw new Exception("An error occured when loading the priority.");
            }
        }
    }
}
