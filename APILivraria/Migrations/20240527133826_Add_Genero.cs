using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APILivraria.Migrations
{
    /// <inheritdoc />
    public partial class Add_Genero : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Genero",
                table: "Livrarias",
                newName: "GeneroId");

            migrationBuilder.CreateTable(
                name: "Generossss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generossss", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Livrarias_GeneroId",
                table: "Livrarias",
                column: "GeneroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Livrarias_Generossss_GeneroId",
                table: "Livrarias",
                column: "GeneroId",
                principalTable: "Generossss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livrarias_Generossss_GeneroId",
                table: "Livrarias");

            migrationBuilder.DropTable(
                name: "Generossss");

            migrationBuilder.DropIndex(
                name: "IX_Livrarias_GeneroId",
                table: "Livrarias");

            migrationBuilder.RenameColumn(
                name: "GeneroId",
                table: "Livrarias",
                newName: "Genero");
        }
    }
}
