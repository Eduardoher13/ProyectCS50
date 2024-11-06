using System;
using System.Collections.Generic;

namespace ProyectCS50.Models;

public partial class DetalleIngreso
{
    public int IdDetalleIngresos { get; set; }

    public int? NumeroEmpleado { get; set; }

    public int? IdIngreso { get; set; }

    public DateTime? FechaIngreso { get; set; }

    public decimal? Monto { get; set; }

    public virtual Ingreso? IdIngresoNavigation { get; set; }

    public virtual Empleado? NumeroEmpleadoNavigation { get; set; }
}
