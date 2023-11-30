using Microsoft.EntityFrameworkCore;
using Mockingbird.API;
using Mockingbird.API.Database;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CarrierContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL")));
builder.Services.AddControllers();

var app = builder.Build();

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetService<CarrierContext>();
context.Database.Migrate();
    
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
