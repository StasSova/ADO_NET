using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore_DbContext.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscountFieldToBookForSale_Def_0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "BooksForSale",
                type: "decimal(3,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "BooksForSale");
        }
    }
}
