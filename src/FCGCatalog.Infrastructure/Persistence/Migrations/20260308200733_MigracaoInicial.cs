using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCGCatalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "jogos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    titulo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    descricao = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    preco = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    data_lancamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ativo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jogos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "biblioteca_usuarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    jogo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    data_compra = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_biblioteca_usuarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_biblioteca_usuarios_jogos_jogo_id",
                        column: x => x.jogo_id,
                        principalTable: "jogos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_biblioteca_usuario_usuario_id",
                table: "biblioteca_usuarios",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_biblioteca_usuarios_jogo_id",
                table: "biblioteca_usuarios",
                column: "jogo_id");

            migrationBuilder.CreateIndex(
                name: "ux_biblioteca_usuario_jogo",
                table: "biblioteca_usuarios",
                columns: new[] { "usuario_id", "jogo_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "biblioteca_usuarios");

            migrationBuilder.DropTable(
                name: "jogos");
        }
    }
}
