using Mockingbird.API;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

#region MockEndpoints

app.MapPost("{endpoint}", (string endpoint) => { return DataSource.GetResponse(endpoint).Value; });
app.MapGet("{endpoint}", (string endpoint) => { return DataSource.GetResponse(endpoint).Value; });
app.MapPut("{endpoint}", (string endpoint) => { return DataSource.GetResponse(endpoint).Value; });
app.MapDelete("{endpoint}", (string endpoint) => { return DataSource.GetResponse(endpoint).Value; });

#endregion


app.Run();
