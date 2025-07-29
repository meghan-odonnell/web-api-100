using Microsoft.AspNetCore.Mvc;

namespace SoftwareCenter.Api.Vendors;

public class Controller : ControllerBase
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
        
        return Ok(response);  // by inheriting from ControllerBase, we can use Ok();
    }
}

public record CreateVendorRequest(string Name, string Url, CreateVendorPointOfContactRequest PointOfContact);

public record CreateVendorPointOfContactRequest(string Name, string Phone, string Email);

public record CreateVendorResponse(
    Guid Id,
    string Name, string Url, CreateVendorPointOfContactRequest PointOfContact
    );