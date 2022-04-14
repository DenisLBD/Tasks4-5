using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Users.Models;
using Users.ViewModels;
using Microsoft.AspNetCore.SignalR;
using WebPush;


namespace Users.Controllers
{
    public class MessageController : Controller
    {
        MessageContext db;
        IHubContext<ChatHub> hubContext;
        UserManager<User> _userManager;

        public MessageController(MessageContext context, IHubContext<ChatHub> hubContext, UserManager<User> userManager)
        {
            db = context;
            this.hubContext = hubContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Inbox() => View(db.Messages.ToList());

        [HttpPost]
        public async Task<IActionResult> Send(MessageViewModel model)
        {
            User user = await _userManager.FindByNameAsync(model.RecipientName);
            if (user == null)
            {
                ModelState.AddModelError("", "Incorrrect recipient name");
                return View(model);
            }

            db.Messages.AddRange(
                new MessageViewModel
                {
                    RecipientName = model.RecipientName,
                    Sender = User.Identity.Name,
                    Topic = model.Topic,
                    Message = model.Message,
                    SendTime = DateTime.Now
                }
                );
            db.SaveChanges();

            await hubContext.Clients.All.SendAsync("Notify", $"You recieved message from {User.Identity.Name} {DateTime.Now.ToShortTimeString()}\n{model.Topic}\n{model.Message}");
           
            return RedirectToAction("Index", "Users");
        }

    }
}
