using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess;
using Logic;

namespace UnitTestColabora
{
    [TestClass]
    public class DataAccess
    {
        [TestMethod]
        public void ConstructorClientes()
        {
            var obtenido = Singleton.GetInstance();
            Assert.AreEqual(3, obtenido.Clientes.Count);
        }

        [TestMethod]
        public void ConstructorSala()
        {
            var obtenido = Singleton.GetInstance();
            Assert.AreEqual(3, obtenido.Salas.Count);
        }

        [TestMethod]
        public void ConstructorGrupoTrabajo()
        {
            var obtenido = Singleton.GetInstance();
            Assert.AreEqual(1, obtenido.GruposTrabajo.Count);
        }

        [TestMethod]
        public void GetClientePorID()
        {
            var BD = Singleton.GetInstance();
            var obtenido = BD.GetClientePorID("1");
            var esperado = BD.Clientes.Find(c => c.ID.Equals("1"));
            Assert.AreEqual(esperado, obtenido);
        }

        [TestMethod]
        public void GetCliente()
        {
            var BD = Singleton.GetInstance();
            var obtenido = BD.GetCliente("mauriciodelbonofripp@gmail.com");
            var esperado = BD.Clientes.Find(c => c.Email.Equals("mauriciodelbonofripp@gmail.com"));
            Assert.AreEqual(esperado, obtenido);
        }

        [TestMethod]
        public void GetClienteID()
        {
            var BD = Singleton.GetInstance();
            var obtenido = BD.GetClienteID();
            Assert.AreEqual("CWS_01", obtenido);
        }

        [TestMethod]
        public void DeleteCliente()
        {
            var BD = Singleton.GetInstance();
            BD.DeleteCliente("mauriciodelbonofripp@gmail.com");
            var obtenido = BD.Clientes.Count;
            var esperado = 2;
            Assert.AreEqual(esperado, obtenido);
        }

        [TestMethod]
        public void ValidLogIn()
        {
            var BD = Singleton.GetInstance();
            var obtenido = BD.ValidLogIn("2", "1234");
            var esperado = BD.GetCliente("mauriciodelbonofripp@gmail.com");
            Assert.AreEqual(esperado, obtenido);
        }

        [TestMethod]
        public void YaExisteIDCliente()
        {
            var BD = Singleton.GetInstance();
            var obtenido = BD.YaExisteIDCliente("2");
            Assert.AreEqual(true, obtenido);
        }

        [TestMethod]
        public void EsPeriodoMembresiaExcluyente()
        {
            var BD = Singleton.GetInstance();
            Membresia membresia = new Membresia(DateTime.Today.AddDays(-1), DateTime.Today.AddDays(1), true);
            var cliente = BD.GetClientePorID("1");
            cliente.Membresias.Add(membresia);

            Membresia membresiaSolapada = new Membresia(DateTime.Today, DateTime.Today.AddDays(2), true);
            var obtenido = BD.EsPeriodoMembresiaExcluyente(cliente, membresiaSolapada);
            Assert.AreEqual(false, obtenido);
        }

        [TestMethod]
        public void GetSala()
        {
            var BD = Singleton.GetInstance();
            var obtenido = BD.GetSala("1");
            var esperado = BD.Salas.Find(s => s.ID.Equals("1"));
            Assert.AreEqual(esperado, obtenido);
        }

        [TestMethod]
        public void DeleteSala()
        {
            var BD = Singleton.GetInstance();
            var obtenido = BD.Salas.Count;
            BD.DeleteSala("1");
            var esperado = BD.Salas.Count;
            Assert.AreEqual(esperado, obtenido);
        }

        [TestMethod]
        public void SalaDisponible_Valido()
        {
            var BD = Singleton.GetInstance();
            var obtenido = BD.SalaDisponible("1", DateTime.Today, DateTime.Today.AddDays(1));
            Assert.AreEqual(true, obtenido);
        }

        [TestMethod]
        public void SalaDisponible_Invalido()
        {
            var BD = Singleton.GetInstance();
            var sala = BD.GetSala("1");
            var cliente = BD.GetClientePorID("1");
            BD.AddReserva(DateTime.Today, DateTime.Today.AddDays(5), sala, cliente);
            var obtenido = BD.SalaDisponible("1", DateTime.Today, DateTime.Today.AddDays(1));
            Assert.AreEqual(false, obtenido);
        }

        [TestMethod]
        public void LoadSalas()
        {
            var BD = Singleton.GetInstance();
            var obtenido = BD.Salas;
            var esperado = BD.LoadSala();
            Assert.AreEqual(esperado, obtenido);
        }

        [TestMethod]
        public void LoadReservas()
        {
            var BD = Singleton.GetInstance();
            var sala1 = BD.GetSala("1");
            var sala2 = BD.GetSala("2");
            var cliente = BD.GetClientePorID("1");
            BD.AddReserva(DateTime.Today, DateTime.Today.AddDays(5), sala1, cliente);
            BD.AddReserva(DateTime.Today.AddDays(10), DateTime.Today.AddDays(20), sala2, cliente);

            var obtenido = BD.Reservas.Count;
            var esperado = BD.LoadReserva("1").Count;
            Assert.AreEqual(esperado, obtenido);
        }

        [TestMethod]
        public void NewReserva()
        {
            var BD = Singleton.GetInstance();
            var sala = BD.GetSala("2");
            var cliente = BD.GetClientePorID("1");
            BD.NewReserva(new Reserva(cliente, sala, DateTime.Today, DateTime.Today.AddDays(5)));
            var obtenido = BD.Reservas.Count;
            var esperado = 1;
            Assert.AreEqual(esperado, obtenido);
        }

        [TestMethod]
        public void AddReserva()
        {
            var BD = Singleton.GetInstance();
            var sala = BD.GetSala("2");
            var cliente = BD.GetClientePorID("1");
            BD.AddReserva(DateTime.Today, DateTime.Today.AddDays(5), sala, cliente);
            var obtenido = BD.Reservas.Count;
            var esperado = 1;
            Assert.AreEqual(esperado, obtenido);
        }

        [TestMethod]
        public void GetReservaById()
        {
            var BD = Singleton.GetInstance();
            var sala = BD.GetSala("2");
            var cliente = BD.GetClientePorID("1");
            var reserva = new Reserva(cliente, sala, DateTime.Today, DateTime.Today.AddDays(5));
            BD.NewReserva(reserva);

            var obtenido = BD.GetReservaById(reserva.ID);
            Assert.AreEqual(reserva, obtenido);
        }

        [TestMethod]
        public void GetReserva()
        {
            var BD = Singleton.GetInstance();
            var sala = BD.GetSala("2");
            var cliente = BD.GetClientePorID("1");
            var fechaDesde = DateTime.Today;
            var reserva = new Reserva(cliente, sala, fechaDesde, DateTime.Today.AddDays(5));
            BD.NewReserva(reserva);

            var obtenido = BD.GetReserva(cliente.ID, sala.ID, fechaDesde);
            Assert.AreEqual(reserva, obtenido);
        }

        [TestMethod]
        public void DeleteReserva()
        {
            var BD = Singleton.GetInstance();
            var sala = BD.GetSala("2");
            var cliente = BD.GetClientePorID("1");
            var fechaDesde = DateTime.Today;
            BD.AddReserva(fechaDesde, DateTime.Today.AddDays(5), sala, cliente);
            BD.DeleteReserva(cliente.ID, sala.ID, fechaDesde);
            var obtenido = BD.Reservas.Count;
            var esperado = 0;
            Assert.AreEqual(esperado, obtenido);
        }

        [TestMethod]
        public void GetGrupoTrabajo()
        {
            var BD = Singleton.GetInstance();
            var obtenido = BD.GetGrupoTrabajo("1");
            var esperado = BD.GruposTrabajo.Find(g => g.ID.Equals("1"));
            Assert.AreEqual(esperado, obtenido);
        }

        [TestMethod]
        public void DeleteGrupoTrabajo()
        {
            var BD = Singleton.GetInstance();
            BD.DeleteGrupoTrabajo("1");
            var obtenido = BD.GruposTrabajo.Count;
            var esperado = 0;
            Assert.AreEqual(esperado, obtenido);
        }
    }
}
