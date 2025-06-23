using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public class Factura
    {
        [Key]
        public int IdFactura { get; set; }

        [Required]
        public int IdPedido { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal MontoTotal { get; set; }

        [Required]
        [StringLength(20)]
        public string Estado { get; set; } = "pendiente";

        public int IdEmpleado { get; set; }
        public int NumFactura { get; set; }

        public DateTime? FechaDeEmision { get; set; }
        public DateTime? FechaDePago { get; set; }

        public Pedido? Pedido { get; set; }
        public Empleado? Empleado { get; set; }
    }
}
