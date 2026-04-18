using FleetingOffers.Common.Enum;
using FleetingOffers.Http;
using FleetingOffers.Module.Product;
using FleetingOffers.Settings;
using Microsoft.AspNetCore.Mvc;

namespace FleetingOffers.Controller;

[Route($"{HttpSettings.AdminRoutePrefix}/product/deal")]
[ApiController]
public class ProductDealController : AdminControllerBase
{
    private readonly ProductControllerService _service;

    public ProductDealController(ProductControllerService productService)
    {
        _service = productService;
    }

    /// <summary>
    /// List deals.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: SuperAdmin
    /// </remarks>
    [HttpGet("list")]
    public async Task<IActionResult> GetDeals()
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.PRODUCT_DEAL,
            "LIST",
            async () =>
            {
                try
                {
                    var res = await _service.GetAllDealsAsync();
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
    /// Get deal details.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: SuperAdmin
    /// </remarks>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDealDetails(string id)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.PRODUCT_DEAL,
            "DETAILS",
            async () =>
            {
                try
                {
                    var res = await _service.GetDealAsync(id);
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
    /// Creates a deal entry.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: SuperAdmin
    /// </remarks>
    [HttpPost("create")]
    public async Task<IActionResult> CreateProductDeal([FromBody] CreateProductDealDto dto)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.PRODUCT_DEAL,
            "CREATE",
            async () =>
            {
                try
                {
                    await _service.CreateDealAsync(dto);
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
    /// Update a deal entry.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: SuperAdmin
    /// </remarks>
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateProductDeal(string id, [FromBody] UpdateProductDealDto dto)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.PRODUCT_DEAL,
            "UPDATE",
            async () =>
            {
                try
                {
                    await _service.UpdateDealAsync(id, dto);
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
    /// Delete a deal entry.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: SuperAdmin
    /// </remarks>
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteProductDeal(string id)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.PRODUCT_DEAL,
            "DELETE",
            async () =>
            {
                try
                {
                    await _service.DeleteDealAsync(id);
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