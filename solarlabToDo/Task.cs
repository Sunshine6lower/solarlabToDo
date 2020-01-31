using System;
using Microsoft.EntityFrameworkCore;

namespace solarlabToDo
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public DateTime Deadline { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Task task = obj as Task;
            if (task == null)
                return false;
            return Id == task.Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}