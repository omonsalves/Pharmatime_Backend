using System;
using System.Collections.Generic;

namespace Pharmatime_Backend.Repositories.Models
{
    public partial class UsuarioEnfermedad
    {
        public int IdUsuarioEnfermedad { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdEnfermedad { get; set; }

        public virtual Enfermedad? IdEnfermedadNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
