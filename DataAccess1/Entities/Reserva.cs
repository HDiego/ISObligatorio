using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess1.Entities
{
    public class Reserva
    {
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public Cliente Cliente { get; set; }
        public Sala Sala { get; set; }
        
        public Reserva() 
        {
        }
    }
}
