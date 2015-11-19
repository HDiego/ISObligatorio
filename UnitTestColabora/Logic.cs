using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using DataAccess;

namespace UnitTestColabora
{
    [TestClass]
    public class Logic
    {
        [TestMethod]
        public void Membresia_CalcularTotal()
        {
            Membresia membresia = new Membresia(DateTime.Today, DateTime.Today.AddDays(5), false);
            double obtenido = membresia.CalcularTotal();
            Assert.AreEqual(61, obtenido);
        }

        [TestMethod]
        public void Membresia_CalcularImpuestos()
        {
            Membresia membresia = new Membresia(DateTime.Today, DateTime.Today.AddDays(5), false);
            double obtenido = membresia.CalcularImpuestos();
            Assert.AreEqual(11, obtenido);
        }

        [TestMethod]
        public void Reserva_SePuedeNotificar_Valido()
        {
            var BD = Singleton.GetInstance();
            Reserva reserva = new Reserva(BD.GetClientePorID("1"), BD.GetSala("1"), DateTime.Today, DateTime.Today.AddDays(5));
            var obtenido = reserva.SePuedeNotificar();
            Assert.AreEqual(true, obtenido);
        }

        [TestMethod]
        public void Reserva_SePuedeNotificar_Invalido()
        {
            var BD = Singleton.GetInstance();
            Reserva reserva = new Reserva(BD.GetClientePorID("1"), BD.GetSala("1"), DateTime.Today.AddDays(-5), DateTime.Today);
            var obtenido = reserva.SePuedeNotificar();
            Assert.AreEqual(false, obtenido);
        }
    }
}
