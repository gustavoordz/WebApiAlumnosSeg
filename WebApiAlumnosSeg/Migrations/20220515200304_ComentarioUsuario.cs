using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiAlumnosSeg.Migrations
{
    public partial class ComentarioUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Cursos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_UsuarioId",
                table: "Cursos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_AspNetUsers_UsuarioId",
                table: "Cursos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_AspNetUsers_UsuarioId",
                table: "Cursos");

            migrationBuilder.DropIndex(
                name: "IX_Cursos_UsuarioId",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Cursos");
        }
    }
}
