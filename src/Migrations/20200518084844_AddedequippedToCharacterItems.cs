using Microsoft.EntityFrameworkCore.Migrations;

namespace GloomhavenTracker.Migrations
{
    public partial class AddedequippedToCharacterItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Equipped",
                table: "Item");

            migrationBuilder.AddColumn<bool>(
                name: "Equipped",
                table: "CharacterItem",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Equipped",
                table: "CharacterItem");

            migrationBuilder.AddColumn<short>(
                name: "Equipped",
                table: "Item",
                type: "bit",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
