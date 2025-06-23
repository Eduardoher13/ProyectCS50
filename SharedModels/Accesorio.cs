using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public class Accesorio
    {
        [Key]
        public int IdAccesorio { get; set; }

        [Required]
        [StringLength(30)]
        public string NombreAccesorio { get; set; }

        [Required]
        public int Stock { get; set; }
    }
}