using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Chat.Models;

namespace Chat
{
    public class ChatHub : Hub
    {
        public static List<User> Users = new List<User>();

        public void Send(string name, string message)
        {

            Clients.All.addMessage(name, message);
            using (var cntx = new ChatContext())
            {
                cntx.TextMessages.Add(new TextMessage() 
                {
                    Text = message,
                    UserId = name
                });
                cntx.SaveChanges();
            }
        }

        public void Connect(string userName)
        {
            var id = Context.ConnectionId;
            using(var cntx = new ChatContext()){
                if (!cntx.Users.Any(x => x.UserId == userName))
                {
                    cntx.Users.Add(new User()
                    {
                        UserId = userName
                    });
                    cntx.SaveChanges();
                }
            }
            if (!Users.Any(x => x.ConnectionId == id))
            {
                Users.Add(new User { ConnectionId = id, UserId = userName });

                Clients.Caller.onConnected();

                Clients.AllExcept(id).onNewUserConnected();
            }
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.UserId);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}