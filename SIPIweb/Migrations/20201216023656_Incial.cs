using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SIPIweb.Migrations
{
    public partial class Incial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id_usuario = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuario_login = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    usuario_pass = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    usuario_email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    usuario_birthday = table.Column<DateTime>(type: "date", nullable: false),
                    usuario_createdday = table.Column<DateTime>(type: "date", nullable: false),
                    usuario_tipo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "informacion",
                columns: table => new
                {
                    id_informacion = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    informacion_titulo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    informacion_cuerpo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    informacion_fechaPublicacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    informacion_fechaLimite = table.Column<DateTime>(type: "datetime", nullable: true),
                    id_usuario = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_informacion", x => x.id_informacion);
                    table.ForeignKey(
                        name: "FK_informacion_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_informacion_id_usuario",
                table: "informacion",
                column: "id_usuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "informacion");

            migrationBuilder.DropTable(
                name: "usuario");
        }
    }
}
