using System.Reflection;
using IFCE.AutoGate.Domain.Entities;
using IFCE.AutoGate.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IFCE.AutoGate.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Administrator> Administrators { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.ApplyForeignKeysBehavior();

        base.OnModelCreating(builder);
    }
}
