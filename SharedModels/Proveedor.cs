using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public class Proveedor
    {
        [Key]
        public int IdProveedor { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreProveedor { get; set; }

        [Required]
        [StringLength(15)]
        public string Telefono { get; set; }

        public ICollection<DetalleProveedor>? Detalles { get; set; }
    }

}
