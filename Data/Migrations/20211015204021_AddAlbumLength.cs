using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicDotNet.Data.Migrations
{
    public partial class AddAlbumLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "Albums",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Length",
                table: "Albums");
        }
    }
}
