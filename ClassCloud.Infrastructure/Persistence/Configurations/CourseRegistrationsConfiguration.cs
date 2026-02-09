using ClassCloud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassCloud.Infrastructure.Persistence.Configurations;

public class CourseRegistrationsConfiguration : IEntityTypeConfiguration<CourseRegistrationsEntity>
{
    public void Configure(EntityTypeBuilder<CourseRegistrationsEntity> builder)
    {
        builder.HasKey(e => e.Id)
        .HasName("PK_CourseRegistration_Id");

        builder.Property(e => e.RegistrationDate)
            .HasColumnType("datetime2(0)")
            .HasDefaultValue("(SYSUTCDATETIME())", "DF_CourseRegistration_RegistrationDate");

        builder.Property(e => e.UpdatedAtUtc)
            .HasColumnType("datetime2(0)")
            .HasDefaultValue("(SYSUTCDATETIME())", "DF_CourseRegistration_UpdatedAtUtc");

        builder.Property(e => e.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasKey(e => e.ParticipantId);

        builder.HasKey(e => e.CourseSessionId);

        builder.HasKey(e => e.CourseStatusId);

        builder.Property(e => e.RowVersion)
            .IsRowVersion();
    }
}
