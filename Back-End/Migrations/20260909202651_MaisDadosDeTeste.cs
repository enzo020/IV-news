using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IVnews.Migrations
{
    /// <inheritdoc />
    public partial class MaisDadosDeTeste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Descricao", "Nome" },
                values: new object[,]
                {
                    { 4, "Notícias sobre saúde e bem-estar", "Saúde" },
                    { 5, "Notícias sobre economia e mercado financeiro", "Economia" }
                });

            migrationBuilder.InsertData(
                table: "Localizacoes",
                columns: new[] { "Id", "Cidade", "Estado", "Pais" },
                values: new object[,]
                {
                    { 3, "Curitiba", "PR", "Brasil" },
                    { 4, "Rio de Janeiro", "RJ", "Brasil" }
                });

            migrationBuilder.InsertData(
                table: "Noticias",
                columns: new[] { "Id", "Autor", "CategoriaId", "Conteudo", "Fonte", "ImagemUrl", "LocalizacaoId", "PublicadoEm", "Titulo" },
                values: new object[,]
                {
                    { 2, "Ana Silva", 1, "Candidatos iniciam campanhas em todo o estado.", "IV News", "https://via.placeholder.com/600x400", 2, new DateTime(2026, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eleições municipais se aproximam" },
                    { 3, "Carlos Souza", 2, "Vitória histórica na final do estadual.", "IV News", "https://via.placeholder.com/600x400", 3, new DateTime(2026, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Time local vence campeonato" },
                    { 4, "Beatriz Lima", 3, "Startup lança ferramenta de saúde com inteligência artificial.", "IV News", "https://via.placeholder.com/600x400", 4, new DateTime(2026, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nova IA promete revolucionar diagnósticos" },
                    { 5, "Rafael Costa", 5, "Bolsa de valores tem alta após anúncio do governo.", "IV News", "https://via.placeholder.com/600x400", 1, new DateTime(2026, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mercado financeiro reage a nova taxa" }
                });

            migrationBuilder.InsertData(
                table: "Resumos",
                columns: new[] { "Id", "NoticiaId", "Texto" },
                values: new object[,]
                {
                    { 2, 2, "Candidatos disputam eleições municipais em todo o estado." },
                    { 3, 3, "Time conquista título estadual em final emocionante." },
                    { 4, 4, "Startup inova com IA aplicada à saúde." },
                    { 5, 5, "Governo anuncia nova taxa e mercado reage positivamente." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Resumos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Resumos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Resumos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Resumos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Noticias",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Noticias",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Noticias",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Noticias",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Localizacoes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Localizacoes",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
