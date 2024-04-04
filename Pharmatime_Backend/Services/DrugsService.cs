
using Pharmatime_Backend.Repositories;



  public class DrugsService
  {

     public List<object> ListDrugs(List<object> drugs)
     {
         if (drugs != null && drugs.Any())
         {
             return drugs;
         }
         else
         {
             throw new InvalidOperationException("La lista de Medicamentos está vacía o nula.");
         }
     }


    public ResultDto AssingnDrugsRegister(AssignDrugsDto drugsPatient)
    {
        var respuestaJson = new ResultDto()
        {
            Mensaje = "El usuario no existe en la base de datos",
            Code = 400
        };

        if (DrugsRepository.RegisterAssingnDrugs(drugsPatient))
        {
            respuestaJson = new ResultDto()
            {
                Mensaje = "Medicamento asignado correctamente al paciente",
                Code = 200
            };

        }

        return respuestaJson;
    }

    public ResultDto MailRequestNewdrugs(MailNewDrugsDto model)
    {
        var respuestaJson = new ResultDto()
        {
            Mensaje = "Error al realizar la solicitud",
            Code = 400
        };

        if (DrugsRepository.RequestNewdrugsMail(model))
        {
            respuestaJson = new ResultDto()
            {
                Mensaje = "Solicitud realizada exitosamente",
                Code = 200
            };

        }

        return respuestaJson;
    }

}

