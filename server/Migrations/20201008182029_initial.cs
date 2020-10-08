using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace server.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "channel",
                columns: table => new
                {
                    id_channel = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(maxLength: 255, nullable: false),
                    link = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_channel", x => x.id_channel);
                });

            migrationBuilder.CreateTable(
                name: "favorite",
                columns: table => new
                {
                    id_favorite = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    link = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favorite", x => x.id_favorite);
                });

            migrationBuilder.CreateTable(
                name: "settings",
                columns: table => new
                {
                    id_settings = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    dark_theme = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_settings", x => x.id_settings);
                });

            migrationBuilder.CreateTable(
                name: "settings_x_channel",
                columns: table => new
                {
                    id_channel = table.Column<int>(nullable: false),
                    id_settings = table.Column<int>(nullable: false),
                    visible = table.Column<bool>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "user_account",
                columns: table => new
                {
                    id_user = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    login = table.Column<string>(maxLength: 255, nullable: false),
                    password = table.Column<string>(maxLength: 255, nullable: false),
                    id_settings = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_account", x => x.id_user);
                    table.ForeignKey(
                        name: "FK_user_account_settings_id_settings",
                        column: x => x.id_settings,
                        principalTable: "settings",
                        principalColumn: "id_settings",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_x_favorite",
                columns: table => new
                {
                    id_user = table.Column<int>(nullable: false),
                    id_favorite = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_settings_x_channel_id_settings",
                table: "settings_x_channel",
                column: "id_settings");

            migrationBuilder.CreateIndex(
                name: "IX_user_account_id_settings",
                table: "user_account",
                column: "id_settings");

            migrationBuilder.CreateIndex(
                name: "IX_user_x_favorite_id_user",
                table: "user_x_favorite",
                column: "id_user");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "settings_x_channel");

            migrationBuilder.DropTable(
                name: "user_x_favorite");

            migrationBuilder.DropTable(
                name: "channel");

            migrationBuilder.DropTable(
                name: "favorite");

            migrationBuilder.DropTable(
                name: "user_account");

            migrationBuilder.DropTable(
                name: "settings");
        }
    }
}
