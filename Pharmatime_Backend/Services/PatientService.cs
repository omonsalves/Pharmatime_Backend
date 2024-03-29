
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
                    Mensaje = "Paciente registrado correctamente",
                    Code = 201
                };

            }

            return respuestaJson;
        }
    }

