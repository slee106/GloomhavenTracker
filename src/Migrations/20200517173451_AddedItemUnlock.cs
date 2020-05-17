using Microsoft.EntityFrameworkCore.Migrations;

namespace GloomhavenTracker.Migrations
{
    public partial class AddedItemUnlock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Unlocked",
                table: "Item",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unlocked",
                table: "Item");
        }
    }
}
