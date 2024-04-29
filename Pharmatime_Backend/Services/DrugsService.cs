
using Pharmatime_Backend.Repositories;



  public class DrugsService
  {

    public ResultDto DrugsRegister(RegisterDrugsDto drug)
    {
        var respuestaJson = new ResultDto()
        {
            Mensaje = "Error al registrar el medicamento",
            Code = 400
        };

        if (DrugsRepository.RegisterDrugs(drug))
        {
            respuestaJson = new ResultDto()
            {
                Mensaje = "Medicamento registrado corretamente en la base de datos",
                Code = 200
            };

        }

        return respuestaJson;
    }

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
            Mensaje = "Error al asignar el medicamento",
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


    public ResultDto DeleteDrug(DeleteDrugDto model)
    {
        var respuestaJson = new ResultDto()
        {
            Mensaje = "Error al inhabilitar el medicamento",
            Code = 400
        };

        if (DrugsRepository.DeleteDrug(model))
        {
            respuestaJson = new ResultDto()
            {
                Mensaje = "Medicamento inhabilitado correctamente",
                Code = 200
            };

        }

        return respuestaJson;
    }
    
    public ResultDto RecoverDrug(DeleteDrugDto model)
    {
        var respuestaJson = new ResultDto()
        {
            Mensaje = "Error al habilitar el medicamento",
            Code = 400
        };

        if (DrugsRepository.RecoverDrug(model))
        {
            respuestaJson = new ResultDto()
            {
                Mensaje = "Medicamento habilitado correctamente",
                Code = 200
            };

        }

        return respuestaJson;
    }


    public List<object> ReadRequestDrug(List<object> request)
    {
        if (request != null && request.Any())
        {
            return request;
        }
        else
        {
            throw new InvalidOperationException("La lista de solicitudes está vacía o nula.");
        }
    }


    public ResultDto RequestAnswered(RequestAnsweredDto model)
    {
        var respuestaJson = new ResultDto()
        {
            Mensaje = "Error al atender la solicitud",
            Code = 400
        };

        if (DrugsRepository.RequestAnswered(model))
        {
            respuestaJson = new ResultDto()
            {
                Mensaje = "Solicitud atendida correctamente",
                Code = 200
            };

        }

        return respuestaJson;
    }

}

