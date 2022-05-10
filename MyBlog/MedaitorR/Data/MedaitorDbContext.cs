using Microsoft.EntityFrameworkCore;

namespace MedaitorR.Data
{
    public partial class MedaitorDbContext:DbContext
    {
        public MedaitorDbContext(DbContextOptions<MedaitorDbContext> options):base(options)
        {

        }
        public DbSet<Customer>? Customers{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer { ID = 1, Name = "Cust1" },
                new Customer { ID = 2, Name = "Cust2" },
                new Customer { ID = 3, Name = "Cust3" }
                );

            base.OnModelCreating(modelBuilder);
        }

    }
}
