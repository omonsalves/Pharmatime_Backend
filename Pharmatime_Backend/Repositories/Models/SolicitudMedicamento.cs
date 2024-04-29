using System;
using System.Collections.Generic;

namespace Pharmatime_Backend.Repositories.Models
{
    public partial class SolicitudMedicamento
    {
        public int IdSolicitudMedicamento { get; set; }
        public int? IdUsuario { get; set; }
        public string? NombreMedicamento { get; set; }
        public string? UsoDado { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
