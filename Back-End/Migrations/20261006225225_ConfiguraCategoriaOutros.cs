using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IVnews.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguraCategoriaOutros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Descricao", "IdExterno", "Nome" },
                values: new object[] { 6, "Notícias sobre categorias variadas", null, "Outros" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
