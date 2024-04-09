
using Pharmatime_Backend.Repositories.Models;
using Pharmatime_Backend.Utilities;
using System.Net.Mail;
using System.Net;


namespace Pharmatime_Backend.Repositories
{
    public class PatientRepository
    {

        public static bool RegisterPatient(CreatePatientDto model)
        {
            Encript e = new Encript();
            Random random = new Random();

            string contraseña = "";

            for (int i = 0; i < 6; i++)
            {
                contraseña += random.Next(0, 10);
            }



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
                            Contrasena = e.EncryptPassword(contraseña),
                            TipoUsuario = 2,
                            Estado = 3
                        };

                        context.Usuarios.Add(user);
                        context.SaveChanges();
                        

                        string asunto = "Creación de cuenta en Pharmatime";
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
                                <img src=""https://i.ibb.co/cQ4qwyL/bienvenida.jpg"" alt=""Descripción de la imagen"">                                
                                <h5>Bienvenido a Pharmatime</h5>
                                <p>La nueva contraseña para su cuenta de pharmatime es:</p>
                                
                                <p><span style='color: #008CBA;'>[contraseña]</span></p>
                                
                                <p>Si necesita más información o tiene alguna pregunta, no dude en ponerse en contacto con nosotros.</p>
                            </body>
                            </html>";

                        mensaje = mensaje.Replace("[contraseña]", contraseña);
                        MailMessage mail = new MailMessage();
                        mail.To.Add(user.Correo);
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




        public List<object> ReadPatient()
        {
            using (var context = new PHARMATIME_DBContext())
            {
                try
                {
                    var usuarios = context.Usuarios
                        .Where(u => u.TipoUsuario == 2)
                        .Select(u => new
                        {
                            IdUsuario = u.IdUsuario,
                            Nombre = u.Nombre,
                            Apellido = u.Apellido,
                            Genero = u.Genero,
                            Telefono = u.Telefono,
                            Edad = u.Edad,
                            Correo = u.Correo
                        })
                        .ToList<object>();

                    return usuarios;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al obtener los usuarios: {ex.Message}");
                    return null;
                }
            }
        }


        public static bool UpdatePatient(UpdatePatientDto model)
        {
            using (var context = new PHARMATIME_DBContext())
            {

                try
                {
                    // Buscar el usuario por su ID en la base de datos
                    var usuario = context.Usuarios.Where(u => u.TipoUsuario == 2).FirstOrDefault(u => u.IdUsuario == model.IdUsuario);

                    if (usuario != null)
                    {
                        // Actualizar las propiedades del usuario con los nuevos valores
                        usuario.Nombre = model.Nombre;
                        usuario.Apellido = model.Apellido;
                        usuario.Genero = model.Genero;
                        usuario.Telefono = model.Telefono;
                        usuario.Edad = model.Edad;
                        usuario.Correo = model.Correo;

                        // Guardar los cambios en la base de datos
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


        public static bool DeletePatient(DeletePatientDto model)
        {
            using (var context = new PHARMATIME_DBContext())
            {
                try
                {
                    // Buscar el usuario por su ID en la base de datos
                    var usuario = context.Usuarios.Where(u => u.TipoUsuario == 2).FirstOrDefault(u => u.IdUsuario == model.IdUsuario);

                    if (usuario != null)
                    {
                        usuario.Estado = 2;
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

                    Console.WriteLine($"Error al eliminar el usuario: {ex.Message}");
                    return false;
                }
            }
        }



    }
}


