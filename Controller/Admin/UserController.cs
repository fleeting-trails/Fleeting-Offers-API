using FleetingOffers.Common.Enum;
using FleetingOffers.Http;
using FleetingOffers.Module.User;
using FleetingOffers.Settings;
using Microsoft.AspNetCore.Mvc;

namespace FleetingOffers.Controller;

[Route($"{HttpSettings.AdminRoutePrefix}/user")]
[ApiController]
public class UserController : AdminControllerBase
{
    private readonly UserService _service;
    public UserController(UserService userService)
    {
        _service = userService;
    }

    /// <summary>
    /// List all users paginated.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Admin, SuperAdmin
    /// </remarks>
    /// <response code="200">Users list</response>
    /// <response code="401">Unauthorized: Access Denied</response>
    [HttpGet("list")]
    public async Task<IActionResult> GetUsersPaginated([FromQuery] PaginationQueryDto paginationQuery)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.USER,
            "LIST",
            async () =>
            {
                try
                {
                    int page = paginationQuery.Page ?? 1;
                    int pageSize = paginationQuery.PageSize ?? 10;
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    var res = await _service.GetUsersPaginatedAsync(page, pageSize);
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
    /// Get user details.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Admin, SuperAdmin
    /// </remarks>
    /// <response code="200">User details</response>
    /// <response code="401">Unauthorized: Access Denied</response>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.USER,
            "DETAILS",
            async () =>
            {
                try
                {
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    var res = await _service.GetUserAsync(id);
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
    /// Create a user.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Admin, SuperAdmin
    /// </remarks>
    /// <response code="200">User created</response>
    /// <response code="401">Unauthorized: Access Denied</response>
    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.USER,
            "CREATE",
            async () =>
            {
                try
                {
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    await _service.CreateUserAsync(dto);
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
    /// Update a user.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Admin, SuperAdmin
    /// </remarks>
    /// <response code="200">User updated</response>
    /// <response code="401">Unauthorized: Access Denied</response>
    [HttpPut("update")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto dto)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.USER,
            "UPDATE",
            async () =>
            {
                try
                {
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    await _service.UpdateUserAsync(dto);
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
    /// Delete a user.
    /// </summary>
    /// <remarks>
    /// 🔐 Roles allowed: Admin, SuperAdmin
    /// </remarks>
    /// <response code="200">User deleted</response>
    /// <response code="401">Unauthorized: Access Denied</response>
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        return await WithPermission(
            HttpContext,
            APP_MODULE.USER,
            "DELETE",
            async () =>
            {
                try
                {
                    var authPayload = HttpHelper.GetAuthorizationPayload(HttpContext);
                    await _service.DeleteUserAsync(id);
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