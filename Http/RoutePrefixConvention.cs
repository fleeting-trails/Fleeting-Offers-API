namespace FleetingOffers.Http;

using Microsoft.AspNetCore.Mvc.ApplicationModels;

public class RoutePrefixConvention : IApplicationModelConvention
{
    private readonly string _prefix;

    public RoutePrefixConvention(string prefix)
    {
        _prefix = prefix;
    }

    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            var routeAttribute = controller.Selectors.FirstOrDefault(s => s.AttributeRouteModel != null);
            if (routeAttribute != null)
            {
                routeAttribute.AttributeRouteModel.Template = $"{_prefix}/{routeAttribute.AttributeRouteModel.Template}";
            }
        }
    }
}
