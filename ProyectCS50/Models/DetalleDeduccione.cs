using System;
using System.Collections.Generic;

namespace ProyectCS50.Models;

public partial class DetalleDeduccione
{
    public int IdDetalleDeducciones { get; set; }

    public int? NumeroEmpleado { get; set; }

    public int? IdDeduccion { get; set; }

    public DateTime? FechaDeduccion { get; set; }

    public decimal? Monto { get; set; }

    public virtual Deduccione? IdDeduccionNavigation { get; set; }

    public virtual Empleado? NumeroEmpleadoNavigation { get; set; }
}
