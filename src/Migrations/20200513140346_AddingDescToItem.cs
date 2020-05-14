﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace GloomhavenTracker.Migrations
{
    public partial class AddingDescToItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Item",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Item");
        }
    }
}
