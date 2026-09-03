using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IVnews.Migrations
{
    /// <inheritdoc />
    public partial class SeedDadosTeste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Política" },
                    { 2, "Esporte" },
                    { 3, "Tecnologia" }
                });

            migrationBuilder.InsertData(
                table: "Localizacoes",
                columns: new[] { "Id", "Cidade", "Estado" },
                values: new object[,]
                {
                    { 1, "Maringá", "PR" },
                    { 2, "São Paulo", "SP" }
                });

            migrationBuilder.InsertData(
                table: "Noticias",
                columns: new[] { "Id", "CategoriaId", "Conteudo", "LocalizacaoId", "PublicadoEm", "Titulo" },
                values: new object[] { 1, 3, "Conteúdo de exemplo pra testar o banco.", 1, new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Notícia de teste" });

            migrationBuilder.InsertData(
                table: "Resumos",
                columns: new[] { "Id", "NoticiaId", "Texto" },
                values: new object[] { 1, 1, "Resumo de exemplo da notícia de teste." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Localizacoes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Resumos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Noticias",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Localizacoes",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
