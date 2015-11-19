using DataAccess;
using DataAccess1.Utils;
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
            if (usuario.Password == null || usuario.UserName == null)
            {
                return View("Login", usuario);
            }
            else
            {
                Singleton BD = Singleton.GetInstance();
                Cliente logeado = BD.ValidLogIn(usuario.UserName, usuario.Password);
                if (logeado == null)
                {
                    ModelState.AddModelError(string.Empty, "El identificador de usuario o contraseña son incorrectos");
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
            CargarViewBags();
            return View();
        }

        [HttpPost]
        public ActionResult Register(Cliente user, string confirmarContra, string idGrupoTrabajo, Membresia Membresia)
        {
            if (ModelState.IsValid)
            {
                var ok = true;
                if (!user.Contraseña.Equals(confirmarContra))
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
                if (ok)
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
            }
            if (user.ID == null)
                user.ID = Singleton.GetInstance().GetClienteID();
            CargarViewBags();
            return View(user);
        }

        public void CargarViewBags()
        {
            List<GrupoTrabajo> grupos = Singleton.GetInstance().GruposTrabajo;
            if (grupos != null)
            {
                ViewBag.Grupos = grupos.Where(g => HayUnoActivo(g.Colaboradores)).Select(g => new SelectListItem()
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

        private bool HayUnoActivo(List<Cliente> list)
        {
            foreach (var cliente in list) 
            {
                if (cliente.EstaActivo()) 
                {
                    return true;
                }
            }
            return false;
        }

        public ActionResult MostrarFactura(Factura factura)
        {
            return View(factura);
        }

        public ActionResult EditarCuenta(string idCliente)
        {
            var usuario = Singleton.GetInstance().GetClientePorID(idCliente);
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
                if (ValidarDatos(cliente, confirmarContra, Membresia, membresiaOn))
                {
                    return ContinuarEdicionCuenta(cliente, confirmarContra, idGrupoTrabajo, Membresia, membresiaOn);
                }
            }
            CargarViewBags();
            return View(cliente);
        }

        private ActionResult ContinuarEdicionCuenta(Cliente cliente, string confirmarContra, string idGrupoTrabajo, Membresia Membresia, bool membresiaOn)
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
                ModificarCliente(cliente);
                ViewBag.ReturnUrl = "Home";
                return View("MostrarFactura", factura);
            }
            ModificarCliente(cliente);
            return RedirectToAction("Index", "Home");
        }

        private bool ValidarDatos(Cliente cliente, string confirmarContra, Membresia Membresia, bool membresiaOn)
        {
            var ok = true;
            if (cliente.Contraseña != null && !cliente.Contraseña.Equals(confirmarContra))
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
            if (membresiaOn && !Singleton.GetInstance().EsPeriodoMembresiaExcluyente(cliente, Membresia))
            {
                ModelState.AddModelError("Membresia.Desde", "El período de membresia debe de ser excluyente de los anteriores");
                ok = false;
            }
            return ok;
        }

        private void ModificarCliente(Cliente cliente)
        {
            var clienteAModificar = Singleton.GetInstance().GetClientePorID(cliente.ID);
            clienteAModificar.Apellido = cliente.Apellido;
            if (!cliente.Contraseña.Equals(""))
                clienteAModificar.Contraseña = cliente.Contraseña;
            clienteAModificar.Direccion = cliente.Direccion;
            clienteAModificar.Email = cliente.Email;
            clienteAModificar.GrupoTrabajo = cliente.GrupoTrabajo;
            clienteAModificar.Membresias = cliente.Membresias;
            clienteAModificar.Nombre = cliente.Nombre;
        }
    }
}
