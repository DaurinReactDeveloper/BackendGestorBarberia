using System;
using System.Collections.Generic;

namespace GestorBarberia.Domain.Entities
{
    public partial class Administradores
    {
        public int AdministradoresId { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
