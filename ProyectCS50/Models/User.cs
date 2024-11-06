using System;
using System.Collections.Generic;

namespace ProyectCS50.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string UserName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;
}
