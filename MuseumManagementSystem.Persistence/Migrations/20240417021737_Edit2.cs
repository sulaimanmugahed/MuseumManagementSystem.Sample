using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MuseumManagementSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Edit2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Main",
                table: "ArtifactConditions",
                keyColumn: "Id",
                keyValue: new Guid("0c27f104-0e9e-462b-a009-e1238439102f"));

            migrationBuilder.DropColumn(
                name: "NameInEnglish",
                schema: "Main",
                table: "TimePeriods");

            migrationBuilder.DropColumn(
                name: "NameInEnglish",
                schema: "Main",
                table: "Stowages");

            migrationBuilder.DropColumn(
                name: "NameInEnglish",
                schema: "Main",
                table: "Safes");

            migrationBuilder.DropColumn(
                name: "NameInEnglish",
                schema: "Main",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "NameInEnglish",
                schema: "Main",
                table: "BioDegs");

            migrationBuilder.DropColumn(
                name: "NameInEnglish",
                schema: "Main",
                table: "ArtifactTypes");

            migrationBuilder.DropColumn(
                name: "NameInEnglish",
                schema: "Main",
                table: "Artifacts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Main",
                table: "ArtifactImages");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "Main",
                table: "ArtifactImages");

            migrationBuilder.DropColumn(
                name: "NameInEnglish",
                schema: "Main",
                table: "ArtifactConditions");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "Main",
                table: "Artifacts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameInEnglish",
                schema: "Main",
                table: "TimePeriods",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameInEnglish",
                schema: "Main",
                table: "Stowages",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameInEnglish",
                schema: "Main",
                table: "Safes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameInEnglish",
                schema: "Main",
                table: "Material",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameInEnglish",
                schema: "Main",
                table: "BioDegs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameInEnglish",
                schema: "Main",
                table: "ArtifactTypes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "Main",
                table: "Artifacts",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AddColumn<string>(
                name: "NameInEnglish",
                schema: "Main",
                table: "Artifacts",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Main",
                table: "ArtifactImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "Main",
                table: "ArtifactImages",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameInEnglish",
                schema: "Main",
                table: "ArtifactConditions",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.InsertData(
                schema: "Main",
                table: "ArtifactConditions",
                columns: new[] { "Id", "Name", "NameInEnglish" },
                values: new object[] { new Guid("0c27f104-0e9e-462b-a009-e1238439102f"), "غير محدد", null });
        }
    }
}
