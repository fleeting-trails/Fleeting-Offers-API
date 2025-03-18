using FleetingOffers.Http;
using FleetingOffers.Modifier;
using Microsoft.AspNetCore.Mvc;

namespace FleetingOffers.Controller;

[Route("uploads")]
[ApiController]
class FileController : AdminController {
    [HttpPost("files")]
    public IActionResult UploadFiles([FromForm] IFormCollection files) {
        try {
            // Upload file logic here
            return AppHttpResponse.Ok("File Uploaded Successfully");
        } catch (Exception e) {
            return AppHttpResponse.BadRequest(e.Message);
        }
    }
}