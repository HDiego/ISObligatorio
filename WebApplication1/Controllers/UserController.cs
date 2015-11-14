using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Colabora.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LoginUser(Cliente user)
        {
            return View();
        }

        public ActionResult Register() 
        {
            return View();
        }

        public ActionResult Register(Cliente user)
        {
            return View();
        }
    }
}
