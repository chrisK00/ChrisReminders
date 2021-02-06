using System;

namespace ChrisReminders
{
    public class Reminder
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool Sent { get; set; }

    }
}