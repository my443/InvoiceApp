using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVendorToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Vendors_VendorId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_VendorId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "VendorId",
                table: "Expenses");

            migrationBuilder.AddColumn<string>(
                name: "Vendor",
                table: "Expenses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vendor",
                table: "Expenses");

            migrationBuilder.AddColumn<int>(
                name: "VendorId",
                table: "Expenses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_VendorId",
                table: "Expenses",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Vendors_VendorId",
                table: "Expenses",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
