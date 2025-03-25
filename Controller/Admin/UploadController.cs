using FleetingOffers.Common.Enum;
using FleetingOffers.Http;
using FleetingOffers.Modifier;
using FleetingOffers.Module.Upload;
using FleetingOffers.Settings;
using Microsoft.AspNetCore.Mvc;

namespace FleetingOffers.Controller;

[Route($"{HttpSettings.AdminRoutePrefix}/upload")]
[ApiController]
public class UploadController : AdminControllerBase
{
    private readonly UploadService _service;
    public UploadController(UploadService uploadService)
    {
        _service = uploadService;
    }

    [HttpPost("files")]
    public async Task<IActionResult> UploadFiles([FromForm] IFormFileCollection files)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.UPLOAD,
            "CREATE",
            async () =>
            {
                try
                {
                    if (files == null || !files.Any())
                    {
                        return AppHttpResponse.BadRequest("No files received");
                    }

                    if (!UploadService.IsAllUploadedFileExtensionsValid(files))
                    {
                        return AppHttpResponse.BadRequest($"All Files must be {string.Join(", ", UploadSettings.AllowedExtensions)}");
                    }
                    var uploaded = await _service.UploadFilesAsync(files);
                    return AppHttpResponse.Ok(uploaded, "Files Uploaded Successfully");
                }
                catch (Exception e)
                {
                    return AppHttpResponse.BadRequest(e.Message);
                }
            }
        );
    }
}