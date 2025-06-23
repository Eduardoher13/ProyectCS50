using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public class Envio
    {
        [Key]
        public int IdEnvio { get; set; }

        [Required]
        public int IdPedido { get; set; }

        [Required]
        [StringLength(100)]
        public string Direccion { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreDestinatario { get; set; }

        [Required]
        [StringLength(50)]
        public string ApellidoDestinatario { get; set; }

        [Required]
        [Phone]
        [StringLength(15)]
        public string TelefonoDestinatario { get; set; }

        public Pedido? Pedido { get; set; }
    }
}
