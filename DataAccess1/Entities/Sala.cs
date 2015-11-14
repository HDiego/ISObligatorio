using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess1.Entities
{
    public class Sala
    {
        public string ID { get; set; }
        public string TipoSala { get; set; }
        public int CapacidadMaxima { get; set; }
        public string Descripcion { get; set; }

        public Sala() 
        {
        }
    }
}
