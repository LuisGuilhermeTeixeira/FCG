using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FCG.Api.Migrations
{
    /// <inheritdoc />
    public partial class InicioProjeto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "fcg");

            migrationBuilder.CreateTable(
                name: "Jogo",
                schema: "fcg",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Preco = table.Column<decimal>(type: "numeric", nullable: false),
                    Categoria = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                schema: "fcg",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    SenhaHash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JogoUsuario",
                schema: "fcg",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    JogoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JogoUsuario", x => new { x.UsuarioId, x.JogoId });
                    table.ForeignKey(
                        name: "FK_JogoUsuario_Jogo_JogoId",
                        column: x => x.JogoId,
                        principalSchema: "fcg",
                        principalTable: "Jogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JogoUsuario_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "fcg",
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JogoUsuario_JogoId",
                schema: "fcg",
                table: "JogoUsuario",
                column: "JogoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Email",
                schema: "fcg",
                table: "Usuario",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JogoUsuario",
                schema: "fcg");

            migrationBuilder.DropTable(
                name: "Jogo",
                schema: "fcg");

            migrationBuilder.DropTable(
                name: "Usuario",
                schema: "fcg");
        }
    }
}
