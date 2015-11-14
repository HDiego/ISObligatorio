using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class GrupoTrabajo
    {
        public string ID { get; set; }
        public string Nombre { get; set; }
        public List<Cliente> Colaboradores { get; set; }

        public GrupoTrabajo() 
        {
        }
    }
}
