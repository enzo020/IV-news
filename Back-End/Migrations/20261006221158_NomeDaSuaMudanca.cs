using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IVnews.Migrations
{
    /// <inheritdoc />
    public partial class NomeDaSuaMudanca : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Resumos",
                keyColumn: "Id",
                keyValue: 1);

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
                keyValue: 1);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Noticias",
                columns: new[] { "Id", "Autor", "CategoriaId", "Conteudo", "Fonte", "IdExterno", "Idioma", "ImagemUrl", "LocalizacaoId", "PublicadoEm", "Titulo", "UrlNoticia" },
                values: new object[,]
                {
                    { 1, "Redação IV News", 3, "Conteúdo de exemplo pra testar o banco.", "IV News", null, null, "https://picsum.photos/600/400?random=5", 1, new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Notícia de teste", "https://ivnews.com/noticia-teste" },
                    { 2, "Ana Silva", 1, "Candidatos iniciam campanhas em todo o estado.", "IV News", null, null, "https://picsum.photos/600/400?random=4", 2, new DateTime(2026, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eleições municipais se aproximam", "https://ivnews.com/eleicoes-municipais" },
                    { 3, "Carlos Souza", 2, "Vitória histórica na final do estadual.", "IV News", null, null, "https://picsum.photos/600/400?random=1", 3, new DateTime(2026, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Time local vence campeonato", "https://ivnews.com/time-vence-campeonato" },
                    { 4, "Beatriz Lima", 3, "Startup lança ferramenta de saúde com inteligência artificial.", "IV News", null, null, "https://picsum.photos/600/400?random=2", 4, new DateTime(2026, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nova IA promete revolucionar diagnósticos", "https://ivnews.com/nova-ia-diagnosticos" },
                    { 5, "Rafael Costa", 5, "Bolsa de valores tem alta após anúncio do governo.", "IV News", null, null, "https://picsum.photos/600/400?random=3", 1, new DateTime(2026, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mercado financeiro reage a nova taxa", "https://ivnews.com/mercado-financeiro-taxa" }
                });

            migrationBuilder.InsertData(
                table: "Resumos",
                columns: new[] { "Id", "NoticiaId", "Texto" },
                values: new object[,]
                {
                    { 1, 1, "Resumo de exemplo da notícia de teste." },
                    { 2, 2, "Candidatos disputam eleições municipais em todo o estado." },
                    { 3, 3, "Time conquista título estadual em final emocionante." },
                    { 4, 4, "Startup inova com IA aplicada à saúde." },
                    { 5, 5, "Governo anuncia nova taxa e mercado reage positivamente." }
                });
        }
    }
}
