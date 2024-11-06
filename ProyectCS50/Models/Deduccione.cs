using System;
using System.Collections.Generic;

namespace ProyectCS50.Models;

public partial class Deduccione
{
    public int IdDeduccion { get; set; }

    public string NombreDeduccion { get; set; } = null!;

    public virtual ICollection<DetalleDeduccione> DetalleDeducciones { get; set; } = new List<DetalleDeduccione>();
}
