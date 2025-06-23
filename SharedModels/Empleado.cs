using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public class Empleado
    {
        [Key]
        public int IdEmpleado { get; set; }

        [Required]
        [StringLength(50)]
        public string PrimerNombre { get; set; }

        [StringLength(50)]
        public string? SegundoNombre { get; set; }

        [Required]
        [StringLength(50)]
        public string PrimerApellido { get; set; }

        [StringLength(50)]
        public string? SegundoApellido { get; set; }

        public string? Sexo { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Contrasena { get; set; }

        [StringLength(20)]
        public string? Telefono { get; set; }

        public DateTime? FechaDeNac { get; set; }

        public bool EsAprobado { get; set; }

        public int RolId { get; set; }

        public Rol? Rol { get; set; }
    }
}
