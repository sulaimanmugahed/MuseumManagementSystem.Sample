using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MuseumManagementSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Main");

            migrationBuilder.CreateTable(
                name: "ArtifactConditions",
                schema: "Main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NameInEnglish = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtifactConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArtifactTypes",
                schema: "Main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NameInEnglish = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtifactTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BioDegs",
                schema: "Main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NameInEnglish = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BioDegs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Material",
                schema: "Main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NameInEnglish = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stowages",
                schema: "Main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NameInEnglish = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stowages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimePeriods",
                schema: "Main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NameInEnglish = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimePeriods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Safes",
                schema: "Main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NameInEnglish = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StowageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Safes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Safes_Stowages_StowageId",
                        column: x => x.StowageId,
                        principalSchema: "Main",
                        principalTable: "Stowages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Artifacts",
                schema: "Main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NameInEnglish = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SerialNumber = table.Column<long>(type: "bigint", nullable: false),
                    OldMuseumNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NewMuseumNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Count = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Size = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    ImageLink = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BioDegId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TimePeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ArtifactTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ArtifactConditionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SafeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artifacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artifacts_ArtifactConditions_ArtifactConditionId",
                        column: x => x.ArtifactConditionId,
                        principalSchema: "Main",
                        principalTable: "ArtifactConditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Artifacts_ArtifactTypes_ArtifactTypeId",
                        column: x => x.ArtifactTypeId,
                        principalSchema: "Main",
                        principalTable: "ArtifactTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Artifacts_BioDegs_BioDegId",
                        column: x => x.BioDegId,
                        principalSchema: "Main",
                        principalTable: "BioDegs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Artifacts_Safes_SafeId",
                        column: x => x.SafeId,
                        principalSchema: "Main",
                        principalTable: "Safes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Artifacts_TimePeriods_TimePeriodId",
                        column: x => x.TimePeriodId,
                        principalSchema: "Main",
                        principalTable: "TimePeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArtifactImages",
                schema: "Main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ArtifactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtifactImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArtifactImages_Artifacts_ArtifactId",
                        column: x => x.ArtifactId,
                        principalSchema: "Main",
                        principalTable: "Artifacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtifactMaterials",
                schema: "Main",
                columns: table => new
                {
                    ArtifactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsImportantMaterial = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtifactMaterials", x => new { x.ArtifactId, x.MaterialId });
                    table.ForeignKey(
                        name: "FK_ArtifactMaterials_Artifacts_ArtifactId",
                        column: x => x.ArtifactId,
                        principalSchema: "Main",
                        principalTable: "Artifacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtifactMaterials_Material_MaterialId",
                        column: x => x.MaterialId,
                        principalSchema: "Main",
                        principalTable: "Material",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Main",
                table: "ArtifactConditions",
                columns: new[] { "Id", "Name", "NameInEnglish" },
                values: new object[] { new Guid("0c27f104-0e9e-462b-a009-e1238439102f"), "غير محدد", null });

            migrationBuilder.CreateIndex(
                name: "IX_ArtifactImages_ArtifactId",
                schema: "Main",
                table: "ArtifactImages",
                column: "ArtifactId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtifactMaterials_MaterialId",
                schema: "Main",
                table: "ArtifactMaterials",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Artifacts_ArtifactConditionId",
                schema: "Main",
                table: "Artifacts",
                column: "ArtifactConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Artifacts_ArtifactTypeId",
                schema: "Main",
                table: "Artifacts",
                column: "ArtifactTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Artifacts_BioDegId",
                schema: "Main",
                table: "Artifacts",
                column: "BioDegId");

            migrationBuilder.CreateIndex(
                name: "IX_Artifacts_SafeId",
                schema: "Main",
                table: "Artifacts",
                column: "SafeId");

            migrationBuilder.CreateIndex(
                name: "IX_Artifacts_TimePeriodId",
                schema: "Main",
                table: "Artifacts",
                column: "TimePeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Safes_StowageId",
                schema: "Main",
                table: "Safes",
                column: "StowageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtifactImages",
                schema: "Main");

            migrationBuilder.DropTable(
                name: "ArtifactMaterials",
                schema: "Main");

            migrationBuilder.DropTable(
                name: "Artifacts",
                schema: "Main");

            migrationBuilder.DropTable(
                name: "Material",
                schema: "Main");

            migrationBuilder.DropTable(
                name: "ArtifactConditions",
                schema: "Main");

            migrationBuilder.DropTable(
                name: "ArtifactTypes",
                schema: "Main");

            migrationBuilder.DropTable(
                name: "BioDegs",
                schema: "Main");

            migrationBuilder.DropTable(
                name: "Safes",
                schema: "Main");

            migrationBuilder.DropTable(
                name: "TimePeriods",
                schema: "Main");

            migrationBuilder.DropTable(
                name: "Stowages",
                schema: "Main");
        }
    }
}
