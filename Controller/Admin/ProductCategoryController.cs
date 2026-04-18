using FleetingOffers.Common.Enum;
using FleetingOffers.Http;
using FleetingOffers.Module.Product;
using FleetingOffers.Settings;
using Microsoft.AspNetCore.Mvc;

namespace FleetingOffers.Controller;

[Route($"{HttpSettings.AdminRoutePrefix}/product/category")]
[ApiController]
public class ProductCategoryController : AdminControllerBase
{
    private readonly ProductControllerService _service;
    
    public ProductCategoryController(ProductControllerService productService)
    {
        _service = productService;
    }

    /// <summary>
    /// List categories.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Organization, Admin, SuperAdmin
    /// </remarks>
    [HttpGet("list")]
    public async Task<IActionResult> GetCategories()
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.PRODUCT_CATEGORY,
            "LIST",
            async () =>
            {
                try
                {
                    var res = await _service.GetAllCategoriesAsync();
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
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryDetails(string id)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.PRODUCT_CATEGORY,
            "DETAILS",
            async () =>
            {
                try
                {
                    var res = await _service.GetCategoryAsync(id);
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
    /// 🔐 Roles allowed: SuperAdmin
    /// </remarks>
    [HttpPost("create")]
    public async Task<IActionResult> CreateProductCategory([FromBody] CreateProductCategoryDto dto)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.PRODUCT_CATEGORY,
            "CREATE",
            async () =>
            {
                try
                {
                    await _service.CreateCategoryAsync(dto);
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
    /// Update a category entry.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: SuperAdmin
    /// </remarks>
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateProductCategory(string id, [FromBody] UpdateProductCategoryDto dto)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.PRODUCT_CATEGORY,
            "UPDATE",
            async () =>
            {
                try
                {
                    await _service.UpdateCategoryAsync(id, dto);
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
    /// Delete a category entry.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: SuperAdmin
    /// </remarks>
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteProductCategory(string id)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.PRODUCT_CATEGORY,
            "DELETE",
            async () =>
            {
                try
                {
                    await _service.DeleteCategoryAsync(id);
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
