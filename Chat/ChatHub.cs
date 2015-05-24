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
        public static List<User> Users = new List<User>();//список, который  хранит подключенных к чату пользователей.

        // Отправка сообщений
        public void Send(string name, string message)
        {

            Clients.All.addMessage(name, message);
            using (var cntx = new ChatContext())
            {
                cntx.TextMessages.Add(new TextMessage() //Сохраняем в БД
                {
                    Text = message,
                    UserId = name
                });
                cntx.SaveChanges();
            }
        }
        // Подключение нового пользователя
        public void Connect(string userName) //Вызываем после ввода логина
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

                // Посылаем сообщение текущему пользователю
                Clients.Caller.onConnected();

                // Посылаем сообщение всем пользователям, кроме текущего
                Clients.AllExcept(id).onNewUserConnected();
            }
        }

        // Отключение пользователя
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