using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jugadores.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventoPartidos",
                columns: table => new
                {
                    EventoPartidoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartidoID = table.Column<int>(type: "int", nullable: false),
                    FechaEvento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventoPartidos", x => x.EventoPartidoID);
                    table.ForeignKey(
                        name: "FK_EventoPartidos_Partidos_PartidoID",
                        column: x => x.PartidoID,
                        principalTable: "Partidos",
                        principalColumn: "PartidoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventoPartidos_PartidoID",
                table: "EventoPartidos",
                column: "PartidoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventoPartidos");
        }
    }
}
