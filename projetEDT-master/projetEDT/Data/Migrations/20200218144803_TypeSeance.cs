using Microsoft.EntityFrameworkCore.Migrations;

namespace projetEDT.Data.Migrations
{
    public partial class TypeSeance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeSeanceid",
                table: "Seance",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TypeSeance",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intitule = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeSeance", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seance_TypeSeanceid",
                table: "Seance",
                column: "TypeSeanceid");

            migrationBuilder.AddForeignKey(
                name: "FK_Seance_TypeSeance_TypeSeanceid",
                table: "Seance",
                column: "TypeSeanceid",
                principalTable: "TypeSeance",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seance_TypeSeance_TypeSeanceid",
                table: "Seance");

            migrationBuilder.DropTable(
                name: "TypeSeance");

            migrationBuilder.DropIndex(
                name: "IX_Seance_TypeSeanceid",
                table: "Seance");

            migrationBuilder.DropColumn(
                name: "TypeSeanceid",
                table: "Seance");
        }
    }
}
