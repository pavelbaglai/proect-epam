using Microsoft.EntityFrameworkCore.Migrations;

namespace Music_Store.Data.Migrations
{
    public partial class ChangeIdNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Album_Artist_ArtistID",
                table: "Album");

            migrationBuilder.DropForeignKey(
                name: "FK_Album_Publisher_PublisherID",
                table: "Album");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Customer_CustomerID",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Song_SongID",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Song_Album_AlbumID",
                table: "Song");

            migrationBuilder.DropForeignKey(
                name: "FK_Song_Artist_ArtistID",
                table: "Song");

            migrationBuilder.DropForeignKey(
                name: "FK_Song_Publisher_PublisherID",
                table: "Song");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Customer_CustomerID",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Employee_EmployeeID",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Song",
                table: "Song");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publisher",
                table: "Publisher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Artist",
                table: "Artist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Album",
                table: "Album");

            migrationBuilder.DropColumn(
                name: "SongID",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "ReviewID",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "PublisherID",
                table: "Publisher");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ArtistID",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "AlbumID",
                table: "Album");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Song",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Review",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Publisher",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Employee",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Customer",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Artist",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Album",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Song",
                table: "Song",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publisher",
                table: "Publisher",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Artist",
                table: "Artist",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Album",
                table: "Album",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Album_Artist_ArtistID",
                table: "Album",
                column: "ArtistID",
                principalTable: "Artist",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Album_Publisher_PublisherID",
                table: "Album",
                column: "PublisherID",
                principalTable: "Publisher",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Customer_CustomerID",
                table: "Review",
                column: "CustomerID",
                principalTable: "Customer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Song_SongID",
                table: "Review",
                column: "SongID",
                principalTable: "Song",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Song_Album_AlbumID",
                table: "Song",
                column: "AlbumID",
                principalTable: "Album",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Song_Artist_ArtistID",
                table: "Song",
                column: "ArtistID",
                principalTable: "Artist",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Song_Publisher_PublisherID",
                table: "Song",
                column: "PublisherID",
                principalTable: "Publisher",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Customer_CustomerID",
                table: "User",
                column: "CustomerID",
                principalTable: "Customer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Employee_EmployeeID",
                table: "User",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Album_Artist_ArtistID",
                table: "Album");

            migrationBuilder.DropForeignKey(
                name: "FK_Album_Publisher_PublisherID",
                table: "Album");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Customer_CustomerID",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Song_SongID",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Song_Album_AlbumID",
                table: "Song");

            migrationBuilder.DropForeignKey(
                name: "FK_Song_Artist_ArtistID",
                table: "Song");

            migrationBuilder.DropForeignKey(
                name: "FK_Song_Publisher_PublisherID",
                table: "Song");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Customer_CustomerID",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Employee_EmployeeID",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Song",
                table: "Song");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publisher",
                table: "Publisher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Artist",
                table: "Artist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Album",
                table: "Album");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Publisher");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Album");

            migrationBuilder.AddColumn<int>(
                name: "SongID",
                table: "Song",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ReviewID",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PublisherID",
                table: "Publisher",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeID",
                table: "Employee",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "CustomerID",
                table: "Customer",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ArtistID",
                table: "Artist",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "AlbumID",
                table: "Album",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Song",
                table: "Song",
                column: "SongID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                column: "ReviewID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publisher",
                table: "Publisher",
                column: "PublisherID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "EmployeeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "CustomerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Artist",
                table: "Artist",
                column: "ArtistID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Album",
                table: "Album",
                column: "AlbumID");

            migrationBuilder.AddForeignKey(
                name: "FK_Album_Artist_ArtistID",
                table: "Album",
                column: "ArtistID",
                principalTable: "Artist",
                principalColumn: "ArtistID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Album_Publisher_PublisherID",
                table: "Album",
                column: "PublisherID",
                principalTable: "Publisher",
                principalColumn: "PublisherID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Customer_CustomerID",
                table: "Review",
                column: "CustomerID",
                principalTable: "Customer",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Song_SongID",
                table: "Review",
                column: "SongID",
                principalTable: "Song",
                principalColumn: "SongID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Song_Album_AlbumID",
                table: "Song",
                column: "AlbumID",
                principalTable: "Album",
                principalColumn: "AlbumID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Song_Artist_ArtistID",
                table: "Song",
                column: "ArtistID",
                principalTable: "Artist",
                principalColumn: "ArtistID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Song_Publisher_PublisherID",
                table: "Song",
                column: "PublisherID",
                principalTable: "Publisher",
                principalColumn: "PublisherID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Customer_CustomerID",
                table: "User",
                column: "CustomerID",
                principalTable: "Customer",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Employee_EmployeeID",
                table: "User",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
