using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Pharmatime_Backend.Repositories.Models;

public class DiseasesRepository
{

    public static bool AssingnDisease(AssingnDiseasesDto model)
    {

        using (var context = new PHARMATIME_DBContext())
        {
            try
            {
                var disease = context.Enfermedads.SingleOrDefault(u => u.IdEnfermedad == model.id_enfermedad);
                var val = context.UsuarioEnfermedads.SingleOrDefault(u => u.IdUsuario == model.id_usuario && u.IdEnfermedad == model.id_enfermedad);
                if (val == null)
                {
                    if (disease != null)
                    {
                        var userDisease = new UsuarioEnfermedad()
                        {
                            IdUsuario = model.id_usuario,
                            IdEnfermedad = model.id_enfermedad
                        };

                        context.UsuarioEnfermedads.Add(userDisease);
                        context.SaveChanges();
                        return true;
                    }
                }
                return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enlazar la enfermedad al paciente: {ex.Message}");
                return false;
            }

        }
    }


    public List<object>? ReadDiseases()
    {
        using (var context = new PHARMATIME_DBContext())
        {
            try
            {
                    var diseases = context.Enfermedads
                    .Select(u => new
                    {
                        IdEnfermedad = u.IdEnfermedad,
                        Nombre = u.Nombre,
                        Descripcion = u.Descripcion
                    })
                    .ToList<object>();
                    return diseases;
                
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los medicamentos: {ex.Message}");
                return null;
            }
        }
    }






}



