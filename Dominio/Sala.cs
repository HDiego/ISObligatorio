using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Sala
    {
        public string ID { get; set; }
        public string TipoSala { get; set; }
        public int CapacidadMaxima { get; set; }
        public string Descripcion { get; set; }

        public Sala(string id, string tipo, int capacidad, string descripcion)
        {
            this.ID = id;
            this.TipoSala = tipo;
            this.CapacidadMaxima = capacidad;
            this.Descripcion = descripcion;
        }

        public Sala(string id)
        {
            this.ID = id;
        }
    }
}
