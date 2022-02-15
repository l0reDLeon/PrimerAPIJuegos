using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI1990081.Migrations
{
    public partial class Plataforma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plataforma",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    compania = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    juegoid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plataforma", x => x.id);
                    table.ForeignKey(
                        name: "FK_Plataforma_Juegos_juegoid",
                        column: x => x.juegoid,
                        principalTable: "Juegos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plataforma_juegoid",
                table: "Plataforma",
                column: "juegoid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plataforma");
        }
    }
}
