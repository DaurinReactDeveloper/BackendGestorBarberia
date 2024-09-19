﻿using System;
using System.Collections.Generic;

namespace GestorBarberia.Domain.Entities
{
    public partial class Clientes
    {
        public Clientes()
        {
            Cita = new HashSet<Citas>();
            Comentarios = new HashSet<Comentarios>();
        }

        public int ClienteId { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Imgcliente { get; set; }

        public virtual ICollection<Citas> Cita { get; set; }
        public virtual ICollection<Comentarios> Comentarios { get; set; }
    }
}
