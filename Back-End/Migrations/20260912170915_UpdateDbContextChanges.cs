using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IVnews.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDbContextChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Texto",
                table: "Resumos",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Noticias",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "ImagemUrl",
                table: "Noticias",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Fonte",
                table: "Noticias",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Conteudo",
                table: "Noticias",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Autor",
                table: "Noticias",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Pais",
                table: "Localizacoes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Localizacoes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Cidade",
                table: "Localizacoes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Categorias",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Categorias",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.UpdateData(
                table: "Noticias",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImagemUrl",
                value: "https://picsum.photos/600/400?random=5");

            migrationBuilder.UpdateData(
                table: "Noticias",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImagemUrl",
                value: "https://picsum.photos/600/400?random=4");

            migrationBuilder.UpdateData(
                table: "Noticias",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImagemUrl",
                value: "https://picsum.photos/600/400?random=1");

            migrationBuilder.UpdateData(
                table: "Noticias",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImagemUrl",
                value: "https://picsum.photos/600/400?random=2");

            migrationBuilder.UpdateData(
                table: "Noticias",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImagemUrl",
                value: "https://picsum.photos/600/400?random=3");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Texto",
                table: "Resumos",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Noticias",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImagemUrl",
                table: "Noticias",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Fonte",
                table: "Noticias",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Conteudo",
                table: "Noticias",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Autor",
                table: "Noticias",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Pais",
                table: "Localizacoes",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Localizacoes",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cidade",
                table: "Localizacoes",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Categorias",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Categorias",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Noticias",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImagemUrl",
                value: "https://via.placeholder.com/600x400");

            migrationBuilder.UpdateData(
                table: "Noticias",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImagemUrl",
                value: "https://via.placeholder.com/600x400");

            migrationBuilder.UpdateData(
                table: "Noticias",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImagemUrl",
                value: "https://via.placeholder.com/600x400");

            migrationBuilder.UpdateData(
                table: "Noticias",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImagemUrl",
                value: "https://via.placeholder.com/600x400");

            migrationBuilder.UpdateData(
                table: "Noticias",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImagemUrl",
                value: "https://via.placeholder.com/600x400");
        }
    }
}
