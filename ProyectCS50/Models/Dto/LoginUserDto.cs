using System.ComponentModel.DataAnnotations;

namespace ProyectCS50.Models.Dto
{
    public class LoginUserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
