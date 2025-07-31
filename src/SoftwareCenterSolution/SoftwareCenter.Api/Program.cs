using FluentValidation;
using Marten;
using SoftwareCenter.Api.CatalogItems;
using SoftwareCenter.Api.Vendors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorizationBuilder().AddPolicy("CanAddVendor", pol =>
{
    pol.RequireRole("Manager");
    pol.RequireRole("SoftwareCenter");
});



// .NET has a good way of doing the connection string.
// If there's no connection string, throw the exception
// checks Appsettings.json, then launchSettings.json.
var connectionString = builder.Configuration.GetConnectionString("db") ?? throw new Exception("Need a connection string.");
Console.WriteLine("Using Connection String: " + connectionString);




builder.Services.AddMarten(config =>
{
    // it makes the IDocumentSession available for injecting into your controllers.
    config.Connection(connectionString);  // connection string to connect to DB (don't hardcode connection strings. these are different by environment.)

}).UseLightweightSessions(); // Marten specific thing. don't need to get into it 



builder.Services.AddScoped<IValidator<CreateVendorRequest>, CreateVendorRequestValidator>();
builder.Services.AddScoped<IValidator<CreateVendorPointOfContactRequest>, CreateVendorPointOfContactRequestValidator>();
// it will give us a scoped service called IDocumentSession
// if this was Entity framework, it would give us a "DbContext" object we can use.


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
// request _____  response
// everything after this is configuring "middleware"
// that is is stuff that will intercept incoming our outgoing HTTP requests
// and process them in some way.
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication(); // when a request, look at the request to see if it is authenicated.
app.UseAuthorization(); /// when a request comes in, look to see if it's authorized.

app.MapControllers();  // go find all the controllers (have to be public class) and look at the attributes (HttpGet, etc)
// and create a cheat sheet

Console.WriteLine("Fixing to run your API");



app.MapCatalogItems();
// extension methods . 
// if I don't have the 'using', this won't work. 
// the IEendpointRouteBuilder in API.cs just makes it look like we are using the .. something

// then you can do things like this... 
    //if (app.Environment.IsDevelopment())
    //{
    //    app.MapCatalogItems;
    //}



app.Run(); // this is a "blocking method" basically a while(true) {... }
Console.WriteLine("done running your API");

public partial class Program;  // In .NET 10 you won't have to do this
