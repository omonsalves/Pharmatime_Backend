using Microsoft.AspNetCore.Mvc;
using Pharmatime_Backend.Models;

namespace Pharmatime_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {

        private readonly PHARMATIMEContext _context;

        public PatientController(PHARMATIMEContext context)
        {
            _context = context;
        }


        // POST api/<UserController>
        [HttpPost("CreatePatient")]
        public IActionResult Register([FromBody] CreatePatientDto paciente)
        {
            var us = new PatientService();
            var result = us.RegisterPatient(paciente);
            return StatusCode(result.Code, result);

        }

    }
}
