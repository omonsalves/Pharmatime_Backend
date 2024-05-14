using Microsoft.EntityFrameworkCore;
using Pharmatime_Backend.Repositories.Models;
using Pharmatime_Backend.Utilities;

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


        public List<object>? ReadDrugs()
        {
            using (var context = new PHARMATIME_DBContext())
            {
                try
                {
                    var Drug = context.Medicamentos.SingleOrDefault(u => u.Estado == 1);

                    if (Drug != null)
                    {
                        var drugs = context.Medicamentos
                        .Include(i=> i.PresentacionNavigation)
                        .Select(u => new
                        {
                            IdMedicamento = u.IdMedicamento,
                            Nombre = u.Nombre,
                            SirvePara = u.SirvePara,
                            Presentacion = u.PresentacionNavigation.Descripcion,
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
                    var user = context.Usuarios.Where(u => u.TipoUsuario ==2).SingleOrDefault(u => u.IdUsuario == model.id_usuario);
                    
                    if (user != null)
                    {
                        var DrugsPatient = new UsuarioMedicamento()
                        {
                            IdUsuario = model.id_usuario,
                            IdMedicamento = model.id_medicamento,   
                            IdTutor = model.id_tutor,   
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
            Mail m = new Mail();    
            bool resultado = false;
            using (var context = new PHARMATIME_DBContext())
            {
                try
                {
                    var drug = context.Medicamentos.SingleOrDefault(u => u.Nombre == model.Medicamento);
                    if (drug == null)
                    {
                        var newDrug = new SolicitudMedicamento()
                        {
                            IdUsuario = model.IdUsuario,
                            NombreMedicamento = model.Medicamento,
                            UsoDado = model.UsoDado
                        };

                        context.SolicitudMedicamentos.Add(newDrug);
                        context.SaveChanges();

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
                                
                                <p>Esta solicitud se encuentra en su lista de solicitudes en PHARMATIME, procure su pronta solución</p>
                            </body>
                            </html>";


                        mensaje = mensaje.Replace("[Nombre del medicamento]", model.Medicamento);
                        mensaje = mensaje.Replace("[Uso del medicamento]", model.UsoDado);

                        string destinatario = "monsalveserrato42@gmail.com";

                        m.SendMail(destinatario, asunto, mensaje);
                        
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



        public List<object>? ReadRequestDrug()
        {
            using (var context = new PHARMATIME_DBContext())
            {

                try
                {
              

                    var drugs = context.SolicitudMedicamentos
                     .Join(context.Usuarios, // la tabla con la que unir
                     solicitud => solicitud.IdUsuario, // la llave primaria
                     usuario => usuario.IdUsuario, // la llave foránea
                    (solicitud, usuario) => new // seleccionar el resultado
                    {
                        IdSolicitudMedicamento = solicitud.IdSolicitudMedicamento,
                        NombreUsuario = usuario.Nombre + " " + usuario.Apellido,
                        NombreMedicamento = solicitud.NombreMedicamento,
                        UsoDado = solicitud.UsoDado
                    })
                    .ToList<object>();

                    return drugs;
                    
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al obtener los usuarios: {ex.Message}");
                    return null;
                }
            }
        }




        public static bool RequestAnswered(RequestAnsweredDto model)
        {
            Mail m = new Mail();
            using (var context = new PHARMATIME_DBContext())
            {
                try
                {
                    // Buscar el usuario por su ID en la base de datos
                    var request = context.SolicitudMedicamentos.SingleOrDefault(u => u.IdSolicitudMedicamento == model.IdSolicitud);
                    
                    if (request != null)
                    {
                        context.SolicitudMedicamentos.Remove(request);
                        context.SaveChanges();

                        var user = context.Usuarios.SingleOrDefault(u => u.IdUsuario == request.IdUsuario);

                        string correo = user.Correo;
                        string asunto = "Solicitud atendida";
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
                                                                
                                <h5>Pharmatime le informa que:</h5>
                                <p>La solicitud que envio para registrar el medicamento <span style='color: #008CBA;'>[medicamneto]</span> ya a sido atendida</p>
                                <p>El medicamento ya se encuentra registrado en la base de datos, revise la lista de medicamentos.</p>
                                <p>Si necesita más información o tiene alguna pregunta, no dude en ponerse en contacto con nosotros.</p>
                            </body>
                            </html>";

                        mensaje = mensaje.Replace("[medicamneto]", request.NombreMedicamento);
                        m.SendMail(correo, asunto, mensaje);

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



        public static bool DeleteS(RequestAnsweredDto model)
        {
            using (var context = new PHARMATIME_DBContext())
            {
                try
                {
                    // Buscar el usuario por su ID en la base de datos
                    var drug = context.SolicitudMedicamentos.FirstOrDefault(u => u.IdSolicitudMedicamento == model.IdSolicitud);

                    if (drug != null)
                    {
                        context.SolicitudMedicamentos.Remove(drug);
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

                    Console.WriteLine($"Error al eliminar la solicitud: {ex.Message}");
                    return false;
                }
            }
        }



    }
}
