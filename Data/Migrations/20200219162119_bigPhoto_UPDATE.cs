using Microsoft.EntityFrameworkCore.Migrations;

namespace GuideOfTurkey_AdminPanel.Data.Migrations
{
    public partial class bigPhoto_UPDATE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Galleries");

            migrationBuilder.DropColumn(
                name: "CityID",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "GalleryID",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "GalleryID",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "GalleryID",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "GalleryID",
                table: "Cities");

            migrationBuilder.CreateTable(
                name: "CityGalleries",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    photoUrl = table.Column<string>(nullable: true),
                    deleteState = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityGalleries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CountryGalleries",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    photoUrl = table.Column<string>(nullable: true),
                    deleteState = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryGalleries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DistrictGalleries",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    photoUrl = table.Column<string>(nullable: true),
                    deleteState = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistrictGalleries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PlaceGalleries",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    photoUrl = table.Column<string>(nullable: true),
                    deleteState = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceGalleries", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityGalleries");

            migrationBuilder.DropTable(
                name: "CountryGalleries");

            migrationBuilder.DropTable(
                name: "DistrictGalleries");

            migrationBuilder.DropTable(
                name: "PlaceGalleries");

            migrationBuilder.AddColumn<int>(
                name: "CityID",
                table: "Places",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GalleryID",
                table: "Places",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GalleryID",
                table: "Photos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GalleryID",
                table: "Countries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GalleryID",
                table: "Cities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Galleries",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galleries", x => x.ID);
                });
        }
    }
}
