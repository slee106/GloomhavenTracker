using Microsoft.EntityFrameworkCore.Migrations;

namespace GloomhavenTracker.Migrations
{
    public partial class AddedPartyItemLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unlocked",
                table: "Item");

            migrationBuilder.CreateTable(
                name: "PartyItem",
                columns: table => new
                {
                    PartyId = table.Column<int>(nullable: false),
                    ItemId = table.Column<int>(nullable: false),
                    Unlocked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyItem", x => new { x.PartyId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_PartyItem_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartyItem_Party_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Party",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartyItem_ItemId",
                table: "PartyItem",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartyItem");

            migrationBuilder.AddColumn<short>(
                name: "Unlocked",
                table: "Item",
                type: "bit",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
