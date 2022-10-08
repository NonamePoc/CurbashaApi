using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurbashaApi.Migrations
{
    public partial class InitialCreateV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateOrder",
                table: "UserOrder",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TakeOrder",
                table: "UserOrder",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateOrder",
                table: "UserOrder");

            migrationBuilder.DropColumn(
                name: "TakeOrder",
                table: "UserOrder");
        }
    }
}
