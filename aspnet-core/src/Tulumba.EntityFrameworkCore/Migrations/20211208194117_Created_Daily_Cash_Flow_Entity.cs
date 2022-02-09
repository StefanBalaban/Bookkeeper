using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tulumba.Migrations
{
    public partial class Created_Daily_Cash_Flow_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppDailyCashFlows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ShopId = table.Column<Guid>(type: "uuid", nullable: false),
                    MonthlyCashFlowId = table.Column<Guid>(type: "uuid", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDailyCashFlows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppDailyCashFlows_AppMonthlyCashFlows_MonthlyCashFlowId",
                        column: x => x.MonthlyCashFlowId,
                        principalTable: "AppMonthlyCashFlows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppDailyCashFlows_AppShops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "AppShops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppDailyCashFlows_MonthlyCashFlowId",
                table: "AppDailyCashFlows",
                column: "MonthlyCashFlowId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDailyCashFlows_ShopId",
                table: "AppDailyCashFlows",
                column: "ShopId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppDailyCashFlows");
        }
    }
}
