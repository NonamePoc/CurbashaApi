using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurbashaApi.Migrations
{
    public partial class initialcommitv17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "AspUserOrder",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "AspUserOrder");
        }
    }
}
