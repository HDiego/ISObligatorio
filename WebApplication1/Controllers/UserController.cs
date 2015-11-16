using DataAccess;
using DataAccess1.Utils;
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
        public ActionResult Register(Cliente user, string confirmarContra, string idGrupoTrabajo, Membresia Membresia)
        {
            if (ModelState.IsValid) 
            {
                if(user.Contraseña.Equals(confirmarContra))
                {
                    if (Singleton.GetInstance().Clientes.Where(c => c.ID.Equals(user.ID)).ToList().Count > 0)
                    {
                        ModelState.AddModelError("ID", "El identificador ya existe");
                    }
                    else
                    {
                        if (Membresia.Desde < Membresia.Hasta)
                        {
                            if (idGrupoTrabajo != null)
                            {
                                user.GrupoTrabajo = Singleton.GetInstance().GetGrupoTrabajo(idGrupoTrabajo);
                            }
                            user.Membresias.Add(Membresia);
                            Singleton.GetInstance().Clientes.Add(user);
                            Factura factura = new Factura(false)
                            {
                                Cliente = user,
                                Membresia = Membresia,
                                TotalAPagar = Membresia.CalcularTotal(),
                                IVA = Membresia.CalcularImpuestos()
                            };

                            return View("MostrarFactura", factura);
                        }
                    ModelState.AddModelError("Membresia.Hasta", "La fecha hasta debe ser mayor a la fecha desde");
                    }
                }
            }
            if(user.ID == null)
                user.ID = Singleton.GetInstance().GetClienteID();
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
            ViewBag.membresia = new List<SelectListItem>() { new SelectListItem() { Text = "Parcial", Value = "false" }, 
                                                             new SelectListItem() { Text = "Total", Value = "true" } };
        }

        public ActionResult MostrarFactura(Factura factura) 
        {
            return View(factura);
        }   
    }
}
