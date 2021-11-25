using Microsoft.EntityFrameworkCore.Migrations;

namespace PetToyShop.Migrations
{
    public partial class PetToyRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PetId",
                table: "Toy",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Toy_PetId",
                table: "Toy",
                column: "PetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Toy_Pet_PetId",
                table: "Toy",
                column: "PetId",
                principalTable: "Pet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Toy_Pet_PetId",
                table: "Toy");

            migrationBuilder.DropIndex(
                name: "IX_Toy_PetId",
                table: "Toy");

            migrationBuilder.DropColumn(
                name: "PetId",
                table: "Toy");
        }
    }
}
