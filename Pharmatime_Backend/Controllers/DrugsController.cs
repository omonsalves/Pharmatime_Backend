using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmatime_Backend.Repositories;

namespace Pharmatime_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugsController : ControllerBase
    {

        [HttpPost("RegisterDrugs")]
        public IActionResult RegisterDrugs([FromBody] RegisterDrugsDto drug)
        {
            var us = new DrugsService();
            var result = us.DrugsRegister(drug);
            return StatusCode(result.Code, result);

        }


        [HttpPost("ReadDrug")]
        public IActionResult ReadDrug()
        {
            try
            {
                var us = new DrugsRepository();
                var pa = new DrugsService();

                var drugs = us.ReadDrugs();

                var drugsList = pa.ListDrugs(drugs);

                if (drugsList != null)
                {

                    return Ok(new { StatusCode = 200, drugsList = drugsList });
                }
                else
                {

                    return StatusCode(500, "No se pudo obtener la lista de medicamentos");
                }

            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error al procesar la solicitud: {ex.Message}");
            }
        }

        [HttpPost("RequestNewDrugs")]
        public IActionResult RequestNewDrugs([FromBody] MailNewDrugsDto newDrugs)
        {
            var us = new DrugsService();
            var result = us.MailRequestNewdrugs(newDrugs);
            return StatusCode(result.Code, result);

        }
    }
}
