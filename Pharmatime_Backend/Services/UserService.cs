using Microsoft.AspNetCore.Mvc;
using Pharmatime_Backend.Repositories.Models;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

public class UserService
{
	public ResultDto RegisterUser(RegisterDto user)
	{
        var respuestaJson = new ResultDto()
        {
            Mensaje = "El correo del usuario ya se encuentra registrado",
            Code = 400
        };

        if (UserRepository.Register(user))
        {
            respuestaJson = new ResultDto()
            {
                Mensaje = "Usuario registrado correctamente",
                Code = 201
            };
            
        }

        return respuestaJson;
    }

    public ResultDto LogingUser(LoginDto model)
    {
        var respuestaJson = new ResultDto()
        {
            Mensaje = "Credenciales incorrectas ",
            Code = 401
        };

        if (UserRepository.Login(model.Correo, model.Contrasena))
        {
            respuestaJson = new ResultDto()
            {
                Mensaje = "Inicio de sesion correcto",
                Code = 200
            };
        }

        return respuestaJson;
    }
    public ResultDto ServiceMail(MailDto destinatario)
    {
        
        var respuestaJson = new ResultDto()
        {
            Mensaje = "Error al enviar el correo",
            Code = 401
        };

        if (UserRepository.Mail(destinatario))
        {
            respuestaJson = new ResultDto()
            {
                Mensaje = "Correo enviado exitosamente",
                Code = 200
            };
        }

        return respuestaJson;
    }



}
