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
        [HttpGet]
        public IEnumerable<Msg> AllMessages() 
        {
            using (var cntx = new ChatContext())
            {
                return (from item in cntx.TextMessages 
                        orderby item.TextMessageId  
                        descending
                        select new Msg { Text = item.Text, UserId = item.UserId }).ToList();
            }
        }

        [HttpGet]
        public IEnumerable<Msg> MessagesOfUser(string id) 
        {
            using (var cntx = new ChatContext())
            {
                return (from item in cntx.TextMessages 
                        orderby item.TextMessageId 
                        descending 
                        where item.UserId == id 
                        select new Msg { Text= item.Text, UserId = item.UserId }).ToList();
            }
        }
    }
}