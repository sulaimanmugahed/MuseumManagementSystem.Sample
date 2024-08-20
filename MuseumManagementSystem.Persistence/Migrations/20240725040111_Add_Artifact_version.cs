using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MuseumManagementSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Artifact_version : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Version",
                schema: "Main",
                table: "Artifacts",
                type: "bigint",
                rowVersion: true,
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                schema: "Main",
                table: "Artifacts");
        }
    }
}
