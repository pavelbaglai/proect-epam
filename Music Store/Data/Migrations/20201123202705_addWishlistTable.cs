using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Music_Store.Data.Migrations
{
    public partial class addWishlistTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wishlist",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlist", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Wishlist_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishlistItem",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WishlistID = table.Column<int>(nullable: false),
                    SongID = table.Column<int>(nullable: true),
                    AlbumID = table.Column<int>(nullable: true),
                    Price = table.Column<float>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WishlistItem_Album_AlbumID",
                        column: x => x.AlbumID,
                        principalTable: "Album",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WishlistItem_Song_SongID",
                        column: x => x.SongID,
                        principalTable: "Song",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WishlistItem_Wishlist_WishlistID",
                        column: x => x.WishlistID,
                        principalTable: "Wishlist",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_CustomerID",
                table: "Wishlist",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItem_AlbumID",
                table: "WishlistItem",
                column: "AlbumID");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItem_SongID",
                table: "WishlistItem",
                column: "SongID");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItem_WishlistID",
                table: "WishlistItem",
                column: "WishlistID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WishlistItem");

            migrationBuilder.DropTable(
                name: "Wishlist");
        }
    }
}
