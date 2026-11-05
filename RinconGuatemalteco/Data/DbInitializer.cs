using RinconGuatemalteco.Models;

namespace RinconGuatemalteco.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // 1. Si ya hay clientes, no hacemos nada
            if (context.Clientes.Any())
            {
                return;
            }

            // 2. Crear clientes automáticos (incluyendo al Consumidor Final)
            var clientes = new Cliente[]
            {
                // Este siempre debe existir para las ventas rápidas
                new Cliente { Nombre = "Consumidor Final", Nit = "CF", Telefono = "", Direccion = "Ciudad", PuntosAcumulados = 0 },

                new Cliente { Nombre = "Cliente Prueba 1", Nit = "1234567", Telefono = "5555-1111", Direccion = "Zona 1", PuntosAcumulados = 15 },
                new Cliente { Nombre = "Cliente Prueba 2", Nit = "1234568", Telefono = "5555-2222", Direccion = "Zona 2", PuntosAcumulados = 0 },
                new Cliente { Nombre = "Cliente Prueba 3", Nit = "1234569", Telefono = "5555-3333", Direccion = "Zona 3", PuntosAcumulados = 5 },
                new Cliente { Nombre = "Cliente Prueba 4", Nit = "1234570", Telefono = "5555-4444", Direccion = "Zona 4", PuntosAcumulados = 0 },
                new Cliente { Nombre = "Cliente Prueba 5", Nit = "1234571", Telefono = "5555-5555", Direccion = "Zona 5", PuntosAcumulados = 0 },
                new Cliente { Nombre = "Cliente Prueba 6", Nit = "1234572", Telefono = "5555-6666", Direccion = "Zona 6", PuntosAcumulados = 50 },
                new Cliente { Nombre = "Cliente Prueba 7", Nit = "1234573", Telefono = "5555-7777", Direccion = "Zona 7", PuntosAcumulados = 0 },
                new Cliente { Nombre = "Cliente Prueba 8", Nit = "1234574", Telefono = "5555-8888", Direccion = "Zona 8", PuntosAcumulados = 0 },
                new Cliente { Nombre = "Cliente Prueba 9", Nit = "1234575", Telefono = "5555-9999", Direccion = "Zona 9", PuntosAcumulados = 10 },
                new Cliente { Nombre = "Cliente Prueba 10", Nit = "1234576", Telefono = "5555-0000", Direccion = "Zona 10", PuntosAcumulados = 0 }
            };
            context.Clientes.AddRange(clientes);
            context.SaveChanges();

            // 3. Crear 50 productos automáticos
            var productos = new List<Producto>();
            string[] categorias = { "Pastas", "Cárnicos", "Embutidos", "Lácteos", "Golosinas" };

            foreach (var cat in categorias)
            {
                for (int i = 1; i <= 10; i++)
                {
                    productos.Add(new Producto
                    {
                        Nombre = $"{cat} Producto {i}",
                        Categoria = cat,
                        Precio = 10.00m + (i * 2),
                        Stock = 50,
                        PuntosOtorgados = i
                    });
                }
            }
            context.Productos.AddRange(productos);
            context.SaveChanges();
        }
    }
}