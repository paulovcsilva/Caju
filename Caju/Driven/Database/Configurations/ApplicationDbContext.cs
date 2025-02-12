using Caju.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Caju.Driven.Database.Configurations
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Balance> Balances { get; set; }
        public virtual DbSet<Merchant> Merchants { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplicationDbContextModelBuilder.Configure(modelBuilder);
        }
    }
}
