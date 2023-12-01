using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Mockingbird.API;
using Mockingbird.API.Database;
using Mockingbird.API.Logic;
using Mockingbird.API.ReverseProxy;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<CarrierContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL")));
builder.Services.AddDbContext<CarrierContext>(options => options.UseInMemoryDatabase("Test"));
builder.Services.AddControllers();
builder.Services.AddScoped<MiddlewareReqResCache>();

var app = builder.Build();

// var scope = app.Services.CreateScope();
// var context = scope.ServiceProvider.GetService<CarrierContext>();
// context.Database.Migrate();

app.MapControllerRoute(
    name: "configuration",
    pattern: "{controller}");

app.UseMiddleware<ProxyMiddleware>();

#region MockEndpoints

app.MapPost("mock/{configurationId}/{endpoint}", (string endpoint) =>
{ 
    return DataSource.GetResponse(endpoint).Value;
});
app.MapGet("mock/{configurationId}/{endpoint}", (string endpoint) => { return DataSource.GetResponse(endpoint).Value; });
app.MapPut("mock/{configurationId}/{endpoint}", (string endpoint) => { return DataSource.GetResponse(endpoint).Value; });
app.MapDelete("mock/{configurationId}/{endpoint}", (string endpoint) => { return DataSource.GetResponse(endpoint).Value; });

#endregion

app.Run();