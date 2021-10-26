using Microsoft.EntityFrameworkCore.Migrations;

namespace Music_Store.Data.Migrations
{
    public partial class changeSongTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Runtime",
                table: "Song");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Song",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RuntimeInSec",
                table: "Song",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "RuntimeInSec",
                table: "Song");

            migrationBuilder.AddColumn<int>(
                name: "Runtime",
                table: "Song",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
