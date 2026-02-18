using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassCloud.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CourseDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(SYSUTCDATETIME())")
                        .Annotation("Relational:DefaultConstraintName", "DF_Courses_CreatedAtUtc"),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(SYSUTCDATETIME())")
                        .Annotation("Relational:DefaultConstraintName", "DF_Courses_UpdatedAtUtc"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStatus_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(SYSUTCDATETIME())")
                        .Annotation("Relational:DefaultConstraintName", "DF_Locations_CreatedAtUtc"),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(SYSUTCDATETIME())")
                        .Annotation("Relational:DefaultConstraintName", "DF_Locations_UpdatedAtUtc"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(SYSUTCDATETIME())")
                        .Annotation("Relational:DefaultConstraintName", "DF_Participants_CreatedAtUtc"),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(SYSUTCDATETIME())")
                        .Annotation("Relational:DefaultConstraintName", "DF_Participants_UpdatedAtUtc"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants_Id", x => x.Id);
                    table.CheckConstraint("UQ_Participants_Email_NotEmpty", "LTRIM(RTRIM([Email])) <> ''");
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Expertise = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(SYSUTCDATETIME())")
                        .Annotation("Relational:DefaultConstraintName", "DF_Teachers_CreatedAtUtc"),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(SYSUTCDATETIME())")
                        .Annotation("Relational:DefaultConstraintName", "DF_Teachers_UpdatedAtUtc"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(SYSUTCDATETIME())")
                        .Annotation("Relational:DefaultConstraintName", "DF_CourseSessions_StartDate"),
                    EndDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(SYSUTCDATETIME())")
                        .Annotation("Relational:DefaultConstraintName", "DF_CourseSessions_EndDate"),
                    MaxParticipants = table.Column<int>(type: "int", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(SYSUTCDATETIME())")
                        .Annotation("Relational:DefaultConstraintName", "DF_CourseSessions_CreatedAtUtc"),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(SYSUTCDATETIME())")
                        .Annotation("Relational:DefaultConstraintName", "DF_CourseSessions_UpdatedAtUtc"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsCanceled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSessions_Id", x => x.Id);
                    table.CheckConstraint("CK_CourseSession_MaxParticipants", "[MaxParticipants] > 0");
                    table.ForeignKey(
                        name: "FK_CourseSessions_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseSessions_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseRegistrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(SYSUTCDATETIME())")
                        .Annotation("Relational:DefaultConstraintName", "DF_CourseRegistrations_RegistrationDate"),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(SYSUTCDATETIME())")
                        .Annotation("Relational:DefaultConstraintName", "DF_CourseRegistrations_UpdatedAtUtc"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ParticipantId = table.Column<int>(type: "int", nullable: false),
                    CourseSessionId = table.Column<int>(type: "int", nullable: false),
                    CourseStatusId = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseRegistrations_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseRegistrations_CourseSessions_CourseSessionId",
                        column: x => x.CourseSessionId,
                        principalTable: "CourseSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseRegistrations_CourseStatus_CourseStatusId",
                        column: x => x.CourseStatusId,
                        principalTable: "CourseStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseRegistrations_Participants_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherCourseSessions",
                columns: table => new
                {
                    CourseSessionId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherCourseSessions_Id", x => new { x.TeacherId, x.CourseSessionId });
                    table.ForeignKey(
                        name: "FK_TeacherCourseSessions_CourseSessions_CourseSessionId",
                        column: x => x.CourseSessionId,
                        principalTable: "CourseSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherCourseSessions_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseRegistrations_CourseSessionId",
                table: "CourseRegistrations",
                column: "CourseSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRegistrations_CourseStatusId",
                table: "CourseRegistrations",
                column: "CourseStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRegistrations_ParticipantId",
                table: "CourseRegistrations",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "UQ_Courses_CourseCode",
                table: "Courses",
                column: "CourseCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseSessions_CourseId",
                table: "CourseSessions",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSessions_LocationId",
                table: "CourseSessions",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "UQ_Participants_Email",
                table: "Participants",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCourseSessions_CourseSessionId",
                table: "TeacherCourseSessions",
                column: "CourseSessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseRegistrations");

            migrationBuilder.DropTable(
                name: "TeacherCourseSessions");

            migrationBuilder.DropTable(
                name: "CourseStatus");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "CourseSessions");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
