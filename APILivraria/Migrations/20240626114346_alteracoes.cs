using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APILivraria.Migrations
{
    /// <inheritdoc />
    public partial class alteracoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Livrarias_LivrariaId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "LivrariaId",
                table: "Users",
                newName: "LivroId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_LivrariaId",
                table: "Users",
                newName: "IX_Users_LivroId");

            migrationBuilder.RenameColumn(
                name: "Livro",
                table: "Livrarias",
                newName: "Nome");

            migrationBuilder.CreateTable(
                name: "CadastroCompras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaisRegião = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cep = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Localidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ddd = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CadastroCompras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnderecoUsuario",
                columns: table => new
                {
                    EnderecoId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnderecoUsuario", x => new { x.UserId, x.EnderecoId });
                    table.ForeignKey(
                        name: "FK_EnderecoUsuario_CadastroCompras_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "CadastroCompras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnderecoUsuario_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnderecoUsuario_EnderecoId",
                table: "EnderecoUsuario",
                column: "EnderecoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Livrarias_LivroId",
                table: "Users",
                column: "LivroId",
                principalTable: "Livrarias",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Livrarias_LivroId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "EnderecoUsuario");

            migrationBuilder.DropTable(
                name: "CadastroCompras");

            migrationBuilder.RenameColumn(
                name: "LivroId",
                table: "Users",
                newName: "LivrariaId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_LivroId",
                table: "Users",
                newName: "IX_Users_LivrariaId");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Livrarias",
                newName: "Livro");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Livrarias_LivrariaId",
                table: "Users",
                column: "LivrariaId",
                principalTable: "Livrarias",
                principalColumn: "Id");
        }
    }
}
