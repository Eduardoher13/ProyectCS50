using System;
using System.Collections.Generic;

namespace ProyectCS50.Models;

public partial class Ingreso
{
    public int IdIngreso { get; set; }

    public string NombreIngreso { get; set; } = null!;

    public virtual ICollection<DetalleIngreso> DetalleIngresos { get; set; } = new List<DetalleIngreso>();
}
