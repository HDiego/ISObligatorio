using DataAccess1.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Reserva
    {
        public string ID { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public Cliente Cliente { get; set; }
        public Sala Sala { get; set; }
        public bool SeNotifico { get; set; }
        
        public Reserva(Cliente cliente, Sala sala, DateTime fechaDesde, DateTime fechaHasta) 
        {
            this.Desde = fechaDesde;
            this.Hasta = fechaHasta;
            this.Cliente = cliente;
            this.Sala = sala;
            this.SeNotifico = false;
            this.ID = cliente.ID + sala.ID + fechaDesde.Day + fechaDesde.Month + fechaDesde.Year;
        }

        public Reserva()
        {
            this.Desde = DateTime.Now.Date;
            this.Hasta = DateTime.Now.Date;
            this.Sala = new Sala();
            this.SeNotifico = false;
        }

        public bool SePuedeNotificar()
        {
            if (!SeNotifico && DateTime.Now < Desde.AddHours(23)) 
            {
                return true;
            }
            return false;
        }

        public double CalcularTotal()
        {
            return this.Hasta.Subtract(this.Desde).Days * Constantes.PRECIO_RESERVA_SALA * (Constantes.PORCENTAJE_IVA / 100 + 1);
        }

        public double CalcularImpuestos()
        {
            return this.Hasta.Subtract(this.Desde).Days * Constantes.PRECIO_RESERVA_SALA * (Constantes.PORCENTAJE_IVA / 100);
        }
    }
}
