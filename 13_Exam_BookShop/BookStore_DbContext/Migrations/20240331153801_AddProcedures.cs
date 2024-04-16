using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore_DbContext.Migrations
{
    /// <inheritdoc />
    public partial class AddProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Получение всех книг, которые по акции
            migrationBuilder.Sql(
                @"
                CREATE PROCEDURE [dbo].[GetBooksOnSale]
                AS
                BEGIN
                    SELECT * FROM BooksForSale WHERE IsOnSale = 1;
                END
                ");

            // 2. Получение всех книг по автору
            migrationBuilder.Sql(
                @"
                CREATE PROCEDURE [dbo].[GetBooksByAuthor]
                    @AuthorId INT
                AS
                BEGIN
                    SELECT * FROM BooksForSale WHERE AuthorId = @AuthorId;
                END
                ");

            // 3. Получение всех книг по жанру
            migrationBuilder.Sql(
                @"
                CREATE PROCEDURE [dbo].[GetBooksByGenre]
                    @GenreId INT
                AS
                BEGIN
                    SELECT * FROM BooksForSale 
                    INNER JOIN M_BookM_Ganre ON BooksForSale.Id = M_BookM_Ganre.BooksId 
                    WHERE M_BookM_Ganre.GanresId = @GenreId;
                END
                ");

            // 4. Получение книг по странице и по количеству книг на одной страничке (для пагинации)
            migrationBuilder.Sql(
                @"
                CREATE PROCEDURE [dbo].[GetBooksByPage]
                    @Page INT,
                    @PerPage INT
                AS
                BEGIN
                    SELECT * FROM BooksForSale 
                    ORDER BY Id OFFSET (@Page - 1) * @PerPage ROWS FETCH NEXT @PerPage ROWS ONLY;
                END
                ");

            // 5. Получение книг по названию (по частичному и по полному, в зависимости от параметра true/false)
            migrationBuilder.Sql(
                @"
                CREATE PROCEDURE [dbo].[GetBooksByTitle]
                    @Title NVARCHAR(MAX),
                    @ExactMatch BIT
                AS
                BEGIN
                    IF @ExactMatch = 1
                        SELECT * FROM BooksForSale WHERE Title = @Title;
                    ELSE
                        SELECT * FROM BooksForSale WHERE Title LIKE '%' + @Title + '%';
                END
                ");

            // 6. Получение последних добавленных книг
            migrationBuilder.Sql(
                @"
                CREATE PROCEDURE [dbo].[GetRecentAddedBooks]
                    @TopCount INT
                AS
                BEGIN
                    SELECT TOP(@TopCount) * FROM BooksForSale ORDER BY DateOfPress DESC;
                END
                ");

            // 7. Получение самых популярных книг
            migrationBuilder.Sql(
                @"
                CREATE PROCEDURE [dbo].[GetPopularBooks]
                    @TopCount INT
                AS
                BEGIN
                    SELECT TOP(@TopCount) * FROM BooksForSale 
                    ORDER BY (SELECT COUNT(*) FROM ShoppingCartBooks WHERE ShoppingCartBooks.BooksId = BooksForSale.Id) DESC;
                END
                ");

            // 8. Списки популярных жанров по итогам дня, недели, месяца, года
            migrationBuilder.Sql(
                @"
                CREATE PROCEDURE [dbo].[GetPopularGenresByPeriod]
                    @StartDate DATETIME,
                    @EndDate DATETIME
                AS
                BEGIN
                    SELECT GanresId, COUNT(*) AS GenreCount FROM M_BookM_Ganre
                    INNER JOIN BooksForSale ON M_BookM_Ganre.BooksId = BooksForSale.Id
                    WHERE DateOfPress BETWEEN @StartDate AND @EndDate
                    GROUP BY GanresId;
                END
                ");
                }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Удаление процедур
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[GetBooksOnSale];");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[GetBooksByAuthor];");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[GetBooksByGenre];");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[GetBooksByPage];");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[GetBooksByTitle];");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[GetRecentAddedBooks];");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[GetPopularBooks];");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[GetPopularGenresByPeriod];");
        }

    }
}
