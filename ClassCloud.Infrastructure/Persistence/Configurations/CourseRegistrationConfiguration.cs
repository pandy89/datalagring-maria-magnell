using ClassCloud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassCloud.Infrastructure.Persistence.Configurations;

public class CourseRegistrationConfiguration : IEntityTypeConfiguration<CourseRegistrationEntity>
{
    public void Configure(EntityTypeBuilder<CourseRegistrationEntity> builder)
    {
        // Primary key
        builder.HasKey(e => e.Id)
            .HasName("PK_CourseRegistrations_Id");

        builder.Property(e => e.RegistrationDate)
            .HasColumnType("datetime2(0)")
            .HasDefaultValue("(SYSUTCDATETIME())", "DF_CourseRegistrations_RegistrationDate");

        builder.Property(e => e.UpdatedAtUtc)
            .HasColumnType("datetime2(0)")
            .HasDefaultValue("(SYSUTCDATETIME())", "DF_CourseRegistrations_UpdatedAtUtc");

        builder.Property(e => e.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        //Relation
        builder.HasOne(cr => cr.Participant)
            .WithMany(cs => cs.CourseRegistrations)
            .HasForeignKey(cr => cr.ParticipantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(cr => cr.CourseSession)
            .WithMany(cs => cs.CourseRegistrations)
            .HasForeignKey(cr => cr.CourseSessionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(cr => cr.CourseStatus)
            .WithMany(cs => cs.CourseRegistrations)
            .HasForeignKey(cr => cr.CourseStatusId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.Property(e => e.RowVersion)
            .IsRowVersion();
    }
}
