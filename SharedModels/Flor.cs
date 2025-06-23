using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public class Flor
    {
        [Key]
        public int IdFlor { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreFlor { get; set; }

        [Required]
        public int Stock { get; set; }
    }
}
