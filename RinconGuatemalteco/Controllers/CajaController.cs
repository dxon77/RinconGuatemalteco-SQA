using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RinconGuatemalteco.Data;
using RinconGuatemalteco.Models;

namespace RinconGuatemalteco.Controllers
{
    public class CajaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CajaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Pantalla para abrir o cerrar la caja
        public IActionResult Index()
        {
            // Buscamos si hay una caja abierta
            var cajaAbierta = _context.Cajas.FirstOrDefault(c => c.FechaCierre == null);

            if (cajaAbierta != null)
            {
                ViewBag.MontoInicial = cajaAbierta.MontoInicial;
                ViewBag.FechaApertura = cajaAbierta.FechaApertura;
            }

            return View(cajaAbierta);
        }

        // Acción para abrir la caja con un monto inicial
        [HttpPost]
        public IActionResult AbrirCaja(decimal montoInicial)
        {
            // Verificamos que no haya otra caja abierta
            if (_context.Cajas.Any(c => c.FechaCierre == null))
            {
                return BadRequest("Ya hay una caja abierta.");
            }

            var nuevaCaja = new Caja
            {
                FechaApertura = DateTime.Now, // Guardamos la hora exacta de apertura
                MontoInicial = montoInicial
            };

            _context.Cajas.Add(nuevaCaja);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // Acción para hacer el corte de caja
        [HttpPost]
        public IActionResult CerrarCaja(decimal montoFinal)
        {
            var cajaAbierta = _context.Cajas.FirstOrDefault(c => c.FechaCierre == null);
            if (cajaAbierta == null) return BadRequest("No hay caja abierta.");

            // CORRECCIÓN: Solo sumamos las facturas en EFECTIVO hechas desde que se abrió esta caja
            var ventasEnEfectivo = _context.Facturas
                .Where(f => f.Fecha >= cajaAbierta.FechaApertura && f.MetodoPago == "Efectivo")
                .Sum(f => f.Total);

            cajaAbierta.FechaCierre = DateTime.Now;
            cajaAbierta.MontoFinal = montoFinal;
            cajaAbierta.TotalVentas = ventasEnEfectivo; // Guardamos solo el efectivo

            _context.Update(cajaAbierta);
            _context.SaveChanges();

            return RedirectToAction("ReporteCierre", new { id = cajaAbierta.Id });
        }

        // Pantalla que muestra el reporte de cierre
        public IActionResult ReporteCierre(int id)
        {
            var caja = _context.Cajas.Find(id);
            if (caja == null) return NotFound();

            return View(caja);
        }
    }
}