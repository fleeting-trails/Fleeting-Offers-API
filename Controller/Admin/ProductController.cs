using FleetingOffers.Common.Enum;
using FleetingOffers.Http;
using FleetingOffers.Module.Product;
using FleetingOffers.Module.User;
using FleetingOffers.Settings;
using Microsoft.AspNetCore.Mvc;

namespace FleetingOffers.Controller;

[Route($"{HttpSettings.AdminRoutePrefix}/product")]
[ApiController]
public class ProductController : AdminControllerBase
{
    private readonly ProductControllerService _service;
    
    public ProductController(ProductControllerService productService)
    {
        _service = productService;
    }

    /// <summary>
    /// List own products paginated.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Organization, Admin, SuperAdmin
    /// </remarks>
    [HttpGet("list/own")]
    public async Task<IActionResult> GetOwnProductsPaginated([FromQuery] PaginationQueryDto paginationQuery)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.PRODUCT,
            "LIST_OWN",
            async () =>
            {
                try
                {
                    int page = paginationQuery.Page ?? 1;
                    int pageSize = paginationQuery.PageSize ?? 10;
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    var res = await _service.GetOwnProductsPaginatedAsync(authPayload.UserId, page, pageSize);
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
    /// List all products paginated.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Admin, Super Admin
    /// </remarks>
    [HttpGet("list")]
    public async Task<IActionResult> GetProductsPaginated([FromQuery] PaginationQueryDto paginationQuery)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.PRODUCT,
            "LIST_ALL",
            async () =>
            {
                try
                {
                    int page = paginationQuery.Page ?? 1;
                    int pageSize = paginationQuery.PageSize ?? 10;
                    var res = await _service.GetAllProductsPaginatedAsync(page, pageSize);
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
    /// Get own product details.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Super Admin, Admin, Organization
    /// </remarks>
    [HttpGet("get/own/{id}")]
    public async Task<IActionResult> GetOwnProduct(string id)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.PRODUCT,
            "READ_OWN",
            async () =>
            {
                try
                {
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    var res = await _service.GetOwnProductAsync(authPayload.UserId, id);
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
    /// Get any product details.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Super Admin, Admin
    /// </remarks>
    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetProduct(string id)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.PRODUCT,
            "READ",
            async () =>
            {
                try
                {
                    var res = await _service.GetProductAsync(id);
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
    /// Creates a product entry.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Organization
    /// </remarks>
    [HttpPost("create")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.PRODUCT,
            "CREATE",
            async () =>
            {
                try
                {
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    await _service.CreateProductAsync(authPayload.UserId, dto);
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
    /// Creates a product for organization by admin.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Admin
    /// </remarks>
    [HttpPost("create-by-admin")]
    public async Task<IActionResult> CreateProductByAdmin([FromBody] CreateProductAdminDto dto)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.PRODUCT,
            "CREATE_BY_ADMIN",
            async () =>
            {
                try
                {
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    if (authPayload.Role == USER_ROLE.SUPER_ADMIN)
                    {
                        return AppHttpResponse.Unauthorized();
                    }
                    await _service.CreateProductByAdminAsync(authPayload.UserId, dto);
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
    /// Update a product entry.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Admin, SuperAdmin, Organization
    /// </remarks>
    [HttpPut("update")]
    public async Task<IActionResult> UpdateProductDetails([FromBody] UpdateProductDetailsDto dto)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.PRODUCT,
            "UPDATE_OWN",
            async () =>
            {
                try
                {
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    await _service.UpdateProductDetailsAsync(authPayload.UserId, dto);
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
    /// Delete a product entry.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Admin, SuperAdmin, Organization
    /// </remarks>
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.PRODUCT,
            "DELETE_OWN",
            async () =>
            {
                try
                {
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    await _service.DeleteProductAsync(authPayload.UserId, id);
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
    /// Delete a product entry by admin.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Admin, SuperAdmin
    /// </remarks>
    [HttpDelete("delete-by-admin/{id}")]
    public async Task<IActionResult> DeleteProductByAdmin(string id)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.PRODUCT,
            "DELETE",
            async () =>
            {
                try
                {
                    await _service.DeleteProductByAdminAsync(id);
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
