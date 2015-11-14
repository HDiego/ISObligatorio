using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess1.Entities
{
    public class FacturaReserva
    {
        public Reserva Reserva { get; set; }
        public double TotalAPagar { get; set; }
        public double IVA { get; set; }

        public FacturaReserva() 
        {
        }
    }
}
