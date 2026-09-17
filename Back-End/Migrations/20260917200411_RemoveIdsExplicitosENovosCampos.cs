using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IVnews.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIdsExplicitosENovosCampos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Noticias_Categorias_CategoriaId",
                table: "Noticias");

            migrationBuilder.DropForeignKey(
                name: "FK_Noticias_Localizacoes_LocalizacaoId",
                table: "Noticias");

            migrationBuilder.AlterColumn<int>(
                name: "LocalizacaoId",
                table: "Noticias",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaId",
                table: "Noticias",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "IdExterno",
                table: "Noticias",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlNoticia",
                table: "Noticias",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Noticias",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IdExterno", "UrlNoticia" },
                values: new object[] { null, "https://ivnews.com/noticia-teste" });

            migrationBuilder.UpdateData(
                table: "Noticias",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IdExterno", "UrlNoticia" },
                values: new object[] { null, "https://ivnews.com/eleicoes-municipais" });

            migrationBuilder.UpdateData(
                table: "Noticias",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IdExterno", "UrlNoticia" },
                values: new object[] { null, "https://ivnews.com/time-vence-campeonato" });

            migrationBuilder.UpdateData(
                table: "Noticias",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "IdExterno", "UrlNoticia" },
                values: new object[] { null, "https://ivnews.com/nova-ia-diagnosticos" });

            migrationBuilder.UpdateData(
                table: "Noticias",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "IdExterno", "UrlNoticia" },
                values: new object[] { null, "https://ivnews.com/mercado-financeiro-taxa" });

            migrationBuilder.AddForeignKey(
                name: "FK_Noticias_Categorias_CategoriaId",
                table: "Noticias",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Noticias_Localizacoes_LocalizacaoId",
                table: "Noticias",
                column: "LocalizacaoId",
                principalTable: "Localizacoes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Noticias_Categorias_CategoriaId",
                table: "Noticias");

            migrationBuilder.DropForeignKey(
                name: "FK_Noticias_Localizacoes_LocalizacaoId",
                table: "Noticias");

            migrationBuilder.DropColumn(
                name: "IdExterno",
                table: "Noticias");

            migrationBuilder.DropColumn(
                name: "UrlNoticia",
                table: "Noticias");

            migrationBuilder.AlterColumn<int>(
                name: "LocalizacaoId",
                table: "Noticias",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaId",
                table: "Noticias",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Noticias_Categorias_CategoriaId",
                table: "Noticias",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Noticias_Localizacoes_LocalizacaoId",
                table: "Noticias",
                column: "LocalizacaoId",
                principalTable: "Localizacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
