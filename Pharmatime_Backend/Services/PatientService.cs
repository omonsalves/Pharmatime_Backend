

using Pharmatime_Backend.Repositories;

public class PatientService
    {
        public ResultDto RegisterPatient(CreatePatientDto patient)
        {
            var respuestaJson = new ResultDto()
            {
                Mensaje = "Error al registrar paciente",
                Code = 400
            };

            if (PatientRepository.RegisterPatient(patient))
            {
                respuestaJson = new ResultDto()
                {
                    Mensaje = "Paciente registrado correctamente",
                    Code = 200
                };

            }

            return respuestaJson;
        }

      public List<object> ListPatient(List<object> usuarios)
      {
        var list = new List<object>();
          if (usuarios != null && usuarios.Any())
          {
              list.AddRange(usuarios);
          }
          else
          {
              list = new List<object>();
          }
        return list;
    }

    public ResultDto PatientUpdate(UpdatePatientDto patient)
    {
        var respuestaJson = new ResultDto()
        {
            Mensaje = "Error al actualizar datos",
            Code = 400
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
            Mensaje = "Error al Inhabilitar el paciente",
            Code = 400
        };

        if (PatientRepository.DeletePatient(patient))
        {
            respuestaJson = new ResultDto()
            {
                Mensaje = "Paciente Inhabilitado correctamente",
                Code = 200
            };
        
        }

        return respuestaJson;
    }
}

