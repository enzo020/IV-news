using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IVnews.Migrations
{
    /// <inheritdoc />
    public partial class ExpandeCamposNoticiaCategoriaLocalizacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Autor",
                table: "Noticias",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Fonte",
                table: "Noticias",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagemUrl",
                table: "Noticias",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Pais",
                table: "Localizacoes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Categorias",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 1,
                column: "Descricao",
                value: "Notícias sobre política nacional e internacional");

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 2,
                column: "Descricao",
                value: "Notícias sobre esportes em geral");

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 3,
                column: "Descricao",
                value: "Notícias sobre tecnologia e inovação");

            migrationBuilder.UpdateData(
                table: "Localizacoes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Pais",
                value: "Brasil");

            migrationBuilder.UpdateData(
                table: "Localizacoes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Pais",
                value: "Brasil");

            migrationBuilder.UpdateData(
                table: "Noticias",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Autor", "Fonte", "ImagemUrl" },
                values: new object[] { "Redação IV News", "IV News", "https://via.placeholder.com/600x400" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Autor",
                table: "Noticias");

            migrationBuilder.DropColumn(
                name: "Fonte",
                table: "Noticias");

            migrationBuilder.DropColumn(
                name: "ImagemUrl",
                table: "Noticias");

            migrationBuilder.DropColumn(
                name: "Pais",
                table: "Localizacoes");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Categorias");
        }
    }
}
