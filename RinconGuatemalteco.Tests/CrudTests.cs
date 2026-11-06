using Xunit;
using Microsoft.EntityFrameworkCore;
using RinconGuatemalteco.Controllers;
using RinconGuatemalteco.Data;
using RinconGuatemalteco.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace RinconGuatemalteco.Tests
{
    public class CrudTests
    {
        private ApplicationDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public void Productos_CrearYEditar_Exito()
        {
            using var context = GetContext();
            var controller = new ProductosController(context);

            // 1. Probar el GET (que carga el formulario)
            var getResult = controller.Create();
            Assert.IsType<ViewResult>(getResult);

            // 2. Probar el POST (que guarda)
            var nuevoProducto = new Producto { Nombre = "Test", Categoria = "Test", Precio = 10, Stock = 5, PuntosOtorgados = 1 };
            var postResult = controller.Create(nuevoProducto).Result;
            Assert.IsType<RedirectToActionResult>(postResult);

            // 3. Probar el Details
            var productoGuardado = context.Productos.First();
            var detailsResult = controller.Details(productoGuardado.Id);
            Assert.NotNull(detailsResult);
        }

        [Fact]
        public void Clientes_CrearYEditar_Exito()
        {
            using var context = GetContext();
            var controller = new ClientesController(context);

            // GET
            var getResult = controller.Create();
            Assert.IsType<ViewResult>(getResult);

            // POST
            var nuevoCliente = new Cliente { Nombre = "Test", Nit = "123", Telefono = "555", Direccion = "Z", PuntosAcumulados = 0 };
            var postResult = controller.Create(nuevoCliente).Result;
            Assert.IsType<RedirectToActionResult>(postResult);
        }

        [Fact]
        public void Proveedores_CrearYEditar_Exito()
        {
            using var context = GetContext();
            var controller = new ProveedoresController(context);

            // GET
            var getResult = controller.Create();
            Assert.IsType<ViewResult>(getResult);

            // POST
            var nuevoProveedor = new Proveedor { Nombre = "Test", Nit = "123", Telefono = "555", Direccion = "Z" };
            var postResult = controller.Create(nuevoProveedor).Result;
            Assert.IsType<RedirectToActionResult>(postResult);
        }

        [Fact]
        public void Facturacion_GenerarQR_RetornaArchivo()
        {
            using var context = GetContext();
            // Crear una factura falsa para que el QR no falle
            var factura = new Factura { Fecha = DateTime.Now, Total = 100, MetodoPago = "Efectivo", PromocionAplicada = "Ninguna", ClienteId = 1 };
            context.Facturas.Add(factura);
            context.SaveChanges();

            var controller = new FacturacionController(context);
            
            // Probar que el QR se genera
            var qrResult = controller.GenerarQR(factura.Id);
            Assert.IsType<FileContentResult>(qrResult);
        }
    }
}
