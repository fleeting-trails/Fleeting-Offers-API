using Microsoft.AspNetCore.Mvc;

namespace FleetingOffers.Http;

public static class AppHttpResponse {
    public static ActionResult Ok(object? data, string message = "Success")
    {
        var response = new
        {
            Success = true,
            Message = message,
            Data = data
        };
        return new OkObjectResult(response);
    }
    public static ActionResult CreatedAtRoute(string route, string Id, object? data, string message = "Success")
    {
        var response = new
        {
            Success = true,
            Message = message,
            Data = data
        };
        // return Results.CreatedAtRoute("GetFileSingle", new { Id = Id }, response);
        return new CreatedAtRouteResult("GetFileSingle", new { Id = Id }, response);
    }
    public static ActionResult BadRequest(string message = "Bad Request!")
    {
        var response = new
        {
            Success = false,
            Message = message
        };
        return new BadRequestObjectResult(response);
    }
    public static ActionResult Unauthorized()
    {
        return new UnauthorizedObjectResult(new
        {
            Success = false,
            Message = "Unauthorized"
        }); 
    }
}