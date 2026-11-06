using Xunit;
using Microsoft.EntityFrameworkCore;
using RinconGuatemalteco.Controllers;
using RinconGuatemalteco.Data;
using RinconGuatemalteco.Models;
using System;

namespace RinconGuatemalteco.Tests
{
    public class CajaTests
    {
        [Fact]
        public void CerrarCaja_SoloSumaVentasEnEfectivo()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new ApplicationDbContext(options);

            // Caja de prueba
            var caja = new Caja { FechaApertura = DateTime.Now.AddHours(-2), MontoInicial = 100 };
            context.Cajas.Add(caja);

            // Factura 1: Efectivo
            context.Facturas.Add(new Factura
            {
                Fecha = DateTime.Now,
                MetodoPago = "Efectivo",
                Total = 50,
                PromocionAplicada = "Ninguna" // Corregido
            });

            // Factura 2: Tarjeta
            context.Facturas.Add(new Factura
            {
                Fecha = DateTime.Now,
                MetodoPago = "Tarjeta",
                Total = 200,
                PromocionAplicada = "Ninguna" // Corregido
            });

            context.SaveChanges();

            var controller = new CajaController(context);
            var resultado = controller.CerrarCaja(150);

            var cajaCerrada = context.Cajas.FirstOrDefault();

            Assert.NotNull(cajaCerrada);
            Assert.Equal(50, cajaCerrada.TotalVentas);
        }
    }
}