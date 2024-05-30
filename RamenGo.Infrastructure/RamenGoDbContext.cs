using Microsoft.EntityFrameworkCore;
using RamenGo.Domain.Entities;

namespace RamenGo.Infrastructure
{
    public class RamenGoDbContext : DbContext
    {
        public RamenGoDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Broth> Broths { get; set; }

        public DbSet<Protein> Proteins { get; set; }
        
        public DbSet<Order> Orders { get; set; }
    }
}
