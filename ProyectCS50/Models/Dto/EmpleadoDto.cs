namespace ProyectCS50.Models.Dto
{
    public class EmpleadoDto
    {
        public int NumeroEmpleado { get; set; }

        public string Cedula { get; set; }

        public string? NInss { get; set; }

        public string? NRuc { get; set; }

        public string PrimerNombre { get; set; }

        public string SegundoNombre { get; set; }

        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }

        public string Sexo { get; set; }

        public DateTime? FechaNac { get; set; }

        public string EstadoCivil { get; set; }

        public string? Direccion { get; set; }

        public string Telefono { get; set; }

        public string Celular { get; set; }

        public DateTime FechaContratacion { get; set; }

        public DateTime FechaCierreContrato { get; set; }
        public decimal? SalarioOrdinario { get; set; }

        public bool? Estado { get; set; }

        public int? YearsTrabajados { get; set; }

    }
}
