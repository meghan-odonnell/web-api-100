using FluentValidation;
using Marten;
using SoftwareCenter.Api.Vendors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.




// .NET has a good way of doing the connection string.
// If there's no connection string, throw the exception
// checks Appsettings.json, then launchSettings.json.
var connectionString = builder.Configuration.GetConnectionString("db") ?? throw new Exception("Need a connection string.");
builder.Services.AddMarten(config =>
{
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();  // go find all the controllers (have to be public class) and look at the attributes (HttpGet, etc)
// and create a cheat sheet

app.Run();

public partial class Program;  // In .NET 10 you won't have to do this
