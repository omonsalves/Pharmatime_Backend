using System;
using System.Collections.Generic;

namespace Pharmatime_Backend.Models
{
    public partial class Rol
    {
        public Rol()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdRol { get; set; }
        public string? NombreRol { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
