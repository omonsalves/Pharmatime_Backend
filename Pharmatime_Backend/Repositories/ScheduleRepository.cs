using Pharmatime_Backend.Repositories.Models;

namespace Pharmatime_Backend.Repositories
{
    public class ScheduleRepository
    {

        public static List<object> DataSchedule(ScheduleDto model)
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
