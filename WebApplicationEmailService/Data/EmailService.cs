using WebApplicationEmailService.Models;
using WebApplicationEmailService.Models.ViewModels;

namespace WebApplicationEmailService.Data
{
    public class EmailService
    {
        private EmailDbContext db = new EmailDbContext();

        public void AddMessage(Message newItem)
        {
            var e = db.Set<Message>().Add(newItem);
            db.SaveChanges();
        }

        public int Add(User newItem)
        {
            var e = db.Set<User>().Add(newItem);
            db.SaveChanges();
            return e.Id;
        }
        public IEnumerable<User> GetAllUsers()
        {
            return db.Set<User>();
        }
        public IEnumerable<MessagesViewModel> GetAllMessagesByUserId(int id)
        {
            var query = from u in db.Users
                        join m in db.Messages on u.Id equals m.Receiver.Id
                        //  join ms in db.Messages on u.Id equals ms.Sender.Id
                        where m.Receiver.Id == id
                        select new MessagesViewModel
                        {
                            Title = m.TitleMessage,
                            Text = m.TextMessage,
                            Reciver = m.Receiver.UserName,
                            Sender = m.Sender.UserName,
                            Date = m.DateMessage
                        };
            return query;              
        }


    }
}

