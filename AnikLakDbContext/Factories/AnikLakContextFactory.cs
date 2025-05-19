using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AnikLakDbContext
{
    public class AnikLakContextFactory : IDesignTimeDbContextFactory<AnikLakContext>
    {
        public AnikLakContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AnikLakContext>();

            optionsBuilder.UseNpgsql(ConnectionSettings.ConnectionString);

            return new AnikLakContext(optionsBuilder.Options);
        }
    }
}
