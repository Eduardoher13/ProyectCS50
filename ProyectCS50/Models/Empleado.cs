using System;
using System.Collections.Generic;

namespace ProyectCS50.Models;

public partial class Empleado
{
    public int NumeroEmpleado { get; set; }

    public string Cedula { get; set; } = null!;

    public string? NInss { get; set; }

    public string? NRuc { get; set; }

    public string PrimerNombre { get; set; } = null!;

    public string SegundoNombre { get; set; } = null!;

    public string PrimerApellido { get; set; } = null!;

    public string SegundoApellido { get; set; } = null!;

    public string Sexo { get; set; } = null!;

    public DateTime? FechaNac { get; set; }

    public string EstadoCivil { get; set; } = null!;

    public string? Direccion { get; set; }

    public string Telefono { get; set; } = null!;

    public string Celular { get; set; } = null!;

    public DateTime FechaContratacion { get; set; }

    public DateTime FechaCierreContrato { get; set; }

    public decimal? SalarioOrdinario { get; set; }

    public bool? Estado { get; set; }

    public int? YearsTrabajados { get; set; }

    public virtual ICollection<DetalleDeduccione> DetalleDeducciones { get; set; } = new List<DetalleDeduccione>();

    public virtual ICollection<DetalleIngreso> DetalleIngresos { get; set; } = new List<DetalleIngreso>();

    public virtual ICollection<Nomina> Nominas { get; set; } = new List<Nomina>();
}
