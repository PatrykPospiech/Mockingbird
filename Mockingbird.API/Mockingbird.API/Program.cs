using System.Net;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Mockingbird.API;
using Mockingbird.API.Database;
using Mockingbird.API.ReverseProxy;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CarrierContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL")));
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllerRoute(
    name: "configuration",
    pattern: "mockingbird/{controller}");

app.UseMiddleware<ProxyMiddleware>();

#region MockEndpoints

app.MapPost("mock/{endpoint}", (string endpoint) =>
{ 
    return DataSource.GetResponse(endpoint).Value;
});
app.MapGet("mock/{endpoint}", (string endpoint) => { return DataSource.GetResponse(endpoint).Value; });
app.MapPut("mock/{endpoint}", (string endpoint) => { return DataSource.GetResponse(endpoint).Value; });
app.MapDelete("mock/{endpoint}", (string endpoint) => { return DataSource.GetResponse(endpoint).Value; });

#endregion

app.Run();