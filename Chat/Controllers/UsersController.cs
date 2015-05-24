using Chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Chat.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/users
        public IEnumerable<User> GetAllUsers() // Список пользователей
        {
            return ChatHub.Users;
        }
    }
}
