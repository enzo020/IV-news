using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IVnews.Migrations
{
    /// <inheritdoc />
    public partial class NovasCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Descricao", "IdExterno", "Nome" },
                values: new object[,]
                {
                    { 7, "Notícias sobre negócios, trading e empreendedorismo", null, "Negócios" },
                    { 8, "Notícias sobre o mundo acadêmico, cursos e pesquisas", null, "Educação" },
                    { 9, "Notícias sobre Arte, Música e eventos culturais", null, "Cultura" },
                    { 10, "Notícias sobre crimes e artigos policiais", null, "Segurança" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
