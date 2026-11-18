using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expenses.Api.Migrations
{
    /// <inheritdoc />
    public partial class Pascalcasefixindb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Expenses",
                table: "Expenses");

            migrationBuilder.RenameTable(
                name: "Expenses",
                newName: "expenses");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "expenses",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "expenses",
                newName: "currency");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "expenses",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "expenses",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ExpenseDate",
                table: "expenses",
                newName: "expense_date");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "expenses",
                newName: "created_at");

            migrationBuilder.AddPrimaryKey(
                name: "pk_expenses",
                table: "expenses",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_expenses",
                table: "expenses");

            migrationBuilder.RenameTable(
                name: "expenses",
                newName: "Expenses");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Expenses",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "currency",
                table: "Expenses",
                newName: "Currency");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "Expenses",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Expenses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "expense_date",
                table: "Expenses",
                newName: "ExpenseDate");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Expenses",
                newName: "CreatedAt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Expenses",
                table: "Expenses",
                column: "Id");
        }
    }
}
