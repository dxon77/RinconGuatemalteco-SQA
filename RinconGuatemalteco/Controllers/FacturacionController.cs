using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json; // Necesitaremos instalar este paquete
using QRCoder;
using RinconGuatemalteco.Data;
using RinconGuatemalteco.Models;

namespace RinconGuatemalteco.Controllers
{
    public class FacturacionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FacturacionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Pantalla principal
        public IActionResult Index()
        {
            ViewBag.Productos = _context.Productos.ToList();
            ViewBag.Clientes = _context.Clientes.ToList();

            // Recuperamos el carrito de la sesión (si existe)
            var carrito = ObtenerCarrito();
            ViewBag.Carrito = carrito;
            ViewBag.Total = carrito.Sum(x => x.SubTotal);

            return View();
        }

        // Agregar producto al carrito
        [HttpPost]
        public IActionResult AgregarAlCarrito(int productoId, int cantidad)
        {
            if (cantidad <= 0) cantidad = 1;

            var producto = _context.Productos.Find(productoId);
            if (producto == null) return NotFound();

            var carrito = ObtenerCarrito();
            var itemExistente = carrito.FirstOrDefault(x => x.ProductoId == productoId);

            if (itemExistente != null)
            {
                // Si ya existe, sumamos la cantidad
                itemExistente.Cantidad += cantidad;
            }
            else
            {
                // Si no existe, lo agregamos
                carrito.Add(new ItemCarrito
                {
                    ProductoId = producto.Id,
                    Nombre = producto.Nombre,
                    Precio = producto.Precio,
                    Cantidad = cantidad
                });
            }

            GuardarCarrito(carrito);
            return RedirectToAction("Index");
        }

        // Quitar un producto del carrito
        [HttpPost]
        public IActionResult QuitarDelCarrito(int productoId)
        {
            var carrito = ObtenerCarrito();
            var item = carrito.FirstOrDefault(x => x.ProductoId == productoId);
            if (item != null)
            {
                carrito.Remove(item);
            }
            GuardarCarrito(carrito);
            return RedirectToAction("Index");
        }

        // Procesar la venta final
        [HttpPost]
        public IActionResult ProcesarVenta(int clienteId, string metodoPago)
        {
            var cliente = _context.Clientes.Find(clienteId);
            if (cliente == null) return NotFound("Cliente no encontrado");

            var carrito = ObtenerCarrito();
            if (!carrito.Any()) 
    return BadRequest("Error de validación SQA: No se puede procesar una venta con el carrito vacío.");
            var nuevaFactura = new Factura
            {
                ClienteId = clienteId,
                Fecha = DateTime.Now,
                MetodoPago = metodoPago,
                Total = 0,
                PromocionAplicada = "Ninguna"
            };

            _context.Facturas.Add(nuevaFactura);
            _context.SaveChanges();

            decimal totalFactura = 0;
            int puntosGanados = 0;

            foreach (var item in carrito)
            {
                var producto = _context.Productos.Find(item.ProductoId);
                if (producto != null)
                {
                    producto.Stock -= item.Cantidad; // Restamos la cantidad comprada
                    _context.Update(producto);

                    var detalle = new DetalleFactura
                    {
                        FacturaId = nuevaFactura.Id,
                        ProductoId = producto.Id,
                        Cantidad = item.Cantidad,
                        PrecioUnitario = producto.Precio,
                        SubTotal = item.SubTotal
                    };

                    _context.DetalleFacturas.Add(detalle);

                    totalFactura += item.SubTotal;
                    puntosGanados += (producto.PuntosOtorgados * item.Cantidad);
                }
            }

            // Descuento por puntos
            decimal descuento = cliente.PuntosAcumulados / 10;
            if (descuento > 0)
            {
                totalFactura -= descuento;
                cliente.PuntosAcumulados = 0;
                nuevaFactura.PromocionAplicada = "Descuento por Puntos";
            }

            cliente.PuntosAcumulados += puntosGanados;
            _context.Update(cliente);

            nuevaFactura.Total = totalFactura;
            _context.Update(nuevaFactura);
            _context.SaveChanges();

            // Limpiamos el carrito
            GuardarCarrito(new List<ItemCarrito>());

            return RedirectToAction("Ticket", new { id = nuevaFactura.Id });
        }

        public IActionResult Ticket(int id)
        {
            var factura = _context.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.Detalles)
                .ThenInclude(d => d.Producto)
                .FirstOrDefault(f => f.Id == id);

            if (factura == null) return NotFound();
            return View(factura);
        }

        public IActionResult GenerarQR(int id)
        {
            var factura = _context.Facturas.Find(id);
            if (factura == null) return NotFound();

            string textoQR = $"FACTURA:{factura.Id}|PROMOCION:{factura.PromocionAplicada}";
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(textoQR, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeImage = qrCode.GetGraphic(20);

            return File(qrCodeImage, "image/png");
        }

        // --- MÉTODOS DE AYUDA PARA LA SESIÓN ---
        private List<ItemCarrito> ObtenerCarrito()
        {
            var carritoJson = HttpContext.Session.GetString("Carrito");
            if (string.IsNullOrEmpty(carritoJson))
                return new List<ItemCarrito>();

            return JsonConvert.DeserializeObject<List<ItemCarrito>>(carritoJson);
        }

        private void GuardarCarrito(List<ItemCarrito> carrito)
        {
            var carritoJson = JsonConvert.SerializeObject(carrito);
            HttpContext.Session.SetString("Carrito", carritoJson);
        }
    }
}
