using Pharmatime_Backend.Repositories.Models;
using Pharmatime_Backend.Utilities;
using System.Net.Mail;
using System.Net;

namespace Pharmatime_Backend.Repositories
{
    public class DrugsRepository
    {


        public List<object> ReadDrugs()
        {
            using (var context = new PHARMATIME_DBContext())
            {
                try
                {

                    var drugs = context.Medicamentos
                        .Select(u => new
                        {
                            Nombre = u.Nombre,
                            SirvePara = u.SirvePara,
                            Presentacion = u.Presentacion,
                            Contraindicaciones = u.Contraindicaciones                            
                        })
                        .ToList<object>();

                    return drugs;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al obtener los medicamentos: {ex.Message}");
                    return null;
                }
            }
        }


        public static bool RegisterAssingnDrugs(AssignDrugsDto model)
        {

            using (var context = new PHARMATIME_DBContext())
            {
                try
                {
                    var user = context.Usuarios.SingleOrDefault(u => u.IdUsuario == model.id_usuario);

                    if (user != null)
                    {
                        var DrugsPatient = new UsuarioMedicamento()
                        {
                            IdUsuario = model.id_usuario,
                            IdMedicamento = model.id_medicamento,   
                            Durante = model.durante,
                            Dosis = model.dosis,    
                            Intervalo = model.intervalo                           
                        };

                        context.UsuarioMedicamentos.Add(DrugsPatient);
                        context.SaveChanges();
                        return true;
                    }

                    return false;

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al asignar el medicamento al paciente: {ex.Message}");
                    return false;
                }

            }
        }


        public static bool RequestNewdrugsMail (MailNewDrugsDto model)
        {
            bool resultado = false;
            using (var context = new PHARMATIME_DBContext())
            {
                try
                {
                    var drugs = context.Medicamentos.SingleOrDefault(u => u.Nombre == model.Medicamento);

                    if (drugs == null)
                    {

                        string asunto = "Solicitud Nuevo Medicamento";
                        string mensaje = "Nombre del medicamento: " + model.Medicamento + " El paciente lo usa para: " + model.UsoDado;
                        string destinatario = "monsalveserrato42@gmail.com";
                        MailMessage mail = new MailMessage();
                        mail.To.Add(destinatario);
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
                catch (Exception ex)
                {
                    resultado = false;
                    Console.WriteLine($"Error al enviar solicitud: {ex.Message}");
                }
            }

                     return resultado;
        }

    }
}
