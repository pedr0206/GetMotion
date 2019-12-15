using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GetFitness02.Migrations
{
    public partial class addActivityEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityItem",
                columns: table => new
                {
                    ActivityItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityName = table.Column<string>(nullable: true),
                    Calorie = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityItem", x => x.ActivityItemId);
                });

            migrationBuilder.CreateTable(
                name: "ActivityEntry",
                columns: table => new
                {
                    ActivityEntryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    ActivityItemId = table.Column<int>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityEntry", x => x.ActivityEntryId);
                    table.ForeignKey(
                        name: "FK_ActivityEntry_ActivityItem_ActivityItemId",
                        column: x => x.ActivityItemId,
                        principalTable: "ActivityItem",
                        principalColumn: "ActivityItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityEntry_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityEntry_ActivityItemId",
                table: "ActivityEntry",
                column: "ActivityItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityEntry_ApplicationUserId",
                table: "ActivityEntry",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityEntry");

            migrationBuilder.DropTable(
                name: "ActivityItem");
        }
    }
}
