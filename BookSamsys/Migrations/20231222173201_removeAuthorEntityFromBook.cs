using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookSamsys.Migrations
{
    /// <inheritdoc />
    public partial class removeAuthorEntityFromBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livros_Autores_idAutor",
                table: "Livros");

            migrationBuilder.DropIndex(
                name: "IX_Livros_idAutor",
                table: "Livros");

            migrationBuilder.AddColumn<int>(
                name: "autoridAutor",
                table: "Livros",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Livros_autoridAutor",
                table: "Livros",
                column: "autoridAutor");

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_Autores_autoridAutor",
                table: "Livros",
                column: "autoridAutor",
                principalTable: "Autores",
                principalColumn: "idAutor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livros_Autores_autoridAutor",
                table: "Livros");

            migrationBuilder.DropIndex(
                name: "IX_Livros_autoridAutor",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "autoridAutor",
                table: "Livros");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_idAutor",
                table: "Livros",
                column: "idAutor");

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_Autores_idAutor",
                table: "Livros",
                column: "idAutor",
                principalTable: "Autores",
                principalColumn: "idAutor",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
