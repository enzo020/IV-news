using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IVnews.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCategoriaResumo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Localizacoes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Localizacoes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Localizacoes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Localizacoes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Resumos");

            migrationBuilder.CreateIndex(
                name: "IX_Localizacoes_Cidade_Pais_Tipo",
                table: "Localizacoes",
                columns: new[] { "Cidade", "Pais", "Tipo" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Localizacoes_Cidade_Pais_Tipo",
                table: "Localizacoes");

            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "Resumos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Localizacoes",
                columns: new[] { "Id", "Cidade", "Estado", "Latitude", "Longitude", "Pais", "Tipo" },
                values: new object[,]
                {
                    { 1, "Maringá", "PR", null, null, "Brasil", null },
                    { 2, "São Paulo", "SP", null, null, "Brasil", null },
                    { 3, "Curitiba", "PR", null, null, "Brasil", null },
                    { 4, "Rio de Janeiro", "RJ", null, null, "Brasil", null }
                });
        }
    }
}
