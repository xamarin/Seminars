using System;

namespace MT52Demo
{
    public class Task
    {   
        public Task ()
        {
            Name = "New Task";
        }
        
        public string Name { get; set; }
        
        public string Description { get; set; }

        public DateTime DueDate { get; set; }
    }
}