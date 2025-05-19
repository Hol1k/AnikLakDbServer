using AnikLakDbContext.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AnikLakDbContext
{
    public class AnikLakContext : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; } = null!;
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Tool> Tools { get; set; } = null!;
        public DbSet<Material> Materials { get; set; } = null!;

        public AnikLakContext(DbContextOptions<AnikLakContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionSettings.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialConfiguration());
            modelBuilder.ApplyConfiguration(new ToolConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
