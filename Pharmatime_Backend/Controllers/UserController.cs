﻿using Microsoft.AspNetCore.Mvc;
using Pharmatime_Backend.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pharmatime_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private  readonly PHARMATIMEContext _context;

        public UserController(PHARMATIMEContext context)
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

    }
}