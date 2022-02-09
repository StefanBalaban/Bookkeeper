using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tulumba.Migrations
{
    public partial class Added_ShopId_To_Employee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShopId",
                table: "AppEmployees",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppEmployees_ShopId",
                table: "AppEmployees",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppEmployees_AppShops_ShopId",
                table: "AppEmployees",
                column: "ShopId",
                principalTable: "AppShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppEmployees_AppShops_ShopId",
                table: "AppEmployees");

            migrationBuilder.DropIndex(
                name: "IX_AppEmployees_ShopId",
                table: "AppEmployees");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "AppEmployees");
        }
    }
}
