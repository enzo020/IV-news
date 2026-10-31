using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IVnews.Migrations
{
    /// <inheritdoc />
    public partial class SuportarMultiplasLocalizacoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Noticias_Localizacoes_LocalizacaoId",
                table: "Noticias");

            migrationBuilder.DropIndex(
                name: "IX_Noticias_LocalizacaoId",
                table: "Noticias");

            migrationBuilder.DropColumn(
                name: "LocalizacaoId",
                table: "Noticias");

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Localizacoes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NoticiaLocalizacao",
                columns: table => new
                {
                    NoticiaId = table.Column<int>(type: "INTEGER", nullable: false),
                    LocalizacaoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoticiaLocalizacao", x => new { x.NoticiaId, x.LocalizacaoId });
                    table.ForeignKey(
                        name: "FK_NoticiaLocalizacao_Localizacoes_LocalizacaoId",
                        column: x => x.LocalizacaoId,
                        principalTable: "Localizacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NoticiaLocalizacao_Noticias_NoticiaId",
                        column: x => x.NoticiaId,
                        principalTable: "Noticias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.Sql("""
                INSERT INTO NoticiaLocalizacao (NoticiaId, LocalizacaoId)
                SELECT Id, LocalizacaoId
                FROM Noticias
                WHERE LocalizacaoId IS NOT NULL;
            """);

            migrationBuilder.UpdateData(
                table: "Localizacoes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Tipo",
                value: null);

            migrationBuilder.UpdateData(
                table: "Localizacoes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Tipo",
                value: null);

            migrationBuilder.UpdateData(
                table: "Localizacoes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Tipo",
                value: null);

            migrationBuilder.UpdateData(
                table: "Localizacoes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Tipo",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_NoticiaLocalizacao_LocalizacaoId",
                table: "NoticiaLocalizacao",
                column: "LocalizacaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoticiaLocalizacao");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Localizacoes");

            migrationBuilder.AddColumn<int>(
                name: "LocalizacaoId",
                table: "Noticias",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Noticias_LocalizacaoId",
                table: "Noticias",
                column: "LocalizacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Noticias_Localizacoes_LocalizacaoId",
                table: "Noticias",
                column: "LocalizacaoId",
                principalTable: "Localizacoes",
                principalColumn: "Id");
        }
    }
}
