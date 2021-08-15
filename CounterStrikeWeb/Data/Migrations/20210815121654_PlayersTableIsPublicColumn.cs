namespace CounterStrikeWeb.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class PlayersTableIsPublicColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Players");
        }
    }
}
