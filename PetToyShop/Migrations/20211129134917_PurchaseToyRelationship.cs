using Microsoft.EntityFrameworkCore.Migrations;

namespace PetToyShop.Migrations
{
    public partial class PurchaseToyRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchaseToy",
                columns: table => new
                {
                    PurchasesId = table.Column<int>(type: "int", nullable: false),
                    ToyItemsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseToy", x => new { x.PurchasesId, x.ToyItemsId });
                    table.ForeignKey(
                        name: "FK_PurchaseToy_Purchase_PurchasesId",
                        column: x => x.PurchasesId,
                        principalTable: "Purchase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseToy_Toy_ToyItemsId",
                        column: x => x.ToyItemsId,
                        principalTable: "Toy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseToy_ToyItemsId",
                table: "PurchaseToy",
                column: "ToyItemsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseToy");
        }
    }
}
