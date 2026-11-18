using Microsoft.EntityFrameworkCore;

namespace Random.User.Rest.Model
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Location> Locations => Set<Location>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(user => user.Location)
                .WithOne(location => location.User)
                .HasForeignKey<Location>(location => location.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
