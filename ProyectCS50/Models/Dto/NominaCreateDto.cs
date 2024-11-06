namespace ProyectCS50.Models.Dto
{
    public class NominaCreateDto
    {


        public int? NumeroEmpleado { get; set; }

        

        public decimal? IngresosTotales { get; set; }

        public decimal? DeduccionesTotales { get; set; }

        public decimal? SalarioNeto { get; set; }

        public DateOnly? Fecha { get; set; }
    }
}
