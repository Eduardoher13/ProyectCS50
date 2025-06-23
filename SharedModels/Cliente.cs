using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(15)]
        [Phone]
        public string Telefono { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreCliente { get; set; }

        public ICollection<Pedido>? Pedidos { get; set; }
    }

}
