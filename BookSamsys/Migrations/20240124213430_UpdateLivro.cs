using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookSamsys.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLivro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "preco",
                table: "Livros",
                newName: "Preco");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Livros",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "idAutor",
                table: "Livros",
                newName: "IdAutor");

            migrationBuilder.RenameColumn(
                name: "isbn",
                table: "Livros",
                newName: "ISBN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Preco",
                table: "Livros",
                newName: "preco");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Livros",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "IdAutor",
                table: "Livros",
                newName: "idAutor");

            migrationBuilder.RenameColumn(
                name: "ISBN",
                table: "Livros",
                newName: "isbn");
        }
    }
}
