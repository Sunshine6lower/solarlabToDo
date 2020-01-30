using System;

namespace solarlabToDo
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public DateTime Deadline { get; set; }
    }
}