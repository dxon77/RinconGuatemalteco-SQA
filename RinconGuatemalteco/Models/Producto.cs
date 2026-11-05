using System.ComponentModel.DataAnnotations;

namespace RinconGuatemalteco.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        // Pastas, Cárnicos, Embutidos, Lácteos, Golosinas, Gaseosas, Carbonatados
        public string Categoria { get; set; }

        [Range(0.01, 9999.99, ErrorMessage = "El precio debe ser mayor a cero")]
        public decimal Precio { get; set; }

        public int Stock { get; set; }

        // Puntos que gana el cliente al comprar este producto
        public int PuntosOtorgados { get; set; }
    }
}