using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceApp.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeConnectionToUser1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Employees_EmployeeId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EmployeeId",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EmployeeId",
                table: "AspNetUsers",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Employees_EmployeeId",
                table: "AspNetUsers",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
