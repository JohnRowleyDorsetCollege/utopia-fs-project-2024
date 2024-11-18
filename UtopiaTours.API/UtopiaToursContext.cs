using Microsoft.EntityFrameworkCore;
using UtopiaTours.Domain;

namespace UtopiaTours.API
{
    public class UtopiaToursContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

           
            string connectionString = "Server=localhost;Database=UtopiaTours;User=root;Password=root";
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 32)));
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           foreach(var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName(entityType.DisplayName());
            }
         
        //   modelBuilder.Entity<Booking>().HasOne(b=>b.Schedule).WithMany(s=>s.Bookings).HasForeignKey(b=>b.ScheduleId);



          //  base.OnModelCreating(modelBuilder); 
        }
    }
}
