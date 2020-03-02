using Microsoft.EntityFrameworkCore.Migrations;

namespace GuideOfTurkey_AdminPanel.Data.Migrations
{
    public partial class district_UPDATE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Explain",
                table: "Districts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Explain",
                table: "Districts");
        }
    }
}
