using Pharmatime_Backend.Repositories.Models;
using System.Net.Mail;
using System.Net;

namespace Pharmatime_Backend.Utilities
{
    public class Mail
    {
        
        public bool SendMail(string correo, string asunto, string mensaje)
        {
            Encript e = new Encript();
            bool resultado = false;
            try
            {
                using (var context = new PHARMATIME_DBContext())
                {

                    var user = context.Usuarios.FirstOrDefault(u => u.Correo == correo);

                    if (user != null)
                    {
                        context.SaveChanges();
                        MailMessage mail = new MailMessage();
                        mail.To.Add(correo);
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
                Console.WriteLine($"Error al recuperar contraseña: {ex.Message}");
            }

            return resultado;
        }

    }
}
