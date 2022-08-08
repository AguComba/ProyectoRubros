using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoPruebas.Data.Migrations
{
    public partial class CampoEliminadoRubros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "Rubro",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "Rubro");
        }
    }
}
