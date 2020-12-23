using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Migrations
{
    public partial class UpdateChannel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "visible",
                table: "channel",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "visible",
                table: "channel");
        }
    }
}
