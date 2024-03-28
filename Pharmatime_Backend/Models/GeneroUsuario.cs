using System;
using System.Collections.Generic;

namespace Pharmatime_Backend.Models
{
    public partial class GeneroUsuario
    {
        public GeneroUsuario()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdGenero { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
