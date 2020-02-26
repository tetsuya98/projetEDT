using Microsoft.EntityFrameworkCore.Migrations;

namespace projetEDT.Data.Migrations
{
    public partial class Correction2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groupe_UE_LUEId",
                table: "Groupe");

            migrationBuilder.DropForeignKey(
                name: "FK_Salle_Batiment_LeBatimentId",
                table: "Salle");

            migrationBuilder.DropForeignKey(
                name: "FK_Seance_UE_LUEId",
                table: "Seance");

            migrationBuilder.DropForeignKey(
                name: "FK_Seance_Salle_LaSalleId",
                table: "Seance");

            migrationBuilder.DropForeignKey(
                name: "FK_Seance_Groupe_LeGroupeId",
                table: "Seance");

            migrationBuilder.DropForeignKey(
                name: "FK_Seance_TypeSeance_TypeSeanceid",
                table: "Seance");

            migrationBuilder.DropIndex(
                name: "IX_Seance_LUEId",
                table: "Seance");

            migrationBuilder.DropIndex(
                name: "IX_Seance_LaSalleId",
                table: "Seance");

            migrationBuilder.DropIndex(
                name: "IX_Seance_LeGroupeId",
                table: "Seance");

            migrationBuilder.DropIndex(
                name: "IX_Seance_TypeSeanceid",
                table: "Seance");

            migrationBuilder.DropIndex(
                name: "IX_Salle_LeBatimentId",
                table: "Salle");

            migrationBuilder.DropIndex(
                name: "IX_Groupe_LUEId",
                table: "Groupe");

            migrationBuilder.DropColumn(
                name: "IdGroupe",
                table: "Seance");

            migrationBuilder.DropColumn(
                name: "IdSalle",
                table: "Seance");

            migrationBuilder.DropColumn(
                name: "IdUE",
                table: "Seance");

            migrationBuilder.DropColumn(
                name: "LUEId",
                table: "Seance");

            migrationBuilder.DropColumn(
                name: "LaSalleId",
                table: "Seance");

            migrationBuilder.DropColumn(
                name: "LeGroupeId",
                table: "Seance");

            migrationBuilder.DropColumn(
                name: "TypeSeanceid",
                table: "Seance");

            migrationBuilder.DropColumn(
                name: "IdBatiment",
                table: "Salle");

            migrationBuilder.DropColumn(
                name: "LeBatimentId",
                table: "Salle");

            migrationBuilder.DropColumn(
                name: "IdUE",
                table: "Groupe");

            migrationBuilder.DropColumn(
                name: "LUEId",
                table: "Groupe");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UE",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "intitule",
                table: "TypeSeance",
                newName: "Intitule");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "TypeSeance",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Seance",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Salle",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Groupe",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Batiment",
                newName: "ID");

            migrationBuilder.AddColumn<int>(
                name: "GroupeID",
                table: "Seance",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalleID",
                table: "Seance",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeID",
                table: "Seance",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UEID",
                table: "Seance",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BatimentID",
                table: "Salle",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UEID",
                table: "Groupe",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Seance_GroupeID",
                table: "Seance",
                column: "GroupeID");

            migrationBuilder.CreateIndex(
                name: "IX_Seance_SalleID",
                table: "Seance",
                column: "SalleID");

            migrationBuilder.CreateIndex(
                name: "IX_Seance_TypeID",
                table: "Seance",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Seance_UEID",
                table: "Seance",
                column: "UEID");

            migrationBuilder.CreateIndex(
                name: "IX_Salle_BatimentID",
                table: "Salle",
                column: "BatimentID");

            migrationBuilder.CreateIndex(
                name: "IX_Groupe_UEID",
                table: "Groupe",
                column: "UEID");

            migrationBuilder.AddForeignKey(
                name: "FK_Groupe_UE_UEID",
                table: "Groupe",
                column: "UEID",
                principalTable: "UE",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Salle_Batiment_BatimentID",
                table: "Salle",
                column: "BatimentID",
                principalTable: "Batiment",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seance_Groupe_GroupeID",
                table: "Seance",
                column: "GroupeID",
                principalTable: "Groupe",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seance_Salle_SalleID",
                table: "Seance",
                column: "SalleID",
                principalTable: "Salle",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seance_TypeSeance_TypeID",
                table: "Seance",
                column: "TypeID",
                principalTable: "TypeSeance",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seance_UE_UEID",
                table: "Seance",
                column: "UEID",
                principalTable: "UE",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groupe_UE_UEID",
                table: "Groupe");

            migrationBuilder.DropForeignKey(
                name: "FK_Salle_Batiment_BatimentID",
                table: "Salle");

            migrationBuilder.DropForeignKey(
                name: "FK_Seance_Groupe_GroupeID",
                table: "Seance");

            migrationBuilder.DropForeignKey(
                name: "FK_Seance_Salle_SalleID",
                table: "Seance");

            migrationBuilder.DropForeignKey(
                name: "FK_Seance_TypeSeance_TypeID",
                table: "Seance");

            migrationBuilder.DropForeignKey(
                name: "FK_Seance_UE_UEID",
                table: "Seance");

            migrationBuilder.DropIndex(
                name: "IX_Seance_GroupeID",
                table: "Seance");

            migrationBuilder.DropIndex(
                name: "IX_Seance_SalleID",
                table: "Seance");

            migrationBuilder.DropIndex(
                name: "IX_Seance_TypeID",
                table: "Seance");

            migrationBuilder.DropIndex(
                name: "IX_Seance_UEID",
                table: "Seance");

            migrationBuilder.DropIndex(
                name: "IX_Salle_BatimentID",
                table: "Salle");

            migrationBuilder.DropIndex(
                name: "IX_Groupe_UEID",
                table: "Groupe");

            migrationBuilder.DropColumn(
                name: "GroupeID",
                table: "Seance");

            migrationBuilder.DropColumn(
                name: "SalleID",
                table: "Seance");

            migrationBuilder.DropColumn(
                name: "TypeID",
                table: "Seance");

            migrationBuilder.DropColumn(
                name: "UEID",
                table: "Seance");

            migrationBuilder.DropColumn(
                name: "BatimentID",
                table: "Salle");

            migrationBuilder.DropColumn(
                name: "UEID",
                table: "Groupe");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "UE",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Intitule",
                table: "TypeSeance",
                newName: "intitule");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "TypeSeance",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Seance",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Salle",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Groupe",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Batiment",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "IdGroupe",
                table: "Seance",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdSalle",
                table: "Seance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdUE",
                table: "Seance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LUEId",
                table: "Seance",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LaSalleId",
                table: "Seance",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LeGroupeId",
                table: "Seance",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeSeanceid",
                table: "Seance",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdBatiment",
                table: "Salle",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LeBatimentId",
                table: "Salle",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdUE",
                table: "Groupe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LUEId",
                table: "Groupe",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Seance_TypeSeanceid",
                table: "Seance",
                column: "TypeSeanceid");

            migrationBuilder.CreateIndex(
                name: "IX_Salle_LeBatimentId",
                table: "Salle",
                column: "LeBatimentId");

            migrationBuilder.CreateIndex(
                name: "IX_Groupe_LUEId",
                table: "Groupe",
                column: "LUEId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groupe_UE_LUEId",
                table: "Groupe",
                column: "LUEId",
                principalTable: "UE",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Salle_Batiment_LeBatimentId",
                table: "Salle",
                column: "LeBatimentId",
                principalTable: "Batiment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seance_UE_LUEId",
                table: "Seance",
                column: "LUEId",
                principalTable: "UE",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seance_Salle_LaSalleId",
                table: "Seance",
                column: "LaSalleId",
                principalTable: "Salle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seance_Groupe_LeGroupeId",
                table: "Seance",
                column: "LeGroupeId",
                principalTable: "Groupe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seance_TypeSeance_TypeSeanceid",
                table: "Seance",
                column: "TypeSeanceid",
                principalTable: "TypeSeance",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
