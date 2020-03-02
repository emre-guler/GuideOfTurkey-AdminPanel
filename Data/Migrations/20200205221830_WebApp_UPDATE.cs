using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GuideOfTurkey_AdminPanel.Data.Migrations
{
    public partial class WebApp_UPDATE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryID = table.Column<int>(nullable: false),
                    GalleryID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Explain = table.Column<string>(nullable: true),
                    photoUrl = table.Column<string>(nullable: true),
                    homepageState = table.Column<bool>(nullable: false),
                    deleteState = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    PlaceID = table.Column<int>(nullable: false),
                    userComment = table.Column<string>(nullable: true),
                    Rating = table.Column<float>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false, defaultValueSql: "getDate()"),
                    deleteState = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Explain = table.Column<string>(nullable: true),
                    photoUrl = table.Column<string>(nullable: true),
                    GalleryID = table.Column<int>(nullable: false),
                    deleteState = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    photoUrl = table.Column<string>(nullable: true),
                    deleteState = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    PlaceID = table.Column<int>(nullable: false),
                    deleteState = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Galleries",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galleries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GalleryID = table.Column<int>(nullable: false),
                    photoUrl = table.Column<string>(nullable: true),
                    sliderState = table.Column<bool>(nullable: false, defaultValue: false),
                    deleteState = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityID = table.Column<int>(nullable: false),
                    DistrictID = table.Column<int>(nullable: false),
                    GalleryID = table.Column<int>(nullable: false),
                    TypeID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Explain = table.Column<string>(nullable: true),
                    Rating = table.Column<float>(nullable: false),
                    photoUrl = table.Column<string>(nullable: true),
                    deleteState = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(nullable: true),
                    homepageState = table.Column<bool>(nullable: false),
                    deleteState = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "userAccounts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fullname = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    photoUrl = table.Column<string>(nullable: true),
                    deleteState = table.Column<bool>(nullable: false, defaultValue: false),
                    userRank = table.Column<bool>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false, defaultValueSql: "getDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userAccounts", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "Galleries");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropTable(
                name: "userAccounts");
        }
    }
}
