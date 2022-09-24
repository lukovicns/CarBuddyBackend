using CarBuddy.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace CarBuddy.WebApi.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void MapHubs(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHub<ChatHub>("/chat-hub");
            endpoints.MapHub<TripRequestHub>("/trip-request-hub");
        }
    }
}
