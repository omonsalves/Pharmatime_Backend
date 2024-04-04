
using Pharmatime_Backend.Repositories.Models;
using Pharmatime_Backend.Repositories;

public class PatientService
    {
        public ResultDto RegisterPatient(CreatePatientDto patient)
        {
            var respuestaJson = new ResultDto()
            {
                Mensaje = "El correo del usuario ya se encuentra registrado",
                Code = 400
            };

            if (PatientRepository.RegisterPatient(patient))
            {
                respuestaJson = new ResultDto()
                {
                    Mensaje = "Usuario registrado correctamente",
                    Code = 201
                };

            }

            return respuestaJson;
        }

      public List<object> ListPatient(List<object> usuarios)
      {
          if (usuarios != null && usuarios.Any())
          {
              return usuarios;
          }
          else
          {
              throw new InvalidOperationException("La lista de usuarios está vacía o nula.");
          }
      }

    public ResultDto PatientUpdate(UpdatePatientDto patient)
    {
        var respuestaJson = new ResultDto()
        {
            Mensaje = "Error al actualizar datos",
            Code = 403
        };

        if (PatientRepository.UpdatePatient(patient))
        {
            respuestaJson = new ResultDto()
            {
                Mensaje = "Cambios registrados correctamente",
                Code = 200
            };
        
        }

        return respuestaJson;
    }


    public ResultDto PatientDelete(DeletePatientDto patient)
    {
        var respuestaJson = new ResultDto()
        {
            Mensaje = "Error al eliminar el paciente",
            Code = 403
        };

        if (PatientRepository.DeletePatient(patient))
        {
            respuestaJson = new ResultDto()
            {
                Mensaje = "Paciente Eliminado correctamente",
                Code = 200
            };
        
        }

        return respuestaJson;
    }
}

