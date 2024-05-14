using Microsoft.EntityFrameworkCore;
using Pharmatime_Backend.Repositories.Models;

namespace Pharmatime_Backend.Repositories
{
    public class ScheduleRepository
    {

        public static List<object> DataScheduleDoctor(ScheduleDto model)
        {
            using (var context = new PHARMATIME_DBContext())
            {
                try
                {
                    var tutor = context.Usuarios.SingleOrDefault(u => u.IdUsuario == model.IdUsuario);
                    var data = context.UsuarioMedicamentos
                        .Where(u => u.IdTutor == tutor.IdUsuario)
                        .Include(i => i.IdMedicamentoNavigation)
                        .Include(i => i.IdUsuarioNavigation)
                        .Select(s => new
                        {
                            Usuario = s.IdUsuarioNavigation.Nombre + " " + s.IdUsuarioNavigation.Apellido,
                             Medicamento = s.IdMedicamentoNavigation.Nombre, 
                             IdTutor = s.IdTutor,
                             Durante = s.Durante,
                             Dosis = s.Dosis,
                             Intervalo = s.Intervalo
                        }).ToList<object>();
                    return data;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al obtener los usuarios: {ex.Message}");
                    return null;
                }
            }
        }
        
        
        public static List<object> DataSchedulePatient(ScheduleDto model)
        {
            using (var context = new PHARMATIME_DBContext())
            {
                try
                {
                    var usuario = context.Usuarios.SingleOrDefault(u => u.IdUsuario == model.IdUsuario);

                     var data = context.UsuarioMedicamentos
                        .Where(u => u.IdUsuario == usuario.IdUsuario)
                        .Join(context.Medicamentos, 
                              um => um.IdMedicamento, 
                              m => m.IdMedicamento,   
                              (um, m) => new          
                              {
                                  Usuario = usuario.Nombre + " " + usuario.Apellido,
                                  Medicamento = m.Nombre, 
                                  IdTutor = um.IdTutor,
                                  Durante = um.Durante,
                                  Dosis = um.Dosis,
                                  Intervalo = um.Intervalo
                              })
                        .ToList<object>();
                    return data;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al obtener los usuarios: {ex.Message}");
                    return null;
                }
            }
        }


    }
}
