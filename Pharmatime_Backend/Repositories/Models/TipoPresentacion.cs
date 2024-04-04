using System;
using System.Collections.Generic;

namespace Pharmatime_Backend.Repositories.Models
{
    public partial class TipoPresentacion
    {
        public TipoPresentacion()
        {
            Medicamentos = new HashSet<Medicamento>();
        }

        public int IdPresentacion { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<Medicamento> Medicamentos { get; set; }
    }
}
