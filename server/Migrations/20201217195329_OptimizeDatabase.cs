using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace server.Migrations
{
    public partial class OptimizeDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_account_settings_id_settings",
                table: "user_account");

            migrationBuilder.DropTable(
                name: "settings_x_channel");

            migrationBuilder.DropTable(
                name: "user_x_favorite");

            migrationBuilder.DropTable(
                name: "settings");

            migrationBuilder.DropIndex(
                name: "IX_user_account_id_settings",
                table: "user_account");

            migrationBuilder.DropColumn(
                name: "id_settings",
                table: "user_account");

            migrationBuilder.AddColumn<bool>(
                name: "dark_theme",
                table: "user_account",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "id_user",
                table: "favorite",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "id_user",
                table: "channel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_favorite_id_user",
                table: "favorite",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_channel_id_user",
                table: "channel",
                column: "id_user");

            migrationBuilder.AddForeignKey(
                name: "FK_channel_user_account_id_user",
                table: "channel",
                column: "id_user",
                principalTable: "user_account",
                principalColumn: "id_user",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_favorite_user_account_id_user",
                table: "favorite",
                column: "id_user",
                principalTable: "user_account",
                principalColumn: "id_user",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_channel_user_account_id_user",
                table: "channel");

            migrationBuilder.DropForeignKey(
                name: "FK_favorite_user_account_id_user",
                table: "favorite");

            migrationBuilder.DropIndex(
                name: "IX_favorite_id_user",
                table: "favorite");

            migrationBuilder.DropIndex(
                name: "IX_channel_id_user",
                table: "channel");

            migrationBuilder.DropColumn(
                name: "dark_theme",
                table: "user_account");

            migrationBuilder.DropColumn(
                name: "id_user",
                table: "favorite");

            migrationBuilder.DropColumn(
                name: "id_user",
                table: "channel");

            migrationBuilder.AddColumn<int>(
                name: "id_settings",
                table: "user_account",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "settings",
                columns: table => new
                {
                    id_settings = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    dark_theme = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_settings", x => x.id_settings);
                });

            migrationBuilder.CreateTable(
                name: "user_x_favorite",
                columns: table => new
                {
                    id_favorite = table.Column<int>(type: "integer", nullable: false),
                    id_user = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_x_favorite", x => new { x.id_favorite, x.id_user });
                    table.ForeignKey(
                        name: "FK_user_x_favorite_favorite_id_favorite",
                        column: x => x.id_favorite,
                        principalTable: "favorite",
                        principalColumn: "id_favorite",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_x_favorite_user_account_id_user",
                        column: x => x.id_user,
                        principalTable: "user_account",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "settings_x_channel",
                columns: table => new
                {
                    id_channel = table.Column<int>(type: "integer", nullable: false),
                    id_settings = table.Column<int>(type: "integer", nullable: false),
                    visible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_settings_x_channel", x => new { x.id_channel, x.id_settings });
                    table.ForeignKey(
                        name: "FK_settings_x_channel_channel_id_channel",
                        column: x => x.id_channel,
                        principalTable: "channel",
                        principalColumn: "id_channel",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_settings_x_channel_settings_id_settings",
                        column: x => x.id_settings,
                        principalTable: "settings",
                        principalColumn: "id_settings",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_account_id_settings",
                table: "user_account",
                column: "id_settings");

            migrationBuilder.CreateIndex(
                name: "IX_settings_x_channel_id_settings",
                table: "settings_x_channel",
                column: "id_settings");

            migrationBuilder.CreateIndex(
                name: "IX_user_x_favorite_id_user",
                table: "user_x_favorite",
                column: "id_user");

            migrationBuilder.AddForeignKey(
                name: "FK_user_account_settings_id_settings",
                table: "user_account",
                column: "id_settings",
                principalTable: "settings",
                principalColumn: "id_settings",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
