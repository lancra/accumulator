using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accumulator.Migrations;

/// <inheritdoc />
public partial class AddItemDefinitions : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "ItemDefinitions",
            columns: table => new
            {
                Code = table.Column<string>(type: "TEXT"),
                Type = table.Column<int>(type: "INTEGER"),
                Name = table.Column<string>(type: "TEXT"),
                GradeType = table.Column<int>(type: "INTEGER"),
                Version = table.Column<byte>(type: "INTEGER"),
                IsStackable = table.Column<bool>(type: "INTEGER"),
                Height = table.Column<int>(type: "INTEGER"),
                Width = table.Column<int>(type: "INTEGER"),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ItemDefinitions", x => x.Code);
            });

        migrationBuilder.CreateTable(
            name: "ItemDefinitionGrades",
            columns: table => new
            {
                ParentCode = table.Column<string>(type: "TEXT"),
                Code = table.Column<string>(type: "TEXT"),
                Type = table.Column<int>(type: "INTEGER"),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ItemDefinitionGrades", x => new { x.ParentCode, x.Code });
                table.ForeignKey(
                    name: "FK_ItemDefinitionGrades_ItemDefinitions_Code",
                    column: x => x.Code,
                    principalTable: "ItemDefinitions",
                    principalColumn: "Code",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_ItemDefinitionGrades_ItemDefinitions_ParentCode",
                    column: x => x.ParentCode,
                    principalTable: "ItemDefinitions",
                    principalColumn: "Code",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_ItemDefinitionGrades_Code",
            table: "ItemDefinitionGrades",
            column: "Code");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "ItemDefinitionGrades");

        migrationBuilder.DropTable(
            name: "ItemDefinitions");
    }
}
