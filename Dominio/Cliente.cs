using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    [MetadataType(typeof(ClientMetadata))]
    public partial class Cliente
    {
        public string ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public GrupoTrabajo GrupoTrabajo { get; set; }
        public List<Membresia> Membresias { get; set; }

        public Cliente() 
        {
            Membresias = new List<Membresia>();
        }

        public Cliente(string id, string nombre, string apellido, string direccion, string contraseña, string email)
        {
            Membresias = new List<Membresia>();
            this.Email = email;
            this.Nombre = nombre;
            this.Contraseña = contraseña;
            this.Apellido = apellido;
            this.ID = id;
            this.Direccion = direccion;
        }

        public Cliente(string email)
        {
            Membresias = new List<Membresia>();
            this.Email = email;
        }
    }

    public class ClientMetadata 
    {
        [DisplayName("Identificador")]
        [Required(ErrorMessage = "Debe ingresar el identificador")]
        public string ID { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar el apellido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Debe ingresar la dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Debe ingresar el email")]
        [RegularExpression(@"(\w|\.)+@(\w|\.)+", ErrorMessage = "El email no es válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe ingresar la contraseña")]
        public string Contraseña { get; set; }
    }
}
