using Microsoft.EntityFrameworkCore;
using Pharmatime_Backend.Models;
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

        using (var context = new PHARMATIMEContext())
        {
            try
            {
                var usuario = context.Usuarios.SingleOrDefault(u => u.Correo == model.Correo);

                if (usuario == null)
                {
                    var user = new Usuario()
                    {

                        Nombre = model.Nombre,
                        Apellido = model.Apellido,
                        Genero = model.Genero,
                        Telefono = model.Telefono,
                        Edad = model.Edad,
                        Correo = model.Correo,
                        Contraseña = e.EncryptPassword(model.Contraseña),
                        TipoUsuario = 1
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

        using (var context = new PHARMATIMEContext())
        {
            try
            {
                var usuario = context.Usuarios.SingleOrDefault(u => u.Correo == correo && u.Contraseña == e.EncryptPassword(contraseña));

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
        bool resultado = false;
        try
        {
            Random random = new Random();
            string contraseña = "";

            for (int i = 0; i < 6; i++)
            {
                contraseña += random.Next(0, 10);
            }


            using (var context = new PHARMATIMEContext())
            {

                var usuario = context.Usuarios.FirstOrDefault(u => u.Correo == model.Destinatario);

                if (usuario != null)
                {
                    string Cencrypt = e.EncryptPassword(contraseña);
                    usuario.Contraseña = Cencrypt;

                    context.SaveChanges();

                


                     string asunto = "Recuperación de contraseña Pharmatime";
                     string mensaje = "La nueva contraseña para su cuenta de pharmatime es:" + contraseña;

                     MailMessage mail = new MailMessage();
                     mail.To.Add(model.Destinatario);
                     mail.From = new MailAddress("pharmatime8@gmail.com");
                     mail.Subject = asunto;
                     mail.Body = mensaje;
                     mail.IsBodyHtml = true;

                     var smtp = new SmtpClient()
                     {
                         Credentials = new NetworkCredential("pharmatime8@gmail.com", "huejdvhbfjbkvgjc"),
                         Host = "smtp.gmail.com",
                         Port = 587,
                         EnableSsl = true
                     };

                     smtp.Send(mail);
                     resultado = true;

                }
            }
        }
        catch (Exception ex)
        { 
            resultado = false;
        }

        return resultado;
    }
}
