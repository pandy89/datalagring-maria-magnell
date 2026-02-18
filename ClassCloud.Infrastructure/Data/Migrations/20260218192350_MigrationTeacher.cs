using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassCloud.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigrationTeacher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Teachers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "UQ_Teachers_Email",
                table: "Teachers",
                column: "Email",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "UQ_Teachers_Email_NotEmpty",
                table: "Teachers",
                sql: "LTRIM(RTRIM([Email])) <> ''");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_Teachers_Email",
                table: "Teachers");

            migrationBuilder.DropCheckConstraint(
                name: "UQ_Teachers_Email_NotEmpty",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Teachers");
        }
    }
}
