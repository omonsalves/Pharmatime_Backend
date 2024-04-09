using System;
using System.Collections.Generic;

namespace Pharmatime_Backend.Repositories.Models
{
    public partial class Estado
    {
        public Estado()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdEstado { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
