using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Вход в чат";

            return View();
        }
        public ActionResult ChatRoom(string user)
        {
            if (user!=null)
                if (user != "")
                    return View((object)user);
                else
                    return View((object)"anon");
            else
                return View((object)"anon");
        }
    }
}
