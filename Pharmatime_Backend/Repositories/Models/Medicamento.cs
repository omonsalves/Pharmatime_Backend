using System;
using System.Collections.Generic;

namespace Pharmatime_Backend.Repositories.Models
{
    public partial class Medicamento
    {
        public Medicamento()
        {
            UsuarioMedicamentos = new HashSet<UsuarioMedicamento>();
        }

        public int IdMedicamento { get; set; }
        public string? Nombre { get; set; }
        public string? SirvePara { get; set; }
        public int? Presentacion { get; set; }
        public string? Contraindicaciones { get; set; }
        public int? Activo { get; set; }

        public virtual TipoPresentacion? PresentacionNavigation { get; set; }
        public virtual ICollection<UsuarioMedicamento> UsuarioMedicamentos { get; set; }
    }
}
