using FleetingOffers.Http;
using Microsoft.AspNetCore.Mvc;

namespace FleetingOffers.Controller;

[Route("uploads")]
[ApiController]
class FileController : ControllerBase {
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