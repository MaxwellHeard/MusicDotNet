using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicDotNet.Data.Migrations
{
    public partial class AddSongTrackNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrackNo",
                table: "Songs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackNo",
                table: "Songs");
        }
    }
}
