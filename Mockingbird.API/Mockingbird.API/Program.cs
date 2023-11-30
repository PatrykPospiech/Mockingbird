using Microsoft.EntityFrameworkCore;
using Mockingbird.API;
using Mockingbird.API.Database;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CarrierContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL")));
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllerRoute(
    name: "configuration",
    pattern: "mockingbird/{controller}");

#region MockEndpoints

app.MapPost("{endpoint}", (string endpoint) => { return DataSource.GetResponse(endpoint).Value; });
app.MapGet("{endpoint}", (string endpoint) => { return DataSource.GetResponse(endpoint).Value; });
app.MapPut("{endpoint}", (string endpoint) => { return DataSource.GetResponse(endpoint).Value; });
app.MapDelete("{endpoint}", (string endpoint) => { return DataSource.GetResponse(endpoint).Value; });

#endregion

app.Run();
