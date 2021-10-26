using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Music_Store.Data.Migrations
{
    public partial class changePublisherTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstablishedDate",
                table: "Publisher");

            migrationBuilder.AddColumn<int>(
                name: "EstablishedYear",
                table: "Publisher",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstablishedYear",
                table: "Publisher");

            migrationBuilder.AddColumn<DateTime>(
                name: "EstablishedDate",
                table: "Publisher",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
