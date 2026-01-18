using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiAutorizadorReportes.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarUsuarioCampos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Apellidos",
                table: "Usuarios",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Edad",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Nombres",
                table: "Usuarios",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Puesto",
                table: "Usuarios",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apellidos",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Edad",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Nombres",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Puesto",
                table: "Usuarios");
        }
    }
}
