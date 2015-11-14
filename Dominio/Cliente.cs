using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Cliente
    {
        public string ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public List<Tuple<DateTime, DateTime, string>> PeriodosMembresia { get; set; }
        public GrupoTrabajo GrupoTrabajo { get; set; }
        public List<Membresia> Membresia { get; set; }

        public Cliente() 
        {
        }

        public Cliente(string nombre, string contraseña, string email, string apellido)
        {
            this.Email = email;
            this.Nombre = nombre;
            this.Contraseña = contraseña;
            this.Apellido = apellido;
        }

        public Cliente(string email)
        {
            this.Email = email;
        }
    }
}
