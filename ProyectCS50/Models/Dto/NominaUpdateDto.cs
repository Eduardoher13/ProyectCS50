namespace ProyectCS50.Models.Dto
{
    public class NominaUpdateDto
    {
        public int IdNomina { get; set; }

        public int? NumeroEmpleado { get; set; }

   

        public decimal? IngresosTotales { get; set; }

        public decimal? DeduccionesTotales { get; set; }

        public decimal? SalarioNeto { get; set; }

        public DateOnly? Fecha { get; set; }
    }
}
