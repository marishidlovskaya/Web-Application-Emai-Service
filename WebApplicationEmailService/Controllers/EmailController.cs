using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using WebApplicationEmailService.Data;
using WebApplicationEmailService.Models;
using WebApplicationEmailService.Models.ViewModels;

namespace WebApplicationEmailService.Controllers
{
    public class EmailController: Controller
    {
        private EmailDbContext db = new EmailDbContext();
        public IActionResult Index(string messages)
        {

            List<MessagesViewModel> model = JsonConvert.DeserializeObject<List<MessagesViewModel>>(messages);
            return View(model);
        }

        public JsonResult SendMessage(string receiverId, string title, string textbody)
        {
            EmailService service = new EmailService();
            string senderId = HttpContext.Session.GetString("userId");
            User sender = service.GetAllUsers().Where(x=>x.Id == int.Parse(senderId)).FirstOrDefault();
            User receiver = service.GetAllUsers().Where(x => x.UserName == receiverId).FirstOrDefault();

            Message mess = new Message() { DateMessage = DateTime.Now, TitleMessage = title, TextMessage = textbody, Receiver = receiver, Sender = sender };
            if (receiver == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("error");
            }
            else
            {
                service.AddMessage(mess);
                return Json("sucess");
            }          
        }
        public JsonResult GetAllMessagesOfCurrentUser()
        {
            EmailService service = new EmailService();
            string senderId = HttpContext.Session.GetString("userId");
            List<MessagesViewModel> listOfAllMessages = service.GetAllMessagesByUserId(Int32.Parse(senderId)).ToList();          
            //var json =  JsonConvert.SerializeObject(listOfAllMessages);
            return Json(listOfAllMessages);
        }

        [HttpPost]
        public JsonResult GetUsers(string Prefix)
        {
            EmailService service = new EmailService();
            var users = service.GetAllUsers().ToList();
            
            List<UseM> result = new List<UseM>();   

            foreach (var user in users.ToList().Where(f=>f.UserName.StartsWith(Prefix, StringComparison.CurrentCultureIgnoreCase)))
            {
                result.Add(new UseM
                {
                    Id = user.Id.ToString(),
                    Name = user.UserName
                });
            }
            return Json(result);
        }


    }

    public class UseM
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

 



}
