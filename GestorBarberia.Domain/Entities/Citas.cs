using System;
using System.Collections.Generic;

namespace GestorBarberia.Domain.Entities
{
    public partial class Citas
    {

        public Citas()
        {

            Comentarios = new HashSet<Comentarios>();

        }

        public int CitaId { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public int BarberoId { get; set; }
        public int ClienteId { get; set; }
        public int EstiloId { get; set; }
        public string Estado { get; set; } = null!;

        public virtual Barberos? Barbero { get; set; }
        public virtual Clientes? Cliente { get; set; }
        public virtual Estilosdecortes? Estilo { get; set; }
        public virtual ICollection<Comentarios> Comentarios { get; set; }

    }
}
