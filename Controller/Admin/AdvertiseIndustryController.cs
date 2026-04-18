using FleetingOffers.Common.Enum;
using FleetingOffers.Http;
using FleetingOffers.Module.Advertise;
using FleetingOffers.Settings;
using Microsoft.AspNetCore.Mvc;

namespace FleetingOffers.Controller;

[Route($"{HttpSettings.AdminRoutePrefix}/advertise/industry")]
[ApiController]
public class AdvertiseIndustryController : AdminControllerBase
{
    private readonly AdvertiseIndustryService _service;
    public AdvertiseIndustryController(AdvertiseIndustryService industryService)
    {
        _service = industryService;
    }
    /// <summary>
    /// List industries paginated.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Organization, Admin, SuperAdmin
    /// </remarks>
    /// <response code="200">Industry Details</response>
    /// <response code="401">Unauthorized: Access Denied</response>
    [HttpGet("list/")]
    public async Task<IActionResult> GetIndustryPaginated([FromQuery] PaginationQueryDto paginationQuery)  
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.ADVERTISE_INDUSTRY,
            "LIST",
            async () =>
            {
                try
                {
                    int page = paginationQuery.Page ?? 1;
                    int pageSize = paginationQuery.PageSize ?? 10;
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    var res = await _service.GetIndustriesPaginatedAsync(page, pageSize);
                    return AppHttpResponse.Ok(res, "Ok");
                }
                catch (Exception ex)
                {
                    return AppHttpResponse.BadRequest(ex.Message);
                }
            }
        );
    }

    /// <summary> 
    /// Get industry details.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Super Admin, Admin, Organization
    /// </remarks>
    /// <response code="200">Industry Details</response>
    /// <response code="401">Unauthorized: Access Denied</response>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetIndustryDetails(string id)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.ADVERTISE_INDUSTRY,
            "DETAILS",
            async () =>
            {
                try
                {
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    var res = await _service.GetIndustryByIdAsync(id);
                    return AppHttpResponse.Ok(res, "Ok");
                }
                catch (Exception ex)
                {
                    return AppHttpResponse.BadRequest(ex.Message);
                }
            }
        );

    }

    /// <summary>
    /// Creates a industry entry.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Organization
    /// </remarks>
    /// <response code="200">Industry created</response>
    /// <response code="401">Unauthorized: Access Denied</response>
    [HttpPost("create")]
    public async Task<IActionResult> CreateAdvertiseIndustry([FromBody] CreateAdvertiseIndustryDto dto)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.ADVERTISE_INDUSTRY,
            "CREATE",
            async () =>
            {
                try
                {
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    await _service.CreateAdvertiseIndustryAsync(dto);
                    return AppHttpResponse.Ok("Ok");
                }
                catch (Exception ex)
                {
                    return AppHttpResponse.BadRequest(ex.Message);
                }
            }
        );

    }

    /// <summary>
    /// Update an industry entry.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Admin, SuperAdmin, Organization
    /// </remarks>
    /// <response code="200">Industry updated</response>
    /// <response code="401">Unauthorized: Access Denied</response>
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAdvertiseIndustry(string id, [FromBody] UpdateAdvertiseIndustryDto dto)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.ADVERTISE_INDUSTRY,
            "UPDATE",
            async () =>
            {
                try
                {
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    await _service.UpdateAdvertiseIndustryAsync(id, dto);
                    return AppHttpResponse.Ok("Ok");
                }
                catch (Exception ex)
                {
                    return AppHttpResponse.BadRequest(ex.Message);
                }
            }
        );

    }

    /// <summary>
    /// Delete an industry entry.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Admin, SuperAdmin, Organization
    /// </remarks>
    /// <response code="200">Industry deleted</response>
    /// <response code="401">Unauthorized: Access Denied</response>
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAdvertiseIndustry(string id)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.ADVERTISE_INDUSTRY,
            "DELETE",
            async () =>
            {
                try
                {
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    await _service.DeleteAdvertiseIndustryAsync(id);
                    return AppHttpResponse.Ok("Ok");
                }
                catch (Exception ex)
                {
                    return AppHttpResponse.BadRequest(ex.Message);
                }
            }
        );

    }
}