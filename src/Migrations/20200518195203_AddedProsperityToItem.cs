using Microsoft.EntityFrameworkCore.Migrations;

namespace GloomhavenTracker.Migrations
{
    public partial class AddedProsperityToItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Prosperity",
                table: "Item",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prosperity",
                table: "Item");
        }
    }
}
