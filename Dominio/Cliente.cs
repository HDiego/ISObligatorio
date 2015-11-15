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

    public class ClientMetadata 
    {
        [DisplayName("Identificador")]
        [Required(ErrorMessage = "Debe ingresar el identificador")]
        public string ID { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar el apellido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Debe ingresar la Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Debe ingresar el email")]
        [RegularExpression(@"(\w|\.)+@(\w|\.)+", ErrorMessage = "El email no es válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe ingresar la contraseña")]
        public string Contraseña { get; set; }
    }
}
