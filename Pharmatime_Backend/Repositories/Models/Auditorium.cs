using System;
using System.Collections.Generic;

namespace Pharmatime_Backend.Repositories.Models
{
    public partial class Auditorium
    {
        public int IdAuditoria { get; set; }
        public string? Usuario { get; set; }
        public string? Accion { get; set; }
        public string? Tabla { get; set; }
        public string? ValorAnterior { get; set; }
        public string? ValorNuevo { get; set; }
        public string? VSql { get; set; }
        public string? Fecha { get; set; }
    }
}
