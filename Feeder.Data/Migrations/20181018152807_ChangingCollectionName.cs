using Microsoft.EntityFrameworkCore.Migrations;

namespace Feeder.Data.Migrations
{
    public partial class ChangingCollectionName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CollectionName",
                table: "Collections",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Collections",
                newName: "CollectionName");
        }
    }
}
