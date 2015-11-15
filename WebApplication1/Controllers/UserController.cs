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
            CargarViewBags();
            return View();
        }

        [HttpPost]
        public ActionResult Register(Cliente user, string confirmarContra, string idGrupoTrabajo)
        {
            if (ModelState.IsValid) 
            {
                if(user.Contraseña.Equals(confirmarContra))
                {
                    if (idGrupoTrabajo != null) 
                    {
                        user.GrupoTrabajo = Singleton.GetInstance().GetGrupoTrabajo(idGrupoTrabajo);
                    }
                    Singleton.GetInstance().Clientes.Add(user);
                    return RedirectToAction("Login");
                }
            }
            CargarViewBags();
            return View(user);
        }

        public void CargarViewBags() 
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
            else
            {
                ViewBag.Grupos = new List<SelectListItem>();
            }
        }
    }
}
