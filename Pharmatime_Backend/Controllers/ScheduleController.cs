using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmatime_Backend.Repositories;
using Wkhtmltopdf.NetCore;

namespace Pharmatime_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {

        [HttpPost("DataScheduleTutor")]
        public IActionResult DataScheduleDoctor(ScheduleDto model)
        {
            try
            {
                
                var sh = new ScheduleService();
            

                var Schedule = sh.ScheduleDataDoctor(model);

                if (Schedule != null)
                {
                   
                    return Ok(new { StatusCode = 200, Usuarios = Schedule });
                }
                else
                {
                    
                    return StatusCode(500, "No se pudo obtener los datos para el horario");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al procesar la solicitud: {ex.Message}");
            }
        }
        
        
        [HttpPost("DataSchedulePatient")]
        public IActionResult DataSchedulePatient(ScheduleDto model)
        {
            try
            {
                
                var sh = new ScheduleService();
            

                var Schedule = sh.ScheduleDataPatient(model);

                if (Schedule != null)
                {
                   
                    return Ok(new { StatusCode = 200, Usuarios = Schedule });
                }
                else
                {
                    
                    return StatusCode(500, "No se pudo obtener los datos para el horario");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al procesar la solicitud: {ex.Message}");
            }
        }


    }
}
