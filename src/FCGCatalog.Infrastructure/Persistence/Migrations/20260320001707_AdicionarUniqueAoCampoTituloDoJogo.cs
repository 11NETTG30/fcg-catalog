using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCGCatalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarUniqueAoCampoTituloDoJogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ux_jogos_titulo",
                table: "jogos",
                column: "titulo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ux_jogos_titulo",
                table: "jogos");
        }
    }
}
