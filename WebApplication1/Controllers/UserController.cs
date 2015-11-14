using DataAccess;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Colabora.Models;

namespace Colabora.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        public ActionResult LoginUser(LoginViewModel usuario)
        {
            if(usuario.Password == null || usuario.UserName == null)
            {
                return View("Login", usuario);
            }
            else
            {
                Singleton BD = Singleton.GetInstance();
                Cliente logeado = BD.ValidLogIn(usuario.UserName, usuario.Password);
                if (logeado == null)
                {
                    ModelState.AddModelError(string.Empty, "El nombre de usuario o contraseña son incorrectos");
                    return View("Login", usuario);
                }
                Session["user"] = logeado;
                return View("Index");
            }
        }

        public ActionResult LogOut()
        {
            Session["user"] = null;
            LoginViewModel model = new LoginViewModel();
            return View("Login", model);
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
