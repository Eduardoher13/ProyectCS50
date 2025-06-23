using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public class DetalleProveedor
    {
        [Key]
        public int IdDetalleProveedor { get; set; }

        // Opcionales, porque en la base pueden ser NULL
        public int? IdAccesorio { get; set; }
        public int? IdFlor { get; set; }

        [Required]
        public int IdProveedor { get; set; }

        public Proveedor? Proveedor { get; set; }
        public Accesorio? Accesorio { get; set; }
        public Flor? Flor { get; set; }
    }

}
