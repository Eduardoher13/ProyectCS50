using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public class DetallePedido
    {
        [Key]
        public int IdDetallePedido { get; set; }

        [Required]
        public int IdPedido { get; set; }

        [Required]
        public int IdArreglo { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PrecioUnitario { get; set; }

        public Pedido? Pedido { get; set; }
        public Arreglo? Arreglo { get; set; }
    }

}
