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
                var ok = true;
                if(!user.Contraseña.Equals(confirmarContra))
                {
                    ModelState.AddModelError("confirmarContra", "Las contraseñas no coinciden");
                    ok = false;
                }
                if (Singleton.GetInstance().YaExisteIDCliente(user.ID))
                {
                    ModelState.AddModelError("ID", "El identificador ya existe");
                    ok = false;
                }
                if (Membresia.Desde >= Membresia.Hasta)
                {
                    ModelState.AddModelError("Membresia.Hasta", "La fecha hasta debe ser mayor a la fecha desde");
                    ok = false;
                }
                if(ok){
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

        public ActionResult EditarCuenta(string idUsuario) 
        {
            var usuario = Singleton.GetInstance().GetClientePorID(idUsuario);
            CargarViewBags();
            return View(usuario);
        }

        [HttpPost]
        public ActionResult EditarCuenta(Cliente cliente, string confirmarContra, string idGrupoTrabajo, Membresia Membresia, bool membresiaOn = false)
        {
            ModelState.Remove("ID");
            ModelState.Remove("Contraseña");
            if (!membresiaOn)
            {
                ModelState.Remove("Membresia.Desde");
                ModelState.Remove("Membresia.Hasta");
            }
            if (ModelState.IsValid)
            {
                var ok = true;
                if (!cliente.Contraseña.Equals(confirmarContra))
                {
                    ModelState.AddModelError("confirmarContra", "Las contraseñas no coinciden");
                    ok = false;
                }
                if (membresiaOn && Membresia.Desde < DateTime.Today) 
                {
                    ModelState.AddModelError("Membresia.Desde", "La fecha desde debe ser mayoy o igual a hoy");
                    ok = false;
                }
                if (membresiaOn && Membresia.Desde >= Membresia.Hasta)
                {
                    ModelState.AddModelError("Membresia.Hasta", "La fecha hasta debe ser mayor a la fecha desde");
                    ok = false;
                }
                if (membresiaOn && Singleton.GetInstance().EsPeriodoMembresiaExcluyente(cliente, Membresia)) 
                {
                    ModelState.AddModelError("Membresia.Desde", "El período de membresia debe de ser excluyente de los anteriores");
                    ok = false;
                }
                if (ok)
                {
                    if (idGrupoTrabajo != null)
                    {
                        cliente.GrupoTrabajo = Singleton.GetInstance().GetGrupoTrabajo(idGrupoTrabajo);
                    }
                    if (membresiaOn)
                    {
                        cliente.Membresias.Add(Membresia);
                        Factura factura = new Factura(false)
                        {
                            Cliente = cliente,
                            Membresia = Membresia,
                            TotalAPagar = Membresia.CalcularTotal(),
                            IVA = Membresia.CalcularImpuestos()
                        };
                        Singleton.GetInstance().Clientes.Add(cliente);
                        return View("MostrarFactura", factura);
                    }
                    Singleton.GetInstance().Clientes.Add(cliente);
                    return RedirectToAction("Index", "Home");
                }
            }
            CargarViewBags();
            return View(cliente);
        }
    }
}
