using System;
using System.Collections.Generic;

namespace Pharmatime_Backend.Repositories.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            UsuarioEnfermedads = new HashSet<UsuarioEnfermedad>();
            UsuarioMedicamentoIdTutorNavigations = new HashSet<UsuarioMedicamento>();
            UsuarioMedicamentoIdUsuarioNavigations = new HashSet<UsuarioMedicamento>();
        }

        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public int? Genero { get; set; }
        public string? Telefono { get; set; }
        public int? Edad { get; set; }
        public string? Correo { get; set; }
        public string? Contrasena { get; set; }
        public int? TipoUsuario { get; set; }
        public int? Estado { get; set; }

        public virtual Estado? EstadoNavigation { get; set; }
        public virtual GeneroUsuario? GeneroNavigation { get; set; }
        public virtual Rol? TipoUsuarioNavigation { get; set; }
        public virtual ICollection<UsuarioEnfermedad> UsuarioEnfermedads { get; set; }
        public virtual ICollection<UsuarioMedicamento> UsuarioMedicamentoIdTutorNavigations { get; set; }
        public virtual ICollection<UsuarioMedicamento> UsuarioMedicamentoIdUsuarioNavigations { get; set; }
    }
}
