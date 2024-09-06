using System;
using System.Collections.Generic;

namespace GestorBarberia.Domain.Entities
{
    public partial class Estilosdecorte
    {
        public Estilosdecorte()
        {
            Cita = new HashSet<Cita>();
        }

        public int EstiloId { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public string Imgestilo { get; set; }

        public virtual ICollection<Cita> Cita { get; set; }
    }
}
