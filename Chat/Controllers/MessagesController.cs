using Chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Chat.Controllers
{
    public class Msg
    {
        public string Text {get; set;}
        public string UserId {get; set;}
    }
    public class MessagesController : ApiController
    {
        // GET api/messages
        [HttpGet]
        public IEnumerable<Msg> AllMessages() //Все сообщения
        {
            using (var cntx = new ChatContext())
            {
                return (from item in cntx.TextMessages 
                        orderby item.TextMessageId   // сортируем сообщения в порядке убывания
                        descending
                        select new Msg { Text = item.Text, UserId = item.UserId }).ToList();// выводим их
            }
        }
        // GET api/messages
        [HttpGet]
        public IEnumerable<Msg> MessagesOfUser(string id) // Сообщения от конкретного пользователя
        {
            using (var cntx = new ChatContext())
            {
                return (from item in cntx.TextMessages 
                        orderby item.TextMessageId 
                        descending //сортируем
                        where item.UserId == id 
                        select new Msg { Text= item.Text, UserId = item.UserId }).ToList();// выводим
            }
        }
    }
}