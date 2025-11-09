using FleetingOffers.Common.Enum;
using FleetingOffers.Http;
using FleetingOffers.Module.Advertise;
using FleetingOffers.Settings;
using Microsoft.AspNetCore.Mvc;

namespace FleetingOffers.Controller;

[Route($"{HttpSettings.AdminRoutePrefix}/advertise/category")]
[ApiController]
public class AdvertiseCategoryController : AdminControllerBase
{
    private readonly AdvertiseCategoryService _service;
    public AdvertiseCategoryController(AdvertiseCategoryService categoryService)
    {
        _service = categoryService;
    }
    /// <summary>
    /// List categories paginated.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Organization, Admin, SuperAdmin
    /// </remarks>
    /// <response code="200">Category Details</response>
    /// <response code="401">Unauthorized: Access Denied</response>
    [HttpGet("list/")]
    public async Task<IActionResult> GetCategoryPaginated([FromQuery] PaginationQueryDto paginationQuery)  
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.ADVERTISE_CATEGORY,
            "LIST",
            async () =>
            {
                try
                {
                    int page = paginationQuery.Page ?? 1;
                    int pageSize = paginationQuery.PageSize ?? 10;
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    var res = await _service.GetCategoriesPaginatedAsync(page, pageSize);
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
    /// Get category details.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Super Admin, Admin, Organization
    /// </remarks>
    /// <response code="200">Category Details</response>
    /// <response code="401">Unauthorized: Access Denied</response>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryDetails(string id)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.ADVERTISE_CATEGORY,
            "DETAILS",
            async () =>
            {
                try
                {
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    var res = await _service.GetCategoryByIdAsync(id);
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
    /// Creates a category entry.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Organization
    /// </remarks>
    /// <response code="200">Category created</response>
    /// <response code="401">Unauthorized: Access Denied</response>
    [HttpPost("create")]
    public async Task<IActionResult> CreateAdvertiseCategory([FromBody] CreateAdvertiseCategoryDto dto)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.ADVERTISE_CATEGORY,
            "CREATE",
            async () =>
            {
                try
                {
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    await _service.CreateAdvertiseCategoryAsync(dto);
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
    /// Update an category entry.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Admin, SuperAdmin, Organization
    /// </remarks>
    /// <response code="200">Category updated</response>
    /// <response code="401">Unauthorized: Access Denied</response>
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAdvertiseCategory(string id, [FromBody] UpdateAdvertiseCategoryDto dto)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.ADVERTISE_CATEGORY,
            "UPDATE",
            async () =>
            {
                try
                {
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    await _service.UpdateAdvertiseCategoryAsync(id, dto);
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
    /// Delete an category entry.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Admin, SuperAdmin, Organization
    /// </remarks>
    /// <response code="200">Category deleted</response>
    /// <response code="401">Unauthorized: Access Denied</response>
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAdvertiseCategory(string id)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.ADVERTISE_CATEGORY,
            "DELETE",
            async () =>
            {
                try
                {
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    await _service.DeleteAdvertiseCategoryAsync(id);
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