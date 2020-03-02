using Microsoft.EntityFrameworkCore.Migrations;

namespace GuideOfTurkey_AdminPanel.Data.Migrations
{
    public partial class placeTypeUPDATE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "photoUrl",
                table: "Types",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "photoUrl",
                table: "Types");
        }
    }
}
