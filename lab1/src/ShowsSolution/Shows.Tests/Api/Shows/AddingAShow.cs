using Alba;
using Shows.Api.Shows;
using Shows.Tests.Api.Fixtures;

namespace Shows.Tests.Api.Shows;

[Collection("SystemTestFixture")]
[Trait("Category", "SystemTest")]
public class AddingAShow(SystemTestFixture fixture)
{
    private readonly IAlbaHost _host = fixture.Host;

    [Fact]
    public async Task AddShow()
    {
        var showToCreate = new CreateShowRequest("Seinfeld", "what is it even about?", "Netflix");
        var response = await _host.Scenario(_ =>
        {
            _.Post.Json(showToCreate).ToUrl("/api/shows");
            _.StatusCodeShouldBeOk();
        });
        

    }
    
}

