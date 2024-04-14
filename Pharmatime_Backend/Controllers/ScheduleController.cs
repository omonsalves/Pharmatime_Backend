﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmatime_Backend.Repositories;

namespace Pharmatime_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {

        [HttpPost("DataSchedule")]
        public IActionResult DataSchedule(ScheduleDto model)
        {
            try
            {
                
                var sh = new ScheduleService();
            

                var Schedule = sh.ScheduleData(model);

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