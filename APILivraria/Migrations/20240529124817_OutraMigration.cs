using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APILivraria.Migrations
{
    /// <inheritdoc />
    public partial class OutraMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livrarias_Generossss_GeneroId",
                table: "Livrarias");

            migrationBuilder.DropIndex(
                name: "IX_Livrarias_GeneroId",
                table: "Livrarias");

            migrationBuilder.DropColumn(
                name: "GeneroId",
                table: "Livrarias");

            migrationBuilder.CreateTable(
                name: "LivroGeneros",
                columns: table => new
                {
                    LivroId = table.Column<int>(type: "int", nullable: false),
                    GeneroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivroGeneros", x => new { x.LivroId, x.GeneroId });
                    table.ForeignKey(
                        name: "FK_LivroGeneros_Generossss_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Generossss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LivroGeneros_Livrarias_LivroId",
                        column: x => x.LivroId,
                        principalTable: "Livrarias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LivroGeneros_GeneroId",
                table: "LivroGeneros",
                column: "GeneroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LivroGeneros");

            migrationBuilder.AddColumn<int>(
                name: "GeneroId",
                table: "Livrarias",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
