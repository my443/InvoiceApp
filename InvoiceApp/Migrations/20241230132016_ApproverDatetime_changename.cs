using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceApp.Migrations
{
    /// <inheritdoc />
    public partial class ApproverDatetime_changename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Approvals_Expenses_ExpenseId",
                table: "Approvals");

            migrationBuilder.RenameColumn(
                name: "DateApproved",
                table: "Approvals",
                newName: "LastUpdated");

            migrationBuilder.AlterColumn<int>(
                name: "ExpenseId",
                table: "Approvals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Approvals_Expenses_ExpenseId",
                table: "Approvals",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Approvals_Expenses_ExpenseId",
                table: "Approvals");

            migrationBuilder.RenameColumn(
                name: "LastUpdated",
                table: "Approvals",
                newName: "DateApproved");

            migrationBuilder.AlterColumn<int>(
                name: "ExpenseId",
                table: "Approvals",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Approvals_Expenses_ExpenseId",
                table: "Approvals",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id");
        }
    }
}
