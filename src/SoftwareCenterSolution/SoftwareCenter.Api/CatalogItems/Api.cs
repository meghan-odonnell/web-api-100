using Marten;
namespace SoftwareCenter.Api.CatalogItems;

public static class Api
{
    public static IEndpointRouteBuilder MapCatalogItems(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("catalog-items");
        group.MapGet("", () =>
        {
            var catalogItems = new List<string>();
            return Results.Ok(catalogItems);
        });

        group.MapPost("", () =>
        {
            return Results.NoContent();
        });
        return app;
    }
    //this is a .NET extension something going to make it look like it's a web app something
    // not in a controller class anymore, so can't used return OK(), like on the other one


public static IServiceCollection AddCataLogItems(this IServiceCollection services)
    {
        //services.AddScoped
        return services;
    }

}