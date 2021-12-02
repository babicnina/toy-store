using Microsoft.EntityFrameworkCore.Migrations;

namespace PetToyShop.Migrations
{
    public partial class UserAccountRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "BankAccount",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "BankAccount",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_UserId1",
                table: "BankAccount",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_AspNetUsers_UserId1",
                table: "BankAccount",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_AspNetUsers_UserId1",
                table: "BankAccount");

            migrationBuilder.DropIndex(
                name: "IX_BankAccount_UserId1",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "BankAccount");
        }
    }
}
