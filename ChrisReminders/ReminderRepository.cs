using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChrisReminders
{
    public class ReminderRepository
    {
        private readonly List<Reminder> _reminders = new()
        {
            new Reminder()
            { Title = "Hämta barnen", Description = "Glöm inte vantarna", DueDate = DateTime.Now.AddDays(-1) },
            new Reminder()
            { Title = "Mata ormen", Description = "", DueDate = DateTime.Now.AddDays(2) },
            new Reminder()
            { Title = "STÄDA RUMMET", Description = "gör det fint :D", DueDate = DateTime.Now.AddDays(-5), Sent = true }
        };

        public IEnumerable<Reminder> GetAllDueReminders()
        {

            return _reminders
                .Where(r => r.DueDate <= DateTime.Now)
                .Where(r => !r.Sent);
        }
    }
}
