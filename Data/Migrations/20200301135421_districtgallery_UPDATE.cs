using Microsoft.EntityFrameworkCore.Migrations;

namespace GuideOfTurkey_AdminPanel.Data.Migrations
{
    public partial class districtgallery_UPDATE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "districtID",
                table: "DistrictGalleries",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "districtID",
                table: "DistrictGalleries");
        }
    }
}
