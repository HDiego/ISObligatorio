using Colabora.Models;
using DataAccess;
using Dominio.Utils;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Colabora.Controllers
{
    public class ReservaController : Controller
    {
        //
        // GET: /Reserva/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            Singleton DB = Singleton.GetInstance();
            Cliente logeado = (Cliente)Session["user"];
            List<Reserva> listReserva = DB.LoadReserva(logeado.ID);
            return View(listReserva);
        }

        public ActionResult AddReservaView()
        {
            AddReservaModel reserva = new AddReservaModel();
            Singleton BD = Singleton.GetInstance();
            reserva.ListSala = BD.LoadSala();
            return View("AddReserva", reserva);
        }

        public ActionResult AddReserva(AddReservaModel model)
        {
            Singleton DB = Singleton.GetInstance();
            if(model.Reserva.Desde == null || model.Reserva.Desde.Date < DateTime.Now.Date)
            {
                ModelState.AddModelError(string.Empty, "La fecha de inicio debe ser a lo sumo hoy");
            }
            else
            {
                if(model.Reserva.Hasta == null || model.Reserva.Hasta.Date < DateTime.Now.Date || model.Reserva.Hasta.Date < model.Reserva.Desde.Date)
                {
                    ModelState.AddModelError(string.Empty, "La fecha de fin debe ser posterior a la de inicio");
                }
                else
                {
                    if(DB.SalaDisponible(model.Reserva.Sala.ID, model.Reserva.Desde, model.Reserva.Hasta))
                    { 
                        Reserva reserva = model.Reserva;
                        reserva.Sala = DB.GetSala(model.Reserva.Sala.ID);
                        reserva.Cliente = (Cliente)Session["user"];
                        reserva.ID = reserva.Cliente.ID + reserva.Sala.ID + reserva.Desde.Day + reserva.Desde.Month + reserva.Desde.Year;
                        DB.NewReserva(reserva);
                        List<Reserva> lstReserva = DB.LoadReserva(reserva.Cliente.ID);
                        return View("List", lstReserva);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "La sala ya se encuentra reservada para esa fecha");
                    }
                }
            }
            model.Reserva.Sala = DB.GetSala(model.Reserva.Sala.ID);
            return View("AddReserva", model);
        }

        public ActionResult SelectSala(string txtID)
        {
            Singleton BD = Singleton.GetInstance();
            AddReservaModel model = new AddReservaModel();
            model.Reserva = new Reserva();
            model.Reserva.Sala = BD.GetSala(txtID);
            if(model.Reserva.Sala == null)
            {
                model.ListSala = BD.LoadSala();
                ModelState.AddModelError(string.Empty, "El identificador no es correcto");
            }
            return View("AddReserva", model);
        }

        public ActionResult NotificarReserva(string idReserva) 
        {
            Session["reserva"] = Singleton.GetInstance().GetReservaById(idReserva);
            var listaColaboradores = ((Cliente)Session["user"]).GrupoTrabajo.Colaboradores.Where(c => !c.ID.Equals(((Cliente)Session["user"]).ID)).ToList();
            return View(listaColaboradores);
        }

        //[HttpPost]
        public ActionResult NotificarColaboradores(string[] colaboradores)
        {
            colaboradores = colaboradores[0].Split(',').ToArray();
            Singleton BD = Singleton.GetInstance();
            var listaClientes = new List<Cliente>();
            foreach (var s in colaboradores) 
            {
                listaClientes.Add(BD.Clientes.Where(c => c.ID.Equals(s)).First());
            }
            Reserva reserva = ((Reserva)Session["reserva"]);
            ServicioEmail.EnviarEmail(listaClientes, reserva, ((Cliente)Session["user"]));
            reserva.SeNotifico = true;
            return RedirectToAction("Index", "User");
        }
	}
}