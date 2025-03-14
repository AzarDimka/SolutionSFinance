using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SFinance.Data.Migrations
{
    /// <inheritdoc />
    public partial class initil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Handbook",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameHandbook = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    tableName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    request = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    keyField = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    selectionField = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    width = table.Column<double>(type: "float", nullable: false),
                    height = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Handbook", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "dbo",
                columns: table => new
                {
                    IdProduct = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameProduct = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.IdProduct);
                });

            migrationBuilder.CreateTable(
                name: "TypeData",
                schema: "dbo",
                columns: table => new
                {
                    idType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeData", x => x.idType);
                });

            migrationBuilder.CreateTable(
                name: "Field",
                schema: "dbo",
                columns: table => new
                {
                    idField = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idHandbook = table.Column<int>(type: "int", nullable: false),
                    indexField = table.Column<int>(type: "int", nullable: false),
                    nameToQuery = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    nameVisible = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    idTypeData = table.Column<int>(type: "int", nullable: false),
                    refHandbookToField = table.Column<int>(type: "int", nullable: true),
                    isVisible = table.Column<bool>(type: "bit", nullable: false),
                    isEdit = table.Column<bool>(type: "bit", nullable: false),
                    isNull = table.Column<bool>(type: "bit", nullable: false),
                    isCheckDuplicate = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Field", x => x.idField);
                    table.ForeignKey(
                        name: "FK_Field_Handbook_idHandbook",
                        column: x => x.idHandbook,
                        principalSchema: "dbo",
                        principalTable: "Handbook",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Field_TypeData_idTypeData",
                        column: x => x.idTypeData,
                        principalSchema: "dbo",
                        principalTable: "TypeData",
                        principalColumn: "idType",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Field_idHandbook",
                schema: "dbo",
                table: "Field",
                column: "idHandbook");

            migrationBuilder.CreateIndex(
                name: "IX_Field_idTypeData",
                schema: "dbo",
                table: "Field",
                column: "idTypeData");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Field",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Handbook",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TypeData",
                schema: "dbo");
        }
    }
}
