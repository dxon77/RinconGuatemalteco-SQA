using System.ComponentModel.DataAnnotations;

namespace RinconGuatemalteco.Models
{
    public class Factura
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public DateTime Fecha { get; set; }

        public decimal Total { get; set; }

        // Efectivo, Tarjeta de Credito, Tarjeta de Debito
        public string MetodoPago { get; set; }

        // Aqui guardo el texto de la promocion que se genero con el QR
        public string PromocionAplicada { get; set; }

        public List<DetalleFactura> Detalles { get; set; }
    }
}