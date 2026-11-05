using System.ComponentModel.DataAnnotations;

namespace RinconGuatemalteco.Models
{
    public class Proveedor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del proveedor es obligatorio")]
        public string Nombre { get; set; }

        public string Nit { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }
    }
}