using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AutomotiveCatalog.Models;

namespace AutomotiveCatalog.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AutomotiveCatalog.Models.Vehicles> Vehiclecs { get; set; } = default!;
        public DbSet<AutomotiveCatalog.Models.AutoParts> AutoParts { get; set; } = default!;
    }
}