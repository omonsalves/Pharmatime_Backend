using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using Pharmatime_Backend.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;


namespace Pharmatime_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {

        [HttpPost("ReportV2")]
        public IActionResult ReportV2(DeletePatientDto model)
        {
            var document = new PdfDocument();
            string htmlContent = @"
                <!DOCTYPE html>
                <html lang=""es"">
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <title>Reporte Horario de medicamentos</title>
                    <style>
                        body {
                            font-family: Arial, sans-serif;
                        }
                
                        #aaa {
                            grid-template-columns: repeat(3, 1fr);
                        }
                
                        .container {
                            margin: 30px;
                        }
                
                        .header {
                            display: flex;
                            justify-content: space-between;
                            margin-bottom: 20px;
                        }
                
                        .logo {
                            width: 140px;
                            height: 115px;
                        }
                
                        .logo img {
                            width: 100%;
                            height: 100%;
                        }
                
                        .contact-info {
                            padding: 30px 0;
                        }
                
                        .report-title {
                            text-align: center;
                            font-size: 20px;
                            font-weight: bold;
                            margin-bottom: 20px;
                        }
                
                        .report-description {
                            padding: 20px 0;
                            border-bottom: 1px solid #ccc;
                        }
                
                        .table-container {
                            margin-top: 20px;
                        }
                
                        table {
                            width: 100%;
                            border-collapse: collapse;
                        }
                
                        th, td {
                            border: 1px solid #ccc;
                            padding: 8px;
                            text-align: center;
                        }
                
                        .footer {
                            text-align: right;
                            margin-top: 20px;
                        }

                        .round-image {
                            border-radius: 50%; /* Define el radio de las esquinas para hacer el borde redondo */
                        }
                    </style>
                </head>
                <body>
                
                    <div class=""container"">
                
                        
                        <div class=""content"">
                            <div class=""report-title"">
                                <h3>
                                <strong>Reporte Horario de medicamentos</strong>
                                <h3/>
                            </div>
                            <div class=""report-description"">
                                Este reporte muestra los medicamentos asignados a cada uno de los pacientes de tutor [Nombre] con sus respectivas dosis y horas asignadas. Siendo el día [fecha] a la hora [hora]  
                            </div>
                            <div class=""table-container"">
                                <table>
                                    <thead>
                                        <tr style=""background-color: #427FAE; color: #ffffff;"">
                                            <th style=""background-color: #427FAE; color: #ffffff; border: 1px solid #257272; padding: 5px;>Paciente</th>
                                            <th style=""background-color: #427FAE; color: #ffffff; border: 1px solid #257272; padding: 5px;>Medicamento</th>
                                            <th style=""background-color: #427FAE; color: #ffffff; border: 1px solid #257272; padding: 5px;>Durante</th>
                                            <th style=""background-color: #427FAE; color: #ffffff; border: 1px solid #257272; padding: 5px;>Dosis</th>
                                            <th style=""background-color: #427FAE; color: #ffffff; border: 1px solid #257272; padding: 5px;>Intervalo</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                       [Cuerpo tabla]
                                    </tbody>
                                </table>
                            </div>
                        </div>

                    <div class=""contact-info"">
                <div>PHARMATIME</div>
                <div>Facatativá - Cundinamarca</div>
                <div>3107937196</div>
                <div>pharmatime8@gmail.com</div>
            </div>

                        <div class=""footer"">
                            <span id=""pageNumber"">Página</span>
                            <span id=""currentPageNumber"">1</span>
                            <span>de</span>
                            <span id=""totalPages"">1</span>
                        </div>
                    </div>
                </body>
                </html>
            ";

            DateTime fechaActual = DateTime.Now;

            // Convertir la fecha actual a string con el formato deseado
            string fecha = fechaActual.ToString("yyyy-MM-dd");
            string hora = fechaActual.ToString("HH:mm:ss");
            htmlContent = htmlContent.Replace("[fecha]", fecha);
            htmlContent = htmlContent.Replace("[hora]", hora);



            

            using (var context = new PHARMATIME_DBContext())
            {
                  List<UsuarioMedicamento> datosMedicamentos;
                var validarContenido = context.Usuarios.SingleOrDefault(u => u.IdUsuario == model.IdUsuario);
                  datosMedicamentos = context.UsuarioMedicamentos.Where(u=>u.IdTutor == model.IdUsuario).ToList();
                  string nombreTutor = " ";
                  StringBuilder tableRows = new StringBuilder();
                  foreach (var datos in datosMedicamentos)
                  {
                      var tutor = context.Usuarios.SingleOrDefault(u => u.IdUsuario == datos.IdTutor);
                      nombreTutor = tutor.Nombre + " " + tutor.Apellido;
                      var paciente = context.Usuarios.SingleOrDefault(u => u.IdUsuario == datos.IdUsuario);
                      var medicamento = context.Medicamentos.SingleOrDefault(u => u.IdMedicamento == datos.IdMedicamento);
                      tableRows.Append("<tr>");
                      tableRows.Append($"<td>{paciente.Nombre + " " + paciente.Apellido}</td>");
                      tableRows.Append($"<td>{medicamento.Nombre}</td>");
                      tableRows.Append($"<td>{datos.Durante}</td>");
                      tableRows.Append($"<td>{datos.Dosis}</td>");
                      tableRows.Append($"<td>{datos.Intervalo}</td>");
                      tableRows.Append("</tr>");
                  }
                  htmlContent = htmlContent.Replace("[Nombre]", nombreTutor);
                  htmlContent = htmlContent.Replace("[Cuerpo tabla]", tableRows.ToString());
                
                if(validarContenido == null)
                {
                    return StatusCode(400, "Error al generar el reporte");
                }

            }



            PdfGenerator.AddPdfPages(document, htmlContent, PageSize.A4);
            byte[]? response = null;
            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                response = ms.ToArray();
            }
            string fechaNombreDocumento = DateTime.Now.ToString("yyyy-MM-dd_HH-mm");
            string fileName = $"Report_{fechaNombreDocumento}.pdf";
            return File(response, "aplication/pdf", fileName);
        }


    }
}
