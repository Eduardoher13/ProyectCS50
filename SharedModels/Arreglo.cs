using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public class Arreglo
    {
        [Key]
        public int IdArreglo { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreArreglo { get; set; }

        [StringLength(200)]
        public string? UrlImagen { get; set; }

        [StringLength(200)]
        public string? Descripcion { get; set; }

        public ICollection<ArregloFlor>? Flores { get; set; }
        public ICollection<ArregloAccesorio>? Accesorios { get; set; }
    }

}
