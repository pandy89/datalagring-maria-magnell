using ClassCloud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassCloud.Infrastructure.Persistence.Configurations;

public class ParticipantConfiguration : IEntityTypeConfiguration<ParticipantEntity>
{
    public void Configure(EntityTypeBuilder<ParticipantEntity> builder)
    {
        // Primary key
        builder.HasKey(e => e.Id)
            .HasName("PK_Participants_Id");

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.PhoneNumber)
            .HasMaxLength(50)
            .IsUnicode(false)
            .IsRequired(false);

        builder.Property(e => e.CreatedAtUtc)
            .HasColumnType("datetime2(0)")
            .HasDefaultValue("(SYSUTCDATETIME())", "DF_Participants_CreatedAtUtc")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.UpdatedAtUtc)
            .HasColumnType("datetime2(0)")
            .HasDefaultValue("(SYSUTCDATETIME())", "DF_Participants_UpdatedAtUtc")
            .ValueGeneratedOnAddOrUpdate();

        builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false);

        builder.Property(e => e.RowVersion)
            .IsRowVersion();


        builder.HasIndex(e => e.Email, "UQ_Participants_Email").IsUnique();


        builder.ToTable(tb => tb.HasCheckConstraint("UQ_Participants_Email_NotEmpty", "LTRIM(RTRIM([Email])) <> ''"));
    }
}
