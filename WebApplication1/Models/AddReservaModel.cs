using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Colabora.Models
{
    public class AddReservaModel
    {
        public Reserva Reserva { get; set; }

        public List<Sala> ListSala { get; set; }
    }
}