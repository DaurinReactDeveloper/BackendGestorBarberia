using System;
using System.Collections.Generic;

namespace GestorBarberia.Domain.Entities
{
    public partial class Cita
    {
        public int CitaId { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public int BarberoId { get; set; }
        public int ClienteId { get; set; }
        public int EstiloId { get; set; }
        public string Estado { get; set; } = null!;

        public virtual Barbero? Barbero { get; set; }
        public virtual Cliente? Cliente { get; set; }
        public virtual Estilosdecorte? Estilo { get; set; }
    }
}
