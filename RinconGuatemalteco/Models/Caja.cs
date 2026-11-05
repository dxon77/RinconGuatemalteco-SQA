using System.ComponentModel.DataAnnotations;

namespace RinconGuatemalteco.Models
{
    public class Caja
    {
        public int Id { get; set; }

        public DateTime FechaApertura { get; set; }

        public decimal MontoInicial { get; set; }

        public DateTime? FechaCierre { get; set; }

        public decimal? MontoFinal { get; set; }

        public decimal? TotalVentas { get; set; }
    }
}