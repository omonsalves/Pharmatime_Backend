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
                        .Select(u => new
                        {
                            IdUsuario = u.IdUsuario,
                            IdMedicamento = u.IdMedicamento,
                            Durante = u.Durante,
                            Dosis = u.Dosis,
                            Intervalo = u.Intervalo,

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
