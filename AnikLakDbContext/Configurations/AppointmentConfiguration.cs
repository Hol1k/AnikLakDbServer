using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnikLakDbContext.Configurations
{
    internal class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.Id);

            builder
                .HasOne(a => a.Client)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.ClientId);

            builder
                .HasMany(a => a.ToolList)
                .WithMany(t => t.Appointments);

            builder
                .HasMany(a => a.MaterialList)
                .WithMany(m => m.Appointments);
        }
    }
}
