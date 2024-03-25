using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore_DbContext.Migrations
{
    /// <inheritdoc />
    public partial class AddImageFieldToBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "BooksForSale",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "https://images.unsplash.com/photo-1707808512103-23f911fab68a?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHwxNHx8fGVufDB8fHx8fA%3D%3D");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "https://images.unsplash.com/photo-1707808512103-23f911fab68a?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHwxNHx8fGVufDB8fHx8fA%3D%3D");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "BooksForSale");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Books");
        }
    }
}
