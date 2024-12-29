using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceApp.Migrations
{
    /// <inheritdoc />
    public partial class RemoveApprovalNote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Approvals_ApprovalId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_ApprovalId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "ApprovalId",
                table: "Notes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApprovalId",
                table: "Notes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_ApprovalId",
                table: "Notes",
                column: "ApprovalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Approvals_ApprovalId",
                table: "Notes",
                column: "ApprovalId",
                principalTable: "Approvals",
                principalColumn: "Id");
        }
    }
}
