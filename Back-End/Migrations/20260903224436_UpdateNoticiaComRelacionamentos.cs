using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IVnews.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNoticiaComRelacionamentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Classificacao",
                table: "Noticias");

            migrationBuilder.RenameColumn(
                name: "Resumo",
                table: "Noticias",
                newName: "Conteudo");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Noticias",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocalizacaoId",
                table: "Noticias",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Noticias_CategoriaId",
                table: "Noticias",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Noticias_LocalizacaoId",
                table: "Noticias",
                column: "LocalizacaoId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Noticias_Categorias_CategoriaId",
                table: "Noticias");

            migrationBuilder.DropForeignKey(
                name: "FK_Noticias_Localizacoes_LocalizacaoId",
                table: "Noticias");

            migrationBuilder.DropIndex(
                name: "IX_Noticias_CategoriaId",
                table: "Noticias");

            migrationBuilder.DropIndex(
                name: "IX_Noticias_LocalizacaoId",
                table: "Noticias");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Noticias");

            migrationBuilder.DropColumn(
                name: "LocalizacaoId",
                table: "Noticias");

            migrationBuilder.RenameColumn(
                name: "Conteudo",
                table: "Noticias",
                newName: "Resumo");

            migrationBuilder.AddColumn<string>(
                name: "Classificacao",
                table: "Noticias",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
