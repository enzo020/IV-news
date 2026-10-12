using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IVnews.Migrations
{
    /// <inheritdoc />
    public partial class AddResultadosIAResumo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "Resumos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PontosChave",
                table: "Resumos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResumoExecutivo",
                table: "Resumos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sentimento",
                table: "Resumos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "Resumos",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Resumos");

            migrationBuilder.DropColumn(
                name: "PontosChave",
                table: "Resumos");

            migrationBuilder.DropColumn(
                name: "ResumoExecutivo",
                table: "Resumos");

            migrationBuilder.DropColumn(
                name: "Sentimento",
                table: "Resumos");

            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "Resumos");
        }
    }
}
