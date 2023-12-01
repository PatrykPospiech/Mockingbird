using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
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

app.MapPost("{endpoint}", (string endpoint, HttpContext context) =>
{
    Console.WriteLine("Post mock endpoint executing....");
    var url = "https://dpdservicesdemo.dpd.com.pl/DPDPackageObjServicesService/DPDPackageObjServices";

    HttpRequest original = context.Request;
    HttpWebRequest newRequest = (HttpWebRequest)WebRequest.Create(url);

    newRequest.ContentType = original.ContentType;
    newRequest.Method = original.Method;

    //var originalStream = ReadFully(original.Body);
    Console.WriteLine("data updating....");

    // using (BinaryReader br = new BinaryReader(original.Body))
    // {
    //     originalStream = br.ReadBytes((int)original.Body.Length);
    // }


    //
    // Stream reqStream = newRequest.GetRequestStream();
    // reqStream.Write(originalStream, 0, originalStream.Length);
    // reqStream.Close();
    //
    //
    // Console.WriteLine("executing request....");
    // var response = newRequest.GetResponse();
    // Console.WriteLine("Response" + response);
    return DataSource.GetResponse(endpoint).Value;
});
app.MapGet("{endpoint}", (string endpoint) => { return DataSource.GetResponse(endpoint).Value; });
app.MapPut("{endpoint}", (string endpoint) => { return DataSource.GetResponse(endpoint).Value; });
app.MapDelete("{endpoint}", (string endpoint) => { return DataSource.GetResponse(endpoint).Value; });

#endregion

app.Run();



// static byte[] ReadFully(Stream input)
// {
//     using (MemoryStream ms = new MemoryStream())
//     {
//         input.CopyTo(ms);
//         return ms.ToArray();
//     }
// }