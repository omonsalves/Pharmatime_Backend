using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using System;

namespace Pharmatime_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {


        [HttpPost("Report")]
        public async Task<IActionResult> Report()
        {
            string fechaActual = DateTime.Now.ToString("yyyy-MM-dd_HH-mm");
            ViewAsPdf view = new();
            byte[] data = await view.BuildFile(ControllerContext);
            return new FileContentResult(data, "application/pdf")
            {
                FileDownloadName = $"Report_{fechaActual}.pdf"
            };


        }


    }
}
