using System.ComponentModel.DataAnnotations;

namespace WebApplicationEmailService.Models.ViewModels
{
    public class MessagesViewModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Reciver { get; set; }
        public string Sender { get; set; }
        public DateTime Date { get; set; }
    }
}
