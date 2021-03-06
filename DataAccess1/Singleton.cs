﻿using DataAccess1.Utils;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Singleton
    {
        #region Propiedades
        public List<Cliente> Clientes { get; set; }
        public List<Sala> Salas { get; set; }
        public List<Reserva> Reservas { get; set; }
        public List<GrupoTrabajo> GruposTrabajo { get; set; }
        public List<Factura> Facturas { get; set; }
        public int ID_Sugerencia { get; set; }
        #endregion

        #region Singleton
        private static Singleton instance;
        private Singleton()
        {
            Clientes = new List<Cliente>();
            Salas = new List<Sala>();
            Reservas = new List<Reserva>();
            GruposTrabajo = new List<GrupoTrabajo>();
            Facturas = new List<Factura>();
            this.ID_Sugerencia = 1;
            AddCliente();
            AddSala();
            AddGruposTrabajo();
        }

        public static Singleton GetInstance()
        {
            if (instance == null)
            {
                instance = new Singleton();
            }
            return instance;
        }
        #endregion

        #region Cliente
        public void AddCliente()
        {
            Clientes.Add(new Cliente("1", "Diego", "Rocca", "Av Italia", "1234", "diego@ort.com"));
            Clientes.Add(new Cliente("2", "Mauricio", "Delbono", "Duvimioso Terra", "1234", "mauriciodelbonofripp@gmail.com"));
            Clientes.Add(new Cliente("3", "Gerardo", "Quintana", "Cuareim", "1234", "gerardo@ort.com"));
        }

        public Cliente GetCliente(string email)
        {
            foreach (Cliente u in Clientes)
            {
                if (u.Email.Equals(email))
                    return u;
            }
            return null;
        }

        public Cliente GetClientePorID(string idUsuario)
        {
            foreach (Cliente u in Clientes)
            {
                if (u.ID.Equals(idUsuario))
                    return u;
            }
            return null;
        }

        public bool DeleteCliente(string email)
        {
            Cliente u = Clientes.Find(c => c.Email.Equals(email));
            if (u != null)
            {
                Clientes.Remove(u);
                return true;
            }
            return false;
        }

        public Cliente ValidLogIn(string idCliente, string contraseña)
        {
            Cliente retorno = null;
            foreach(Cliente cli in Clientes)
            {
                if (cli.ID.Equals(idCliente) && cli.Contraseña.Equals(contraseña))
                    retorno = cli;
            }
            return retorno;
        }

        public string GetClienteID() 
        {
            var nuevoID = "CWS_" + this.ID_Sugerencia.ToString("00");
            this.ID_Sugerencia ++;
            while (Clientes.Where(c => c.ID.Equals(nuevoID)).Count() > 0) 
            {
                nuevoID = "CWS_" + this.ID_Sugerencia.ToString("00");
                this.ID_Sugerencia++;
            }
            return nuevoID;
        }

        public bool YaExisteIDCliente(string idCliente) 
        {
            return Clientes.Where(c => c.ID.Equals(idCliente)).ToList().Count > 0;
        }

        public bool EsPeriodoMembresiaExcluyente(Cliente cliente, Membresia membresia) 
        {
            bool esExcluyente = true;
            foreach (var m in cliente.Membresias) 
            {
                if (m.Desde < membresia.Desde && m.Hasta > membresia.Desde) 
                {
                    esExcluyente = false;
                    break;
                }
            }
            return esExcluyente;
        }
        #endregion

        #region Salas
        public void AddSala()
        {
            Salas.Add(new Sala("1", Constantes.SALA_TIPO_CONFERENCIA, 50, "Equipado con un estrado, microfono y parlantes"));
            Salas.Add(new Sala("2", Constantes.SALA_TIPO_CELEBRACION, 300, "Equipado con mesas, sillas, consola DJ, bar y luces de fiesta"));
            Salas.Add(new Sala("3", Constantes.SALA_TIPO_REUNION, 20, "Equipaod con mesas y sillas"));
        }

        public Sala GetSala(string id)
        {
            foreach (Sala s in Salas)
            {
                if (s.ID.Equals(id))
                    return s;
            }
            return null;
        }

        public bool DeleteSala(string id)
        {
            Sala s = new Sala(id);
            if (Salas.Contains(s))
            {
                Salas.Remove(s);
                return true;
            }
            return false;
        }

        public bool SalaDisponible(string id, DateTime desde, DateTime hasta)
        {
            foreach(Reserva r in Reservas)
            {
                if (r.Sala.ID.Equals(id))
                {
                    if (r.Desde.Date > hasta.Date)
                        return true;
                    if (r.Hasta.Date < desde.Date)
                        return true;
                    return false;
                }
            }
            return true;
        }
        public List<Sala> LoadSala()
        {
            return this.Salas;
        }
        #endregion

        #region Reserva
        public void NewReserva(Reserva r)
        {
            this.Reservas.Add(r);
        }

        public void AddReserva(DateTime desde, DateTime hasta, Sala sala, Cliente cliente)
        {
            Reserva reserva = new Reserva(cliente, sala, desde, hasta);
            Reservas.Add(reserva);
        }

        public Reserva GetReserva(string idCliente, string idSala, DateTime fechaDesde)
        {
            foreach (Reserva r in Reservas)
            {
                if (r.Cliente.ID.Equals(idCliente) && r.Sala.ID.Equals(idSala) && r.Desde.Equals(fechaDesde))
                    return r;
            }
            return null;
        }

        public Reserva GetReservaById(string idReserva)
        {
            return Reservas.Where(r => r.ID.Equals(idReserva)).FirstOrDefault();
        }

        public bool DeleteReserva(string idCliente, string idSala, DateTime fechaDesde)
        {
            Reserva reserva = Reservas.Where(r => r.Cliente.ID.Equals(idCliente) && r.Sala.ID.Equals(idSala) && r.Desde.Equals(fechaDesde)).FirstOrDefault();
            if (reserva != null)
            {
                Reservas.Remove(reserva);
                return true;
            }
            return false;
        }

        public List<Reserva> LoadReserva(string idCliente)
        {
            List<Reserva> listReserva = new List<Reserva>();
            foreach(Reserva r in Reservas)
            {
                if (r.Cliente.ID.Equals(idCliente))
                    listReserva.Add(r);
            }
            return listReserva;
        }
        #endregion

        #region Factura
        public Factura GetFactura(Cliente cliente, DateTime desde, DateTime hasta)
        {
            foreach (Factura f in Facturas.Where(f => !f.EsPorReserva).ToList())
            {
                if (f.Cliente.ID.Equals(cliente.ID))
                {
                    if (f.Membresia.Desde.Equals(desde) && f.Membresia.Hasta.Equals(hasta)) 
                    {
                        return f;
                    }
                }
            }
            return null;
        }

        public bool DeleteFactura(string idCliente, Membresia membresia = null, Reserva reserva = null)
        {
            Factura factura = Facturas.Where(f => f.Cliente.ID.Equals(idCliente) && f.Reserva.Equals(reserva) && f.Membresia.Equals(membresia)).FirstOrDefault();
            if (factura != null)
            {
                Facturas.Remove(factura);
                return true;
            }
            return false;
        }
        #endregion

        #region GrupoTrabajo
        public void AddGruposTrabajo()
        {
            var grupoNuevo = new GrupoTrabajo() { ID = "1", Nombre = "Grupo 1", Colaboradores = Clientes};
            GruposTrabajo.Add(grupoNuevo);
            Clientes.Where(c => c.ID.Equals("1")).First().GrupoTrabajo = grupoNuevo;
            Clientes.Where(c => c.ID.Equals("2")).First().GrupoTrabajo = grupoNuevo;
            Clientes.Where(c => c.ID.Equals("3")).First().GrupoTrabajo = grupoNuevo;

            Membresia membresia = new Membresia(DateTime.Today, DateTime.Today.AddDays(5), false);
            Clientes.Where(c => c.ID.Equals("1")).First().Membresias.Add(membresia);
        }
        public GrupoTrabajo GetGrupoTrabajo(string idGrupo)
        {
            foreach (GrupoTrabajo g in GruposTrabajo)
            {
                if (g.ID.Equals(idGrupo))
                {
                    return g;
                }
            }
            return null;
        }

        public bool DeleteGrupoTrabajo(string idGrupo)
        {
            GrupoTrabajo grupo = GruposTrabajo.Where(g => g.ID.Equals(idGrupo)).FirstOrDefault();
            if (grupo != null)
            {
                GruposTrabajo.Remove(grupo);
                return true;
            }
            return false;
        }
        #endregion
    }
}
