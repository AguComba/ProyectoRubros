using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoPruebas.Data.Migrations
{
    public partial class PrimerMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rubro",
                columns: table => new
                {
                    IdRubro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rubro", x => x.IdRubro);
                });

            migrationBuilder.CreateTable(
                name: "SubRubro",
                columns: table => new
                {
                    SubRubroID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RubroID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubRubro", x => x.SubRubroID);
                    table.ForeignKey(
                        name: "FK_SubRubro_Rubro_RubroID",
                        column: x => x.RubroID,
                        principalTable: "Rubro",
                        principalColumn: "IdRubro",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubRubro_RubroID",
                table: "SubRubro",
                column: "RubroID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubRubro");

            migrationBuilder.DropTable(
                name: "Rubro");
        }
    }
}
