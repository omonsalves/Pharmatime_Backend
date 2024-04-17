using Pharmatime_Backend.Repositories.Models;
using Pharmatime_Backend.Utilities;
using System.Net.Mail;
using System.Net;

namespace Pharmatime_Backend.Repositories
{
    public class DrugsRepository
    {

        public static bool RegisterDrugs(RegisterDrugsDto model)
        {
           
            using (var context = new PHARMATIME_DBContext())
            {
                try
                {
                    var Drug = context.Medicamentos.SingleOrDefault(u => u.Nombre == model.Nombre);

                    if (Drug == null)
                    {
                        var Drugs = new Medicamento()
                        {

                            Nombre = model.Nombre,
                            SirvePara = model.sirve_para,
                            Presentacion = model.Presentacion,
                            Contraindicaciones = model.contraindicaciones,
                            Estado = 1
                        };

                        context.Medicamentos.Add(Drugs);
                        context.SaveChanges();
                        return true;
                    }

                    return false;

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al guardar el medicamento: {ex.Message}");
                    return false;
                }

            }
        }


        public List<object> ReadDrugs()
        {
            using (var context = new PHARMATIME_DBContext())
            {
                try
                {
                    var Drug = context.Medicamentos.SingleOrDefault(u => u.Estado == 1);

                    if (Drug != null)
                    {
                        var drugs = context.Medicamentos
                        .Select(u => new
                        {
                            IdMedicamento = u.IdMedicamento,
                            Nombre = u.Nombre,
                            SirvePara = u.SirvePara,
                            Presentacion = u.Presentacion,
                            Contraindicaciones = u.Contraindicaciones
                        })
                        .ToList<object>();
                        return drugs;
                    }
                    return null;
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
                                                                
                                <p>Medicamento: <span style='color: #008CBA;'>[Nombre del medicamento]</span></p>
                                
                                <p>El paciente lo usa para:</p>
                                
                                <p><span style='color: #008CBA;'>[Uso del medicamento]</span></p>
                                
                                <p>Si necesita más información o tiene alguna pregunta, no dude en ponerse en contacto con nosotros.</p>
                            </body>
                            </html>";


                        mensaje = mensaje.Replace("[Nombre del medicamento]", model.Medicamento);
                        mensaje = mensaje.Replace("[Uso del medicamento]", model.UsoDado);

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


        public static bool DeleteDrug(DeleteDrugDto model)
        {
            using (var context = new PHARMATIME_DBContext())
            {
                try
                {
                    // Buscar el usuario por su ID en la base de datos
                    var drug = context.Medicamentos.FirstOrDefault(u => u.IdMedicamento == model.IdMedicamento);

                    if (drug != null)
                    {
                        drug.Estado = 2;
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


        public static bool RecoverDrug(DeleteDrugDto model)
        {
            using (var context = new PHARMATIME_DBContext())
            {
                try
                {
                    // Buscar el usuario por su ID en la base de datos
                    var drug = context.Medicamentos.FirstOrDefault(u => u.IdMedicamento == model.IdMedicamento);

                    if (drug != null)
                    {
                        drug.Estado = 1;
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
