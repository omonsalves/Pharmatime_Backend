using Microsoft.AspNetCore.Mvc;
using Pharmatime_Backend.Repositories.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pharmatime_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

         private  readonly PHARMATIME_DBContext _context;
        
         public UserController(PHARMATIME_DBContext context)
         {
             _context = context; 
         }

        // POST api/<UserController>
        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterDto usuario)
        {
            var us = new UserService();
            var result = us.RegisterUser(usuario);
            return StatusCode(result.Code, result);  
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDto usuario)
        {
            var us = new UserService();
            var result = us.LogingUser(usuario);
            return StatusCode(result.Code, result);
        }
        
        [HttpPost("RecoverPasword")]
        public IActionResult RecoverPasword([FromBody] MailDto Destinatario)
        {
            var us = new UserService();
            var result = us.ServiceMail(Destinatario);
            return StatusCode(result.Code, result);
        }

        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword([FromBody] ChagePasswordDto model)
        {
            var us = new UserService();
            var result = us.ChagePassword(model);
            return StatusCode(result.Code, result);
        }
        
        [HttpPost("RecoverAccount")]
        public IActionResult RecoverAccount([FromBody] DeletePatientDto model)
        {
            var us = new UserService();
            var result = us.RecoverAccount(model);
            return StatusCode(result.Code, result);
        }

    }
}
