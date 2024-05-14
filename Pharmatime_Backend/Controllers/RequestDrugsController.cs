using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmatime_Backend.Repositories;

namespace Pharmatime_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestDrugsController : ControllerBase
    {

        [HttpPost("RequestNewDrugs")]
        public IActionResult RequestNewDrugs([FromBody] MailNewDrugsDto request)
        {
            var us = new DrugsService();
            var result = us.MailRequestNewdrugs(request);
            return StatusCode(result.Code, result);

        }


        [HttpPost("ReadRequestDrugs")]
        public IActionResult ReadPatient()
        {
            try
            {
                var re = new DrugsRepository();
                var ser = new DrugsService();
                
                var request = re.ReadRequestDrug();

                var list = ser.ReadRequestDrug(request);

                if (list != null)
                {
                    
                    return Ok(new { StatusCode = 200, Solicitudes = list });
                }
                else
                {
                    
                    return StatusCode(500, "No se pudo obtener la lista de usuarios");
                }

            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Error al procesar la solicitud: {ex.Message}");
            }
        }



        [HttpPost("RequestAnswered")]
        public IActionResult RequestAnswered([FromBody] RequestAnsweredDto request)
        {
            var us = new DrugsService();
            var result = us.RequestAnswered(request);
            return StatusCode(result.Code, result);

        }
        
        [HttpPost("DeleteRequest")]
        public IActionResult DeleteRequest([FromBody] RequestAnsweredDto request)
        {
            var us = new DrugsService();
            var result = us.DeleteS(request);
            return StatusCode(result.Code, result);

        }

    }
}
