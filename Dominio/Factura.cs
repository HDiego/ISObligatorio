using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Factura
    {
        public Cliente Cliente { get; set; }
        public Reserva Reserva { get; set; }
        public double TotalAPagar { get; set; }
        public double IVA { get; set; }
        public bool EsPorReserva { get; set; }
        public Membresia Membresia { get; set; }

        public Factura(bool esPorReserva)
        {
            this.EsPorReserva = esPorReserva;
        }
    }
}
