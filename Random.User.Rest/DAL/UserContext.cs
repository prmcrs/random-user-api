using Microsoft.EntityFrameworkCore;

namespace Random.User.Rest.Model
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<User>()
            //    .HasOne(ul => ul.Location)
            //    .WithOne(u => u.User)
            //    .HasForeignKey<Location>(b => b.IdUser);

            //modelBuilder.Entity<User>()
            //    .HasOne(a => a.Location).WithOne(b => b.User)
            //    .HasForeignKey<Location>(e => e.UserId);

            //modelBuilder.Entity<User>().ToTable("User");
            //modelBuilder.Entity<Location>().ToTable("User");
        }
    }
}
