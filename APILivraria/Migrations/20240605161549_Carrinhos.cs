using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APILivraria.Migrations
{
    /// <inheritdoc />
    public partial class Carrinhos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LivrariaUser");

            migrationBuilder.AddColumn<int>(
                name: "CarrinhoId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LivrariaId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Carrinhos",
                columns: table => new
                {
                    CarrinhoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuantidadeItens = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrinhos", x => x.CarrinhoId);
                    table.ForeignKey(
                        name: "FK_Carrinhos_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarrinhoItems",
                columns: table => new
                {
                    CarrinhoItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarrinhoId = table.Column<int>(type: "int", nullable: false),
                    LivroId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoItems", x => x.CarrinhoItemId);
                    table.ForeignKey(
                        name: "FK_CarrinhoItems_Carrinhos_CarrinhoId",
                        column: x => x.CarrinhoId,
                        principalTable: "Carrinhos",
                        principalColumn: "CarrinhoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarrinhoItems_Livrarias_LivroId",
                        column: x => x.LivroId,
                        principalTable: "Livrarias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CarrinhoId", "LivrariaId" },
                values: new object[] { 0, null });

            migrationBuilder.CreateIndex(
                name: "IX_Users_LivrariaId",
                table: "Users",
                column: "LivrariaId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoItems_CarrinhoId",
                table: "CarrinhoItems",
                column: "CarrinhoId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoItems_LivroId",
                table: "CarrinhoItems",
                column: "LivroId");

            migrationBuilder.CreateIndex(
                name: "IX_Carrinhos_UserId",
                table: "Carrinhos",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Livrarias_LivrariaId",
                table: "Users",
                column: "LivrariaId",
                principalTable: "Livrarias",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Livrarias_LivrariaId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "CarrinhoItems");

            migrationBuilder.DropTable(
                name: "Carrinhos");

            migrationBuilder.DropIndex(
                name: "IX_Users_LivrariaId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CarrinhoId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LivrariaId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "LivrariaUser",
                columns: table => new
                {
                    CarrinhoId = table.Column<int>(type: "int", nullable: false),
                    UsuariosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivrariaUser", x => new { x.CarrinhoId, x.UsuariosId });
                    table.ForeignKey(
                        name: "FK_LivrariaUser_Livrarias_CarrinhoId",
                        column: x => x.CarrinhoId,
                        principalTable: "Livrarias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LivrariaUser_Users_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LivrariaUser_UsuariosId",
                table: "LivrariaUser",
                column: "UsuariosId");
        }
    }
}
