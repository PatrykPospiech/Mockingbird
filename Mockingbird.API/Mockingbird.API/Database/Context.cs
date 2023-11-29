using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Mockingbird.API.Database;

public class CarrierContext : DbContext
{
    public DbSet<Carrier> Carriers { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<ApiResource> ApiResources { get; set; }
    public DbSet<Method> Method { get; set; }
    public DbSet<Response> Responses { get; set; }
    public DbSet<Header> Headers { get; set; }

    public CarrierContext(DbContextOptions<CarrierContext> options) : base(options) { }
}