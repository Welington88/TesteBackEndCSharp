using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteBackEndCSharp.Migrations
{
    public partial class CriarTabela : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Money",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Moeda = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    data_inicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    data_fim = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Money", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Money");
        }
    }
}
