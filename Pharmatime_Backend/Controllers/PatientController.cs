using Microsoft.AspNetCore.Mvc;
using Pharmatime_Backend.Repositories.Models;
using Pharmatime_Backend.Repositories;

namespace Pharmatime_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {

        private readonly PHARMATIME_DBContext _context;

        public PatientController(PHARMATIME_DBContext context)
        {
            _context = context;
        }


        // POST api/<UserController>
        [HttpPost("CreatePatient")]
        public IActionResult Register([FromBody] CreatePatientDto patient)
        {
            var us = new PatientService();
            var result = us.RegisterPatient(patient);
            return StatusCode(result.Code, result);

        }


        [HttpPost("ReadPatient")]
        public IActionResult ReadPatient()
        {
            try
            {
                 var us = new PatientRepository();
                 var pa = new PatientService();
                // Aquí se invoca el método para obtener la lista de usuarios
                var patient = us.ReadPatient();

                 var usuarios = pa.ListPatient(patient);

                if (usuarios != null)
                {
                    // Devuelve un código de estado HTTP 200 (OK) junto con la lista de usuarios
                    return Ok(new { StatusCode = 200, Usuarios = usuarios });
                }
                else
                {
                    // Si no se pudo obtener la lista de usuarios, devuelve un código de estado HTTP 500 (Error interno del servidor)
                    return StatusCode(500, "No se pudo obtener la lista de usuarios");
                }
                
            }
            catch (Exception ex)
            {
                // Si se produce una excepción, devuelve un código de estado HTTP 500 (Error interno del servidor) junto con un mensaje de error
                return StatusCode(500, $"Error al procesar la solicitud: {ex.Message}");
            }
        }


        [HttpPost("UpdatePatient")]
        public IActionResult Update([FromBody] UpdatePatientDto patient)
        {
            var us = new PatientService();
            var result = us.PatientUpdate(patient);
            return StatusCode(result.Code, result);

        }
        
        [HttpPost("DeletePatient")]
        public IActionResult Delete([FromBody] DeletePatientDto patient)
        {
            var us = new PatientService();
            var result = us.PatientDelete(patient);
            return StatusCode(result.Code, result);

        }
    }
}
