using Microsoft.EntityFrameworkCore.Migrations;

namespace Tulumba.Migrations
{
    public partial class Add_Amount_To_Expenses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "AppExpenses",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "AppExpenses");
        }
    }
}
