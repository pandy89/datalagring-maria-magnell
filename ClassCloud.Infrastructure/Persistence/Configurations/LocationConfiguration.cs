using ClassCloud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassCloud.Infrastructure.Persistence.Configurations;

public class LocationConfiguration : IEntityTypeConfiguration<LocationEntity>
{
    public void Configure(EntityTypeBuilder<LocationEntity> builder)
    {
        // Primary key
        builder.HasKey(e => e.Id)
            .HasName("PK_Locations_Id");

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.CreatedAtUtc)
            .HasColumnType("datetime2(0)")
            .HasDefaultValueSql("(SYSUTCDATETIME())", "DF_Locations_CreatedAtUtc")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.UpdatedAtUtc)
            .HasColumnType("datetime2(0)")
            .HasDefaultValueSql("(SYSUTCDATETIME())", "DF_Locations_UpdatedAtUtc")
            .ValueGeneratedOnAdd(); 

        builder.Property(e => e.RowVersion)
            .IsRowVersion();
    }
}

