using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;

namespace EmployeeManagment.Models
{
    public class ApDbcontext: IdentityDbContext<Appuser>
    {
        public ApDbcontext()
        {
        }

        public ApDbcontext(DbContextOptions<ApDbcontext> options)
            
            :base(options)
        {
        
        
        
        }
        // public DbSet<Appuser> appusers { get; set; }
        public DbSet<FullItem> FullItems { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<AddItem> AddItems { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Expenses> Expenses { get; set; }
  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
           //modelBuilder.seed();
            foreach (var ForeignKey  in modelBuilder.Model.
                GetEntityTypes().SelectMany(e =>e.GetForeignKeys()))
            {

                ForeignKey.DeleteBehavior= DeleteBehavior.Restrict;

            }

        }


       



    }
}
