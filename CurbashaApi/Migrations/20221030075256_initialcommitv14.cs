using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurbashaApi.Migrations
{
    public partial class initialcommitv14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AspUserOrderId",
                table: "AspOrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AspUserOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspUserOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspUserOrder_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspOrderItems_AspUserOrderId",
                table: "AspOrderItems",
                column: "AspUserOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AspUserOrder_UserId",
                table: "AspUserOrder",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspOrderItems_AspUserOrder_AspUserOrderId",
                table: "AspOrderItems",
                column: "AspUserOrderId",
                principalTable: "AspUserOrder",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspOrderItems_AspUserOrder_AspUserOrderId",
                table: "AspOrderItems");

            migrationBuilder.DropTable(
                name: "AspUserOrder");

            migrationBuilder.DropIndex(
                name: "IX_AspOrderItems_AspUserOrderId",
                table: "AspOrderItems");

            migrationBuilder.DropColumn(
                name: "AspUserOrderId",
                table: "AspOrderItems");
        }
    }
}
