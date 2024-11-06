using System;
using System.Collections.Generic;

namespace ProyectCS50.Models;

public partial class Nomina
{
    public int IdNomina { get; set; }

    public int? NumeroEmpleado { get; set; }

    public decimal? IngresosTotales { get; set; }

    public decimal? DeduccionesTotales { get; set; }

    public decimal? SalarioNeto { get; set; }

    public DateOnly? Fecha { get; set; }

    public virtual Empleado? NumeroEmpleadoNavigation { get; set; }
}
