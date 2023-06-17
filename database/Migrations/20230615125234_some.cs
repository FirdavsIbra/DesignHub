using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace database.Migrations
{
    /// <inheritdoc />
    public partial class some : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Designs",
                newName: "Style");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Designs",
                newName: "Form");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Designs",
                newName: "Font");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Designs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Designs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LogoConcept",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uniqueness",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Sketches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sketches", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sketches");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Designs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Designs");

            migrationBuilder.DropColumn(
                name: "LogoConcept",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Uniqueness",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "Style",
                table: "Designs",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Form",
                table: "Designs",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "Font",
                table: "Designs",
                newName: "Description");
        }
    }
}
