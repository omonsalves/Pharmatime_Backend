using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmatime_Backend.Repositories;
using Pharmatime_Backend.Services;

namespace Pharmatime_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseasesController : ControllerBase
    {

        [HttpPost("AssingnDisease")]
        public IActionResult AssingnDisease([FromBody] AssingnDiseasesDto disease)
        {
            var us = new DiseasesService();
            var result = us.AssingnDisease(disease);
            return StatusCode(result.Code, result);

        }

        [HttpPost("ReadDiseases")]
        public IActionResult ReadDiseases()
        {
            try
            {
                var us = new DiseasesRepository();
                var pa = new DiseasesService();

                var diseases = us.ReadDiseases();

                var diseasesList = pa.ListDiseases(diseases);

                if (diseasesList != null)
                {

                    return Ok(new { StatusCode = 200, diseasesList = diseasesList });
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



    }
}
