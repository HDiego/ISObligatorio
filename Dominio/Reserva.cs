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
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public Cliente Cliente { get; set; }
        public Sala Sala { get; set; }
        
        public Reserva(Cliente cliente, Sala sala, DateTime fechaDesde, DateTime fechaHasta) 
        {
            this.Desde = fechaDesde;
            this.Hasta = fechaHasta;
            this.Cliente = cliente;
            this.Sala = sala;
        }

        public Reserva()
        {
            this.Desde = DateTime.Now.Date;
            this.Hasta = DateTime.Now.Date;
            this.Sala = new Sala();
        }
    }
}
