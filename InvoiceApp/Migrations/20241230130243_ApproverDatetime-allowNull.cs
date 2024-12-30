using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceApp.Migrations
{
    /// <inheritdoc />
    public partial class ApproverDatetimeallowNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Notes_Expenses_ExpenseId",
            //    table: "Notes");

            //migrationBuilder.AlterColumn<int>(
            //    name: "ExpenseId",
            //    table: "Notes",
            //    type: "INTEGER",
            //    nullable: false,
            //    defaultValue: 0,
            //    oldClrType: typeof(int),
            //    oldType: "INTEGER",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "DateApproved",
            //    table: "Approvals",
            //    type: "TEXT",
            //    nullable: true,
            //    oldClrType: typeof(DateTime),
            //    oldType: "TEXT");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Notes_Expenses_ExpenseId",
            //    table: "Notes",
            //    column: "ExpenseId",
            //    principalTable: "Expenses",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Expenses_ExpenseId",
                table: "Notes");

            migrationBuilder.AlterColumn<int>(
                name: "ExpenseId",
                table: "Notes",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateApproved",
                table: "Approvals",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Expenses_ExpenseId",
                table: "Notes",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id");
        }
    }
}
