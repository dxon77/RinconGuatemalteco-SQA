using System.ComponentModel.DataAnnotations;

namespace RinconGuatemalteco.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del cliente es obligatorio")]
        public string Nombre { get; set; }

        public string Nit { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }

        // Puntos acumulados para usar en su proxima compra
        public int PuntosAcumulados { get; set; }
    }
}