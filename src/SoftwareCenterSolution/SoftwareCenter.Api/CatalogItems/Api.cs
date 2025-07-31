using System.Runtime.Intrinsics.Arm;
using Marten;
using Microsoft.AspNetCore.Http.HttpResults;
namespace SoftwareCenter.Api.CatalogItems;

public static class Api
{
    //this is a .NET extension something going to make it look like it's a web app something
    // not in a controller class anymore, so can't used return OK(), like on the other one
    public static IEndpointRouteBuilder MapCatalogItems(this IEndpointRouteBuilder app)
    {
        //var group = app.MapGroup("catalog-items");
        //group.MapGet("", () =>
        //{
        //    var catalogItems = new List<string>();
        //    return Results.Ok(catalogItems);
        //});

        //group.MapPost("", () =>
        //{
        //    return Results.NoContent();
        //});
        
        app.MapPost("/vendors/{id:guid}/items", async (
            Guid Id,
            CatalogItemCreateRequest request,
            IDocumentSession session) =>
        {
            //validate teh stuff
            // create entity 

            // map from CatalogItemCreateRequest to CatalogItemEntity
            //var entity = new CatalogItemEntity
            //{
            //    Id = Guid.NewGuid(),
            //    Created = DateTimeOffset.UtcNow,
            //    Description = request.Description,
            //    Name = request.Name,
            //    Version = request.Version,
            //};
            
            var entity = request.MapToEntity(Id);


            //save to DB(side effect),
            session.Store(entity);
            await session.SaveChangesAsync();
            
            //create response.

            return Results.Ok();
        });
        return app;
    }



public static IServiceCollection AddCataLogItems(this IServiceCollection services)
    {
        //services.AddScoped
        return services;
    }

}

public record CatalogItemCreateRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty ;
    public string Version { get; set; } = string.Empty ; 

    public CatalogItemEntity MapToEntity()  // map the entity here instead of mapping it up there in the AddCatalogItem Method
                                            // then you can create a unit test to verify it
                                            // or use tools like Mapperly, AutoMapper
    {
        {
            Id = Guid.NewGuid(),
            Created = DateTimeOffset.UtcNow,
            Description = Description,
            Name = Name,
            Version = Version,
            }
        ;
    }
}

public record CatalogItemDetails
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;


}

public class CatalogItemEntity  // basiclly the details, plus anything else we'd want to save in database about the Item
    //entity is something with an ID
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public DateTimeOffset Created { get; set; }

    // add the method of the thing you want to send back to the DB
    // you could make it an extension but soemthing something
    public CatalogItemDetails MapToResponse()
    {
        return new CatalogItemDetails
        {
            Id = Id,
            Description = Description,
            Name = Name,
            Version = Version
        };
    }
}