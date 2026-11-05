using Microsoft.EntityFrameworkCore;
using RinconGuatemalteco.Models;

namespace RinconGuatemalteco.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }

        // Estas son las líneas nuevas para la facturación
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<DetalleFactura> DetalleFacturas { get; set; }

        public DbSet<Caja> Cajas { get; set; }
    }
}