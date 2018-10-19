using Microsoft.EntityFrameworkCore.Migrations;

namespace Feeder.Data.Migrations
{
    public partial class AddSourceTypeInFeedTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Feeds",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Feeds");
        }
    }
}
