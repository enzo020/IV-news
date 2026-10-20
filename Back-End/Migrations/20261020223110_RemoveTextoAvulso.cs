using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IVnews.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTextoAvulso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Texto",
                table: "Resumos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Texto",
                table: "Resumos",
                type: "TEXT",
                nullable: true);
        }
    }
}
