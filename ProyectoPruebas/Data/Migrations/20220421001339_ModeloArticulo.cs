using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoPruebas.Data.Migrations
{
    public partial class ModeloArticulo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articulo",
                columns: table => new
                {
                    ArticuloID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UltAct = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrecioCosto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PorcentajeGanancia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioVenta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubRubroID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articulo", x => x.ArticuloID);
                    table.ForeignKey(
                        name: "FK_Articulo_SubRubro_SubRubroID",
                        column: x => x.SubRubroID,
                        principalTable: "SubRubro",
                        principalColumn: "SubRubroID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articulo_SubRubroID",
                table: "Articulo",
                column: "SubRubroID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articulo");
        }
    }
}
