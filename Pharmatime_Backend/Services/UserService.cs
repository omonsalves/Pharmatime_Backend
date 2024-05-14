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
            Mensaje = "Error al registrar el usuario",
            Code = 400
        };

        if (UserRepository.Register(user))
        {
            respuestaJson = new ResultDto()
            {
                Mensaje = "Usuario registrado correctamente",
                Code = 200
            };
            
        }

        return respuestaJson;
    }

    public ResultLoginDto LogingUser(LoginDto model)
    {
        var respuestaJson = new ResultLoginDto()
        {
            Mensaje = "Credenciales incorrectas",
            Code = 400
        };
      
            

        if (UserRepository.Login(model.Correo, model.Contrasena))
        {
            
            using (var context = new PHARMATIME_DBContext())
            {
                var user = context.Usuarios.First(u => u.Correo == model.Correo);
                var u = context.Usuarios.SingleOrDefault(u => u.IdUsuario == user.IdUsuario);

                string rol = "";

                if(u.TipoUsuario == 1)
                {
                    rol = "tutor";
                } else if (u.TipoUsuario == 2)
                {
                    rol = "paciente";
                }else if (u.TipoUsuario == 3) {
                    rol = "medico";
                }
                respuestaJson = new ResultLoginDto()
                {
                    Mensaje = "Inicio de sesion correcto",
                    Code = 200,
                    IdUsuario = u.IdUsuario,
                    Nombre = u.Nombre,
                    Apellido = u.Apellido,
                    Rol = rol
                };

            }
        }

        return respuestaJson ;
    }
    public ResultDto ServiceMail(MailDto destinatario)
    {
        
        var respuestaJson = new ResultDto()
        {
            Mensaje = "Error al enviar el correo",
            Code = 400
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
    
    public ResultDto ChagePassword(ChagePasswordDto model)
    {
        
        var respuestaJson = new ResultDto()
        {
            Mensaje = "Error al cambiar la contraseña",
            Code = 400
        };

        if (UserRepository.ChangePassword(model))
        {
            respuestaJson = new ResultDto()
            {
                Mensaje = "Contraseña actualizada correctamente",
                Code = 200
            };
        }

        return respuestaJson;
    }
    
    public ResultDto RecoverAccount(DeletePatientDto model)
    {
        
        var respuestaJson = new ResultDto()
        {
            Mensaje = "Error al recuperar cuenta",
            Code = 400
        };

        if (UserRepository.RecoverAccount(model))
        {
            respuestaJson = new ResultDto()
            {
                Mensaje = "Cuenta habilitada correctamente",
                Code = 200
            };
        }

        return respuestaJson;
    }





}
