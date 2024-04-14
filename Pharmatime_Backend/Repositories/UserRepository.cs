using Microsoft.EntityFrameworkCore;
using Pharmatime_Backend.Repositories.Models;
using Pharmatime_Backend.Utilities;
using System;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore.ValueGeneration;

public class UserRepository
{

     public static bool Register(RegisterDto model)
     {
         Encript e = new Encript();
    
         using (var context = new PHARMATIME_DBContext())
         {
             try
             {
                 var usuario = context.Usuarios.SingleOrDefault(u => u.Correo == model.Correo);
    
                 if (usuario == null)
                 {
                     var user = new Usuario()
                     {
                         IdUsuario = model.IdUsuario,
                         Nombre = model.Nombre,
                         Apellido = model.Apellido,
                         Genero = model.Genero,
                         Telefono = model.Telefono,
                         Edad = model.Edad,
                         Correo = model.Correo,
                         Contrasena = e.EncryptPassword(model.Contrasena),
                         TipoUsuario = 1,
                         Estado = 1
                     };
    
                     context.Usuarios.Add(user);
                     context.SaveChanges();
                     return true;
                 }
    
                 return false;
    
             }
             catch (Exception ex)
             {
                 Console.WriteLine($"Error al guardar el usuario: {ex.Message}");
                 return false;
             }
    
         }
     }
    
    
     public static bool Login(string correo, string contraseña)
     {
         Encript e = new Encript();
    
         using (var context = new PHARMATIME_DBContext())
         {
             try
             {
                 var usuario = context.Usuarios.SingleOrDefault(u => u.Correo == correo && u.Contrasena == e.EncryptPassword(contraseña));
    
                 if (usuario != null)
                 {
                     
                     return true;
                 }
                 else
                 {
                     
                     return false;
                 }
             }
             catch (Exception ex)
             {
                 Console.WriteLine($"Error al consultar el usuario: {ex.Message}");
                 return false;
             }
         }
     }
    
    
    
    
     public static bool Mail(MailDto model)
     {
         Encript e = new Encript();
         Mail m = new Mail();
         bool resultado = false;
         try
         {
             Random random = new Random();
             string contraseña = "";
    
             for (int i = 0; i < 6; i++)
             {
                 contraseña += random.Next(0, 10);
             }
    
    
             using (var context = new PHARMATIME_DBContext())
             {
    
                 var user = context.Usuarios.FirstOrDefault(u => u.Correo == model.Destinatario);
    
                 if (user != null)
                 {
                     string Cencrypt = e.EncryptPassword(contraseña);
                     user.Contrasena = Cencrypt;
                     user.Estado = 3;
    
                     context.SaveChanges();
    
              
                      string asunto = "Recuperación de contraseña Pharmatime";
                      string mensaje = @"
                            <html>
                            <head>
                                <style>
                                    body {
                                        font-family: Arial, sans-serif;
                                    }
                                    h1 {
                                        color: #333333;
                                    }
                                    p {
                                        color: #666666;
                                    }
                                </style>
                            </head>
                            <body>
                                                                
                                <h5>Recuperación de contraseña de su cuenta pharmatime</h5>
                                <p>La nueva contraseña para su cuenta de pharmatime es:</p>
                                
                                <p><span style='color: #008CBA;'>[contraseña]</span></p>
                                
                                <p>Si necesita más información o tiene alguna pregunta, no dude en ponerse en contacto con nosotros.</p>
                            </body>
                            </html>";
                    mensaje = mensaje.Replace("[contraseña]", contraseña);
                    m.SendMail(model.Destinatario, asunto, mensaje);
                    resultado = true;
    
                 }
             }
         }
         catch (Exception ex)
         { 
             resultado = false;
             Console.WriteLine($"Error al recuperar contraseña: {ex.Message}");
         }
    
         return resultado;
     }

    public static bool ChangePassword(ChagePasswordDto model)
    {
        Encript e = new Encript();
        using (var context = new PHARMATIME_DBContext())
        {

            try
            {
                var user = context.Usuarios.FirstOrDefault(u => u.IdUsuario == model.IdUsuario);

                if (user != null)
                {
                    user.Contrasena = e.EncryptPassword(model.Contrasena);
                    user.Estado = 1;
                    context.SaveChanges();
                    
                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error al editar el usuario: {ex.Message}");
                return false;
            }
        }
    }
    
    
    public static bool RecoverAccount(DeletePatientDto model)
    {
        Encript e = new Encript();
        using (var context = new PHARMATIME_DBContext())
        {

            try
            {
                var user = context.Usuarios.FirstOrDefault(u => u.IdUsuario == model.IdUsuario);

                if (user != null)
                {
                    user.Estado = 1;
                    context.SaveChanges();
                    
                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error al editar el usuario: {ex.Message}");
                return false;
            }
        }
    }




}
