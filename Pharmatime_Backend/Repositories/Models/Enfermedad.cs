using System;
using System.Collections.Generic;

namespace Pharmatime_Backend.Repositories.Models
{
    public partial class Enfermedad
    {
        public Enfermedad()
        {
            UsuarioEnfermedads = new HashSet<UsuarioEnfermedad>();
        }

        public int IdEnfermedad { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<UsuarioEnfermedad> UsuarioEnfermedads { get; set; }
    }
}
