using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnikLakDbContext.Configurations
{
    internal class ToolConfiguration : IEntityTypeConfiguration<Tool>
    {
        public void Configure(EntityTypeBuilder<Tool> builder)
        {
            builder.HasKey(t => t.Id);

            builder
                .HasMany(t => t.Appointments)
                .WithMany(a => a.ToolList);
        }
    }
}
