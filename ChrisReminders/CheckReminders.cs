using Coravel.Invocable;
using System.Threading.Tasks;
using System;
using System.Net.Mail;

namespace ChrisReminders
{
    public class CheckReminders : IInvocable
    {
        private readonly ReminderRepository _repository;
        private readonly MailService _mailService;

        public CheckReminders(ReminderRepository repository, MailService mailService)
        {
            _repository = repository;
            _mailService = mailService;
        }
        public async Task Invoke()
        {
            foreach (var item in _repository.GetAllDueReminders())
            {
                if (item.DueDate <= DateTime.Now && !item.Sent)
                {
                    Console.WriteLine($"Time to do: {item.Title}");
                    var sent = await _mailService.SendMail(
                        new MailAddress("christianhunter707@gmail.com"),
                        item.Title,
                        item.Description);
                    if (sent)
                    {
                        item.Sent = true;
                    }
                   
                }
            }
        }
    }
}