using FleetingOffers.Common.Enum;
using FleetingOffers.Http;
using FleetingOffers.Module.Advertise;
using FleetingOffers.Settings;
using Microsoft.AspNetCore.Mvc;

namespace FleetingOffers.Controller;

[Route($"{HttpSettings.AdminRoutePrefix}/advertise")]
[ApiController]
public class AdvertiseController : AdminControllerBase
{
    private readonly AdvertiseControllerService _service;
    public AdvertiseController(AdvertiseControllerService advertiseService)
    {
        _service = advertiseService;
    }
    /// <summary>
    /// List own advertises paginated.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Organization, Admin, SuperAdmin
    /// </remarks>
    /// <response code="200">AD Details</response>
    /// <response code="401">Unauthorized: Access Denied</response>
    [HttpGet("list/own")]
    public async Task<IActionResult> GetOwnAdvertisesPaginated([FromQuery] PaginationQueryDto paginationQuery)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.ADVERTISE,
            "LIST_OWN",
            async () =>
            {
                try
                {
                    int page = paginationQuery.Page ?? 1;
                    int pageSize = paginationQuery.PageSize ?? 10;
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    var res = await _service.GetOwnAdvertisesPaginatedAsync(authPayload.UserId, page, pageSize);
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
    /// List all advertise paginated.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Admin, Super Admin
    /// </remarks>
    /// <response code="200">AD Details</response>
    /// <response code="401">Unauthorized: Access Denied</response>
    [HttpGet("list")]
    public async Task<IActionResult> GetAdvertisesPaginated([FromQuery] PaginationQueryDto paginationQuery)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.ADVERTISE,
            "LIST_ALL",
            async () =>
            {
                try
                {
                    int page = paginationQuery.Page ?? 1;
                    int pageSize = paginationQuery.PageSize ?? 10;
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    var res = await _service.GetAllAdvertisesPaginatedAsync(page, pageSize);
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
    /// Get own advertise details.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Super Admin, Admin, Organization
    /// </remarks>
    /// <response code="200">AD Details</response>
    /// <response code="401">Unauthorized: Access Denied</response>
    [HttpGet("get/own/{id}")]
    public async Task<IActionResult> GetOwnAdvertise(string id)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.ADVERTISE,
            "READ_OWN",
            async () =>
            {
                try
                {
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    var res = await _service.GetOwnAdvertiseAsync(authPayload.UserId, id);
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
    /// Get any advertise details.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Super Admin, Admin
    /// </remarks>
    /// <response code="200">AD Details</response>
    /// <response code="401">Unauthorized: Access Denied</response>
    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetAdvertise(string id)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.ADVERTISE,
            "READ",
            async () =>
            {
                try
                {
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    var res = await _service.GetAdvertiseAsync(id);
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
    /// Creates an advertise entry.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Organization
    /// </remarks>
    /// <response code="200">Ad created</response>
    /// <response code="401">Unauthorized: Access Denied</response>
    [HttpPost("create")]
    public async Task<IActionResult> CreateAdvertise([FromBody] CreateAdvertiseDto dto)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.ADVERTISE,
            "CREATE",
            async () =>
            {
                try
                {
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    await _service.CreateAdvertiseAsync(authPayload.UserId, dto);
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
    /// Creates an advertise for organization by admin.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Admin, SuperAdmin
    /// </remarks>
    /// <response code="200">Ad created</response>
    /// <response code="401">Unauthorized: Access Denied</response>
    [HttpPost("create-by-admin")]
    public async Task<IActionResult> CreateAdvertiseByAdmin([FromBody] CreateAdvertiseAdminDto dto)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.ADVERTISE,
            "CREATE_BY_ADMIN",
            async () =>
            {
                try
                {
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    await _service.CreateAdvertiseByAdminAsync(authPayload.UserId, dto);
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
    /// Update an advertise entry.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Admin, SuperAdmin, Organization
    /// </remarks>
    /// <response code="200">Ad created</response>
    /// <response code="401">Unauthorized: Access Denied</response>
    [HttpPut("update")]
    public async Task<IActionResult> UpdateAdvertiseDetails([FromBody] UpdateAdvertiseDetailsDto dto)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.ADVERTISE,
            "UPDATE_OWN",
            async () =>
            {
                try
                {
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    await _service.UpdateAdvertiseDetailsAsync(authPayload.UserId, dto);
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