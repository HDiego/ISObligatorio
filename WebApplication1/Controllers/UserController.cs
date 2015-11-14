using DataAccess;
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
            List<GrupoTrabajo> grupos = Singleton.GetInstance().GruposTrabajo;
            if (grupos != null)
            {
                ViewBag.Grupos = grupos.Select(g => new SelectListItem()
                {
                    Text = g.Nombre,
                    Value = g.ID
                }).ToList();
            }
                return View();
        }

        public ActionResult RegisterUser(Cliente user)
        {
            return View();
        }
    }
}
