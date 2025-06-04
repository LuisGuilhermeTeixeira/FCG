using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCG.Api.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoPerfil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Perfil",
                schema: "fcg",
                table: "Usuario",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Perfil",
                schema: "fcg",
                table: "Usuario");
        }
    }
}
