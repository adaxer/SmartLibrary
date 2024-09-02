using SmartLibrary.Api.Endpoints.Books;
using SmartLibrary.Auth.Endpoints;

namespace SmartLibrary.Api.Endpoints;

public static class EndpointExtensions
{
    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapAccountEndpoints();

        app.MapSaveBookEndpoint();
    }
}
