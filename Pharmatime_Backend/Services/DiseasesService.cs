using Pharmatime_Backend.Repositories;

namespace Pharmatime_Backend.Services
{
    public class DiseasesService
    {
        public ResultDto AssingnDisease(AssingnDiseasesDto drug)
        {
            var respuestaJson = new ResultDto()
            {
                Mensaje = "El usuario ya tiene tal enfermedad asignada",
                Code = 400
            };

            if (DiseasesRepository.AssingnDisease(drug))
            {
                respuestaJson = new ResultDto()
                {
                    Mensaje = "Enfermedad asignada correctamente a usuario",
                    Code = 200
                };

            }

            return respuestaJson;
        }



        public List<object> ListDiseases(List<object> diseases)
        {
            if (diseases != null && diseases.Any())
            {
                return diseases;
            }
            else
            {
                throw new InvalidOperationException("La lista de Medicamentos está vacía o nula.");
            }
        }


    }
}
