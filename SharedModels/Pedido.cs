using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public class Pedido
    {
        [Key]
        public int IdPedido { get; set; }

        [Required]
        public int IdCliente { get; set; }

        [StringLength(300)]
        public string? Descripcion { get; set; }

        [Required]
        public DateTime FechaSolicitud { get; set; }

        [Required]
        public DateTime FechaEntrega { get; set; }

        [StringLength(200)]
        public string? EnviarseA { get; set; }

        [Required]
        [StringLength(20)]
        public string Estado { get; set; } = "pendiente";

        public Cliente? Cliente { get; set; }
        public ICollection<DetallePedido>? Detalles { get; set; }
    }
}
