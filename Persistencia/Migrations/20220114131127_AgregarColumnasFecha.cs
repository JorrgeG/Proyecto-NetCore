using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistencia.Migrations
{
    public partial class AgregarColumnasFecha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "CursoInstructor",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Curso",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Comentario",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Barrio",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DireccionCasa",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "CursoInstructor");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Curso");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Comentario");

            migrationBuilder.DropColumn(
                name: "Barrio",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DireccionCasa",
                table: "AspNetUsers");
        }
    }
}
