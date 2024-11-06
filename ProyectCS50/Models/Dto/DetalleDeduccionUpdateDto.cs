namespace ProyectCS50.Models.Dto
{
    public class DetalleDeduccionUpdateDto
    {
        public int IdDetalleDeducciones { get; set; }

        public int? NumeroEmpleado { get; set; }

        public int? IdDeduccion { get; set; }

        public DateTime? FechaDeduccion { get; set; }

        public decimal? Monto { get; set; }
    }
}
