using DataAccess1.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    [MetadataType(typeof(MembresiaMetadata))]
    public class Membresia
    {
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public bool EsTotal { get; set; }

        public Membresia(DateTime desde, DateTime hasta, bool estotal) 
        {
            this.Desde = desde;
            this.Hasta = hasta;
            this.EsTotal = estotal;
        } 

        public double CalcularTotal()
        {
            if(EsTotal)
                return this.Hasta.Subtract(this.Desde).Days * Constantes.PRECIO_MEMBRESIA_TOTAL * (Constantes.PORCENTAJE_IVA/100 + 1);
            else
                return this.Hasta.Subtract(this.Desde).Days * Constantes.PRECIO_MEMBRESIA_PARCIAL * (Constantes.PORCENTAJE_IVA / 100 + 1);
        }

        public double CalcularImpuestos()
        {
            if (EsTotal)
                return this.Hasta.Subtract(this.Desde).Days * Constantes.PRECIO_MEMBRESIA_TOTAL * (Constantes.PORCENTAJE_IVA / 100);
            else
                return this.Hasta.Subtract(this.Desde).Days * Constantes.PRECIO_MEMBRESIA_PARCIAL * (Constantes.PORCENTAJE_IVA / 100);            
        }
    }

    public class MembresiaMetadata
    {
        [Required(ErrorMessage = "Debe ingresar la fecha de inicio")]
        public DateTime Desde { get; set; }

        [Required(ErrorMessage = "Debe ingresar la fecha de fin")]
        public DateTime Hasta { get; set; }

        [Required(ErrorMessage = "Debe ingresar el tipo de membresia")]
        public bool EsTotal { get; set; }
    }
}
