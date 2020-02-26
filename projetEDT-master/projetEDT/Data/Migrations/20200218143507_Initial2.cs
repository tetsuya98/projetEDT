using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace projetEDT.Data.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Batiment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomBatiment = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batiment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UE",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Intitule = table.Column<string>(nullable: false),
                    Numero = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Salle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomSalle = table.Column<string>(nullable: false),
                    IdBatiment = table.Column<int>(nullable: false),
                    LeBatimentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salle_Batiment_LeBatimentId",
                        column: x => x.LeBatimentId,
                        principalTable: "Batiment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Groupe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomGroupe = table.Column<string>(nullable: false),
                    IdUE = table.Column<int>(nullable: false),
                    LUEId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groupe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groupe_UE_LUEId",
                        column: x => x.LUEId,
                        principalTable: "UE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Seance",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Jour = table.Column<DateTime>(nullable: false),
                    HeureDebut = table.Column<DateTime>(nullable: false),
                    Duree = table.Column<int>(nullable: false),
                    IdSalle = table.Column<int>(nullable: false),
                    LaSalleId = table.Column<int>(nullable: true),
                    IdUE = table.Column<int>(nullable: false),
                    LUEId = table.Column<int>(nullable: true),
                    IdGroupe = table.Column<int>(nullable: true),
                    LeGroupeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seance_UE_LUEId",
                        column: x => x.LUEId,
                        principalTable: "UE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Seance_Salle_LaSalleId",
                        column: x => x.LaSalleId,
                        principalTable: "Salle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Seance_Groupe_LeGroupeId",
                        column: x => x.LeGroupeId,
                        principalTable: "Groupe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groupe_LUEId",
                table: "Groupe",
                column: "LUEId");

            migrationBuilder.CreateIndex(
                name: "IX_Salle_LeBatimentId",
                table: "Salle",
                column: "LeBatimentId");

            migrationBuilder.CreateIndex(
                name: "IX_Seance_LUEId",
                table: "Seance",
                column: "LUEId");

            migrationBuilder.CreateIndex(
                name: "IX_Seance_LaSalleId",
                table: "Seance",
                column: "LaSalleId");

            migrationBuilder.CreateIndex(
                name: "IX_Seance_LeGroupeId",
                table: "Seance",
                column: "LeGroupeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Seance");

            migrationBuilder.DropTable(
                name: "Salle");

            migrationBuilder.DropTable(
                name: "Groupe");

            migrationBuilder.DropTable(
                name: "Batiment");

            migrationBuilder.DropTable(
                name: "UE");
        }
    }
}
