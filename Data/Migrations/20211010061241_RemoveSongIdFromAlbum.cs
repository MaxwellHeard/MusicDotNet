using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicDotNet.Data.Migrations
{
    public partial class RemoveSongIdFromAlbum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SongId",
                table: "Albums");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SongId",
                table: "Albums",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
