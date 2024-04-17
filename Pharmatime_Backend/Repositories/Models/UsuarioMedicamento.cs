using System;
using System.Collections.Generic;

namespace Pharmatime_Backend.Repositories.Models
{
    public partial class UsuarioMedicamento
    {
        public int IdUsuarioMedicamento { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdMedicamento { get; set; }
        public int? IdTutor { get; set; }
        public string? Durante { get; set; }
        public string? Dosis { get; set; }
        public int? Intervalo { get; set; }

        public virtual Medicamento? IdMedicamentoNavigation { get; set; }
        public virtual Usuario? IdTutorNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
