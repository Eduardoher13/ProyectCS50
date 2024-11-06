namespace ProyectCS50.Models.Dto
{
    public class DetalleIngresoUpdateDto
    {
        public int IdDetalleIngresos { get; set; }

        public int? NumeroEmpleado { get; set; }

        public int? IdIngreso { get; set; }

        public DateTime? FechaIngreso { get; set; }

        public decimal? Monto { get; set; }

    }
}
