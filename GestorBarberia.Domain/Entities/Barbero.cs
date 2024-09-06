﻿using System;
using System.Collections.Generic;

namespace GestorBarberia.Domain.Entities
{
    public partial class Barbero
    {
        public Barbero()
        {
            Cita = new HashSet<Cita>();
        }

        public int BarberoId { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public string Imgbarbero { get; set; }

        public virtual ICollection<Cita> Cita { get; set; }
    }
}
