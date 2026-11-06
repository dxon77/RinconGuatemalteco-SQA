using Xunit;
using Microsoft.EntityFrameworkCore;
using RinconGuatemalteco.Controllers;
using RinconGuatemalteco.Data;
using RinconGuatemalteco.Models;
using System;

namespace RinconGuatemalteco.Tests
{
    public class OtrosTests
    {
        private ApplicationDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public void ClientesController_Index_RetornaVista()
        {
            using var context = GetContext();
            var controller = new ClientesController(context);
            var result = controller.Index(); // Asegúrate de que el método sea síncrono o usa await si es async
            Assert.NotNull(result);
        }

        [Fact]
        public void ProductosController_Index_RetornaVista()
        {
            using var context = GetContext();
            var controller = new ProductosController(context);
            var result = controller.Index();
            Assert.NotNull(result);
        }

        [Fact]
        public void ProveedoresController_Index_RetornaVista()
        {
            using var context = GetContext();
            var controller = new ProveedoresController(context);
            var result = controller.Index();
            Assert.NotNull(result);
        }
    }
}
