using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tulumba.Migrations
{
    public partial class Add_Employee_To_Expense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "AppExpenses",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppExpenses_DailyCashFlowId",
                table: "AppExpenses",
                column: "DailyCashFlowId");

            migrationBuilder.CreateIndex(
                name: "IX_AppExpenses_EmployeeId",
                table: "AppExpenses",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppExpenses_MonthlyCashFlowId",
                table: "AppExpenses",
                column: "MonthlyCashFlowId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDailyEarnings_DailyCashFlowId",
                table: "AppDailyEarnings",
                column: "DailyCashFlowId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDailyEarnings_AppDailyCashFlows_DailyCashFlowId",
                table: "AppDailyEarnings",
                column: "DailyCashFlowId",
                principalTable: "AppDailyCashFlows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppExpenses_AppDailyCashFlows_DailyCashFlowId",
                table: "AppExpenses",
                column: "DailyCashFlowId",
                principalTable: "AppDailyCashFlows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppExpenses_AppEmployees_EmployeeId",
                table: "AppExpenses",
                column: "EmployeeId",
                principalTable: "AppEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppExpenses_AppMonthlyCashFlows_MonthlyCashFlowId",
                table: "AppExpenses",
                column: "MonthlyCashFlowId",
                principalTable: "AppMonthlyCashFlows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDailyEarnings_AppDailyCashFlows_DailyCashFlowId",
                table: "AppDailyEarnings");

            migrationBuilder.DropForeignKey(
                name: "FK_AppExpenses_AppDailyCashFlows_DailyCashFlowId",
                table: "AppExpenses");

            migrationBuilder.DropForeignKey(
                name: "FK_AppExpenses_AppEmployees_EmployeeId",
                table: "AppExpenses");

            migrationBuilder.DropForeignKey(
                name: "FK_AppExpenses_AppMonthlyCashFlows_MonthlyCashFlowId",
                table: "AppExpenses");

            migrationBuilder.DropIndex(
                name: "IX_AppExpenses_DailyCashFlowId",
                table: "AppExpenses");

            migrationBuilder.DropIndex(
                name: "IX_AppExpenses_EmployeeId",
                table: "AppExpenses");

            migrationBuilder.DropIndex(
                name: "IX_AppExpenses_MonthlyCashFlowId",
                table: "AppExpenses");

            migrationBuilder.DropIndex(
                name: "IX_AppDailyEarnings_DailyCashFlowId",
                table: "AppDailyEarnings");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "AppExpenses");
        }
    }
}
