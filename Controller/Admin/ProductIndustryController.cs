using FleetingOffers.Common.Enum;
using FleetingOffers.Http;
using FleetingOffers.Module.Product;
using FleetingOffers.Settings;
using Microsoft.AspNetCore.Mvc;

namespace FleetingOffers.Controller;

[Route($"{HttpSettings.AdminRoutePrefix}/product/industry")]
[ApiController]
public class ProductIndustryController : AdminControllerBase
{
    private readonly ProductControllerService _service;
    
    public ProductIndustryController(ProductControllerService productService)
    {
        _service = productService;
    }

    /// <summary>
    /// List industries.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Organization, Admin, SuperAdmin
    /// </remarks>
    [HttpGet("list")]
    public async Task<IActionResult> GetIndustries()
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.PRODUCT_INDUSTRY,
            "LIST",
            async () =>
            {
                try
                {
                    var res = await _service.GetAllIndustriesAsync();
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
    [HttpGet("{id}")]
    public async Task<IActionResult> GetIndustryDetails(string id)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.PRODUCT_INDUSTRY,
            "DETAILS",
            async () =>
            {
                try
                {
                    var res = await _service.GetIndustryAsync(id);
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
    /// Creates an industry entry.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: SuperAdmin
    /// </remarks>
    [HttpPost("create")]
    public async Task<IActionResult> CreateProductIndustry([FromBody] CreateProductIndustryDto dto)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.PRODUCT_INDUSTRY,
            "CREATE",
            async () =>
            {
                try
                {
                    await _service.CreateIndustryAsync(dto);
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
    /// 🔐 Roles allowed: SuperAdmin
    /// </remarks>
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateProductIndustry(string id, [FromBody] UpdateProductIndustryDto dto)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.PRODUCT_INDUSTRY,
            "UPDATE",
            async () =>
            {
                try
                {
                    await _service.UpdateIndustryAsync(id, dto);
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
    /// 🔐 Roles allowed: SuperAdmin
    /// </remarks>
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteProductIndustry(string id)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.PRODUCT_INDUSTRY,
            "DELETE",
            async () =>
            {
                try
                {
                    await _service.DeleteIndustryAsync(id);
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
