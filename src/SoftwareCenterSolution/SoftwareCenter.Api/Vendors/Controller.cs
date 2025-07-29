using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareCenter.Api.Vendors;

public class Controller(IDocumentSession session) : ControllerBase
{
    //Controller based routing
    //this is the method you should call when a POST /vendors is received.
    [HttpPost("/vendors")]
    public async Task<ActionResult> AddAVendorAsync(
        [FromBody] CreateVendorRequest request,        // [FromBody] is a little helper 
        CancellationToken token)
    {
        // validation
        // field validation - what's required, optional.. what are the rules for required things

        // Mapping Code (copy from one object to another)
        var response = new CreateVendorResponse(
            Guid.NewGuid(),
            request.Name,
            request.Url,
            request.PointOfContact
            );
        session.Store(response); //Marten stuff
        await session.SaveChangesAsync();//Marten

        return Ok(response);  // by inheriting from ControllerBase, we can use Ok();
    }

   
    // GET /vendors/tacos
    [HttpGet("/vendors/{id:guid}")]
    public async Task<ActionResult> GetVendorByIdAsync(Guid id, CancellationToken token)
    {
        // look that thing up in the database.
        var response = await session
            .Query<CreateVendorResponse>()
            .Where(v => v.Id == id)
            .SingleOrDefaultAsync();

        if (response is null)
        {
            return NotFound();
        }
        else
        {
            return Ok(response);
        }
    }

    [HttpGet("/vendors")]
    public async Task<ActionResult> GetAllVendorsAsync(CancellationToken token)
    {
        var vendors = await session.Query<CreateVendorResponse>().ToListAsync();

        return Ok(vendors);
    }

}

/*{
    "name": "Microsoft",
    "url": "https://wwww.microsoft.com",
    "pointOfContact": {
        "name": "Satya",
        "phone": "800 big-boss",
        "email": "satya@microsoft.com"
    }
}*/


public record CreateVendorRequest
{

    public string Name { get; init; } = string.Empty;
    public string Url { get; init; } = string.Empty;
    public CreateVendorPointOfContactRequest PointOfContact { get; init; } = new();
}

public class CreateVendorRequestValidator : AbstractValidator<CreateVendorRequest>
{
    public CreateVendorRequestValidator()
    {
        RuleFor(v => v.Name).NotEmpty().MinimumLength(3).MaximumLength(255);
        RuleFor(v => v.Url).NotEmpty();
        RuleFor(v => v.PointOfContact).NotNull();
    }
}

public record CreateVendorPointOfContactRequest
{
    public string Name { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
};

public class CreateVendorPointOfContactRequestValidator :
    AbstractValidator<CreateVendorPointOfContactRequest>
{
    public CreateVendorPointOfContactRequestValidator()
    {
        RuleFor(p => p.Name).NotEmpty();

    }
}

public record CreateVendorResponse(
    Guid Id,
    string Name, string Url, CreateVendorPointOfContactRequest PointOfContact
    );


