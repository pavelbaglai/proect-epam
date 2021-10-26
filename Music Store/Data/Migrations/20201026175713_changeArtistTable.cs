using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Music_Store.Data.Migrations
{
    public partial class changeArtistTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "Lastname",
                table: "Artist");

            migrationBuilder.RenameColumn(
                name: "StageName",
                table: "Artist",
                newName: "Stagename");

            migrationBuilder.AddColumn<int>(
                name: "DebutYear",
                table: "Artist",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Fullname",
                table: "Artist",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Artist",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DebutYear",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "Fullname",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Artist");

            migrationBuilder.RenameColumn(
                name: "Stagename",
                table: "Artist",
                newName: "StageName");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Artist",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "Artist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Artist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                table: "Artist",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
