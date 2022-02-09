using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tulumba.Migrations
{
    public partial class Remove_Full_Audited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppMonthlyCashFlows");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppMonthlyCashFlows");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppMonthlyCashFlows");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppExpenseTypes");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppExpenseTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppExpenseTypes");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppExpenses");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppExpenses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppExpenses");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppEmployees");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppEmployees");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppEmployees");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppDailyEarnings");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppDailyEarnings");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppDailyEarnings");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppDailyCashFlows");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppDailyCashFlows");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppDailyCashFlows");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AppMonthlyCashFlows",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppMonthlyCashFlows",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppMonthlyCashFlows",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AppExpenseTypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppExpenseTypes",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppExpenseTypes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AppExpenses",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppExpenses",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppExpenses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AppEmployees",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppEmployees",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppEmployees",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AppDailyEarnings",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppDailyEarnings",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppDailyEarnings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AppDailyCashFlows",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppDailyCashFlows",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppDailyCashFlows",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
