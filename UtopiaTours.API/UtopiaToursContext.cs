using Microsoft.EntityFrameworkCore;
using UtopiaTours.Domain;

namespace UtopiaTours.API
{
    public class UtopiaToursContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=localhost;Database=Utopia;User=root;Password=root";
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 32)));
        }
    }
}
