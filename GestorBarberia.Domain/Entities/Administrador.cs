using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBarberia.Domain.Entities
{
    public partial class Administrador
    {

        public int AdministradorID { get; set; }
        public string Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

    }
}
