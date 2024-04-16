using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore_DbContext.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "M_BookSequence");

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Genre = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PublishingHouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublishingHouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelationshipTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Relationship = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationshipTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "M_BookM_Ganre",
                columns: table => new
                {
                    BooksId = table.Column<int>(type: "int", nullable: false),
                    GanresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M_BookM_Ganre", x => new { x.BooksId, x.GanresId });
                    table.ForeignKey(
                        name: "FK_M_BookM_Ganre_Genres_GanresId",
                        column: x => x.GanresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [M_BookSequence]"),
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfPage = table.Column<int>(type: "int", nullable: false),
                    DateOfPress = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishingHouseId = table.Column<int>(type: "int", nullable: true),
                    AuthorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Books_PublishingHouses_PublishingHouseId",
                        column: x => x.PublishingHouseId,
                        principalTable: "PublishingHouses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BooksForSale",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [M_BookSequence]"),
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfPage = table.Column<int>(type: "int", nullable: false),
                    DateOfPress = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishingHouseId = table.Column<int>(type: "int", nullable: true),
                    AuthorId = table.Column<int>(type: "int", nullable: true),
                    CostPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(4,2)", nullable: false, defaultValue: 0m),
                    IsOnSale = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksForSale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BooksForSale_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BooksForSale_PublishingHouses_PublishingHouseId",
                        column: x => x.PublishingHouseId,
                        principalTable: "PublishingHouses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookRelationships",
                columns: table => new
                {
                    FirstBookId = table.Column<int>(type: "int", nullable: false),
                    SecondBookId = table.Column<int>(type: "int", nullable: false),
                    RelationshipTypeId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookRelationships", x => new { x.FirstBookId, x.SecondBookId });
                    table.ForeignKey(
                        name: "FK_BookRelationships_RelationshipTypes_RelationshipTypeId",
                        column: x => x.RelationshipTypeId,
                        principalTable: "RelationshipTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookForSaleId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseItems_BooksForSale_BookForSaleId",
                        column: x => x.BookForSaleId,
                        principalTable: "BooksForSale",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartBooks",
                columns: table => new
                {
                    BooksId = table.Column<int>(type: "int", nullable: false),
                    ShoppingCartsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartBooks", x => new { x.BooksId, x.ShoppingCartsId });
                    table.ForeignKey(
                        name: "FK_ShoppingCartBooks_BooksForSale_BooksId",
                        column: x => x.BooksId,
                        principalTable: "BooksForSale",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCartBooks_ShoppingCarts_ShoppingCartsId",
                        column: x => x.ShoppingCartsId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookRelationships_RelationshipTypeId",
                table: "BookRelationships",
                column: "RelationshipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BookRelationships_SecondBookId",
                table: "BookRelationships",
                column: "SecondBookId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_DateOfPress",
                table: "Books",
                column: "DateOfPress");

            migrationBuilder.CreateIndex(
                name: "IX_Books_NumberOfPage",
                table: "Books",
                column: "NumberOfPage");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublishingHouseId",
                table: "Books",
                column: "PublishingHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Title",
                table: "Books",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BooksForSale_AuthorId",
                table: "BooksForSale",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_BooksForSale_DateOfPress",
                table: "BooksForSale",
                column: "DateOfPress");

            migrationBuilder.CreateIndex(
                name: "IX_BooksForSale_NumberOfPage",
                table: "BooksForSale",
                column: "NumberOfPage");

            migrationBuilder.CreateIndex(
                name: "IX_BooksForSale_PublishingHouseId",
                table: "BooksForSale",
                column: "PublishingHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_BooksForSale_Title",
                table: "BooksForSale",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_Genre",
                table: "Genres",
                column: "Genre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_M_BookM_Ganre_GanresId",
                table: "M_BookM_Ganre",
                column: "GanresId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartBooks_ShoppingCartsId",
                table: "ShoppingCartBooks",
                column: "ShoppingCartsId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseItems_BookForSaleId",
                table: "WarehouseItems",
                column: "BookForSaleId",
                unique: true);


            // Заполнение таблицы Authors
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "FirstName", "LastName", "DateOfBirth" },
                values: new object[,]
                {
                    { "George", "Orwell", new DateTime(1903, 6, 25) },
                    { "Leo", "Tolstoy", new DateTime(1828, 9, 9) },
                    { "Jane", "Austen", new DateTime(1775, 12, 16) },
                    { "Ernest", "Hemingway", new DateTime(1899, 7, 21) },
                    { "Fyodor", "Dostoevsky", new DateTime(1821, 11, 11) },
                    { "Mark", "Twain", new DateTime(1835, 11, 30) },
                    { "Charles", "Dickens", new DateTime(1812, 2, 7) },
                    { "Gabriel", "García Márquez", new DateTime(1927, 3, 6) },
                    { "Agatha", "Christie", new DateTime(1890, 9, 15) },
                    { "Toni", "Morrison", new DateTime(1931, 2, 18) },
                    { "Haruki", "Murakami", new DateTime(1949, 1, 12) },
                    { "Virginia", "Woolf", new DateTime(1882, 1, 25) },
                    { "J.R.R.", "Tolkien", new DateTime(1892, 1, 3) },
                    { "Herman", "Melville", new DateTime(1819, 8, 1) },
                    { "Franz", "Kafka", new DateTime(1883, 7, 3) }
                });


            // Заполнение таблицы PublishingHouses
            migrationBuilder.InsertData(
                table: "PublishingHouses",
                columns: new[] { "Name" },
                values: new object[,]
                {
                    { "HarperCollins" },
                    { "Simon & Schuster" },
                    { "Hachette Livre" },
                    { "Macmillan Publishers" },
                    { "Pearson Education" },
                    { "Scholastic Corporation" },
                    { "Oxford University Press" },
                    { "Cambridge University Press" },
                    { "Wiley" },
                    { "Elsevier" },
                    { "Taylor & Francis" },
                    { "Springer Nature" },
                    { "Cengage Learning" },
                    { "Thomson Reuters" },
                    { "John Wiley & Sons" }
                });

            // Заполнение таблицы Genres
            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Genre" },
                values: new object[,]
                {
                    { "Science Fiction" },
                    { "Mystery" },
                    { "Romance" },
                    { "Thriller" },
                    { "Adventure" },
                    { "Historical Fiction" },
                    { "Young Adult" },
                    { "Biography" },
                    { "Memoir" },
                    { "Self-Help" },
                    { "Cookbook" },
                    { "Travel" },
                    { "Poetry" },
                    { "Art" },
                    { "Music" }
                });

            // Заполнение таблицы RelationshipTypes
            migrationBuilder.InsertData(
                table: "RelationshipTypes",
                columns: new[] { "Relationship" },
                values: new object[,]
                {
                    { "continue" },
                    { "related" },
                    { "sequel" },
                    { "prequel" },
                    { "spin-off" },
                    { "adaptation" },
                    { "companion" },
                    { "anthology" },
                    { "box set" },
                    { "collection" },
                    { "omnibus" },
                    { "series" },
                    { "trilogy" },
                    { "quartet" },
                    { "duology" },
                    { "standalone" }
                });

            // Заполнение таблицы Users
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Username", "Password" },
                values: new object[,]
                {
                    { "user1", "password1" },
                    { "user2", "password2" },
                });



            // Заполнение таблицы BooksForSale
            migrationBuilder.InsertData(
                table: "BooksForSale",
                columns: new[] { "Title", "Description", "NumberOfPage", "DateOfPress", "PublishingHouseId", "AuthorId", "CostPrice", "SellingPrice", "Discount", "IsOnSale" },
                values: new object[,]
                {
                    // George Orwell
                    { "1984", "A dystopian novel by George Orwell", 328, new DateTime(1949, 6, 8), 1, 3, 10.99m, 19.99m, 0m, true },
                    { "Animal Farm", "An allegorical novella by George Orwell", 141, new DateTime(1945, 8, 17), 1, 3, 12.99m, 24.99m, 0m, true },
                    { "Keep the Aspidistra Flying", "A novel by George Orwell", 302, new DateTime(1936, 4, 20), 1, 3, 9.99m, 18.99m, 0m, true },
                    { "Homage to Catalonia", "A memoir by George Orwell", 319, new DateTime(1938, 4, 25), 1, 3, 11.99m, 21.99m, 0m, true },

                    // Leo Tolstoy
                    { "War and Peace", "An epic novel by Leo Tolstoy", 1225, new DateTime(1869, 1, 1), 2, 4, 14.99m, 27.99m, 0m, true },
                    { "Anna Karenina", "A realist novel by Leo Tolstoy", 964, new DateTime(1877, 1, 1), 2, 4, 13.99m, 26.99m, 0m, true },
                    { "Resurrection", "A novel by Leo Tolstoy", 510, new DateTime(1899, 1, 1), 2, 4, 8.99m, 17.99m, 0m, true },
                    { "The Death of Ivan Ilyich", "A novella by Leo Tolstoy", 80, new DateTime(1886, 1, 1), 2, 4, 10.99m, 19.99m, 0m, true },

                    // Jane Austen
                    { "Pride and Prejudice", "A romantic novel by Jane Austen", 279, new DateTime(1813, 1, 28), 3, 5, 12.99m, 24.99m, 0m, true },
                    { "Sense and Sensibility", "A romantic novel by Jane Austen", 409, new DateTime(1811, 10, 30), 3, 5, 9.99m, 18.99m, 0m, true },
                    { "Emma", "A novel by Jane Austen", 474, new DateTime(1815, 1, 1), 3, 5, 11.99m, 21.99m, 0m, true },
                    { "Mansfield Park", "A novel by Jane Austen", 507, new DateTime(1814, 1, 1), 3, 5, 14.99m, 27.99m, 0m, true },

                    // Ernest Hemingway
                    { "The Old Man and the Sea", "A novel by Ernest Hemingway", 127, new DateTime(1952, 9, 1), 4, 6, 13.99m, 26.99m, 0m, true },
                    { "For Whom the Bell Tolls", "A novel by Ernest Hemingway", 480, new DateTime(1940, 10, 21), 4, 6, 8.99m, 17.99m, 0m, true },
                    { "A Farewell to Arms", "A novel by Ernest Hemingway", 332, new DateTime(1929, 9, 27), 4, 6, 10.99m, 19.99m, 0m, true },
                    { "The Sun Also Rises", "A novel by Ernest Hemingway", 251, new DateTime(1926, 10, 22), 4, 6, 12.99m, 24.99m, 0m, true },

                    // Fyodor Dostoevsky
                    { "Crime and Punishment", "A novel by Fyodor Dostoevsky", 551, new DateTime(1866, 12, 22), 5, 7, 9.99m, 18.99m, 0m, true },
                    { "The Brothers Karamazov", "A novel by Fyodor Dostoevsky", 796, new DateTime(1880, 11, 6), 5, 7, 11.99m, 21.99m, 0m, true },
                    { "The Idiot", "A novel by Fyodor Dostoevsky", 667, new DateTime(1869, 1, 1), 5, 7, 14.99m, 27.99m, 0m, true },
                    { "The Gambler", "A novella by Fyodor Dostoevsky", 206, new DateTime(1867, 1, 1), 5, 7, 13.99m, 26.99m, 0m, true },

                    // Mark Twain
                    { "The Adventures of Tom Sawyer", "A novel by Mark Twain", 219, new DateTime(1876, 12, 1), 6, 8, 8.99m, 17.99m, 0m, true },
                    { "Adventures of Huckleberry Finn", "A novel by Mark Twain", 366, new DateTime(1884, 12, 10), 6, 8, 10.99m, 19.99m, 0m, true },
                    { "The Prince and the Pauper", "A novel by Mark Twain", 217, new DateTime(1881, 12, 1), 6, 8, 12.99m, 24.99m, 0m, true },
                    { "A Connecticut Yankee in King Arthur's Court", "A novel by Mark Twain", 453, new DateTime(1889, 12, 18), 6, 8, 9.99m, 18.99m, 0m, true },

                    // Charles Dickens
                    { "A Tale of Two Cities", "A novel by Charles Dickens", 448, new DateTime(1859, 11, 26), 7, 9, 11.99m, 21.99m, 0m, true },
                    { "Great Expectations", "A novel by Charles Dickens", 544, new DateTime(1861, 8, 1), 7, 9, 14.99m, 27.99m, 0m, true },
                    { "David Copperfield", "A novel by Charles Dickens", 882, new DateTime(1850, 5, 1), 7, 9, 13.99m, 26.99m, 0m, true },
                    { "Oliver Twist", "A novel by Charles Dickens", 554, new DateTime(1837, 11, 1), 7, 9, 8.99m, 17.99m, 0m, true },

                    // Gabriel García Márquez
                    { "One Hundred Years of Solitude", "A novel by Gabriel García Márquez", 417, new DateTime(1967, 5, 30), 8, 10, 10.99m, 19.99m, 0m, true },
                    { "Love in the Time of Cholera", "A novel by Gabriel García Márquez", 348, new DateTime(1985, 1, 8), 8, 10, 12.99m, 24.99m, 0m, true },
                    { "Chronicle of a Death Foretold", "A novella by Gabriel García Márquez", 120, new DateTime(1981, 1, 7), 8, 10, 9.99m, 18.99m, 0m, true },
                    { "Love and Other Demons", "A novel by Gabriel García Márquez", 147, new DateTime(1994, 3, 22), 8, 10, 11.99m, 21.99m, 0m, true },

                    // J.R.R. Tolkien
                    { "The Hobbit", "A fantasy novel by J.R.R. Tolkien", 310, new DateTime(1937, 9, 21), 12, 13, 14.99m, 27.99m, 0m, true },
                    { "The Lord of the Rings", "A fantasy novel by J.R.R. Tolkien", 1178, new DateTime(1954, 7, 29), 12, 13, 13.99m, 26.99m, 0m, true },
                    { "The Silmarillion", "A fantasy novel by J.R.R. Tolkien", 365, new DateTime(1977, 9, 15), 12, 13, 8.99m, 17.99m, 0m, true },
                    { "The Children of Húrin", "A fantasy novel by J.R.R. Tolkien", 313, new DateTime(2007, 4, 17), 12, 13, 10.99m, 19.99m, 0m, true },

                    // Herman Melville
                    { "Moby-Dick", "A novel by Herman Melville", 641, new DateTime(1851, 10, 18), 13, 14, 12.99m, 24.99m, 0m, true },
                    { "Bartleby, the Scrivener", "A novella by Herman Melville", 64, new DateTime(1853, 11, 14), 13, 14, 9.99m, 18.99m, 0m, true },
                    { "Pierre; or, The Ambiguities", "A novel by Herman Melville", 609, new DateTime(1852, 2, 17), 13, 14, 11.99m, 21.99m, 0m, true },
                    { "Israel Potter: His Fifty Years of Exile", "A novel by Herman Melville", 346, new DateTime(1855, 2, 1), 13, 14, 14.99m, 27.99m, 0m, true },

                    // Franz Kafka
                    { "The Metamorphosis", "A novella by Franz Kafka", 44, new DateTime(1915, 10, 15), 14, 15, 13.99m, 26.99m, 0m, true },
                    { "The Trial", "A novel by Franz Kafka", 213, new DateTime(1925, 4, 26), 14, 15, 8.99m, 17.99m, 0m, true },
                    { "A Hunger Artist", "A short story by Franz Kafka", 32, new DateTime(1924, 10, 11), 14, 15, 10.99m, 19.99m, 0m, true },
                    { "The Castle", "A novel by Franz Kafka", 352, new DateTime(1926, 4, 20), 14, 15, 12.99m, 24.99m, 0m, true },


                    // Agatha Christie
                    { "Murder on the Orient Express", "A mystery novel by Agatha Christie", 322, new DateTime(1934, 1, 1), 9, 11, 9.99m, 18.99m, 0m, true },
                    { "The Murder of Roger Ackroyd", "A mystery novel by Agatha Christie", 312, new DateTime(1926, 6, 1), 9, 11, 11.99m, 21.99m, 0m, true },
                    { "And Then There Were None", "A mystery novel by Agatha Christie", 264, new DateTime(1939, 11, 6), 9, 11, 14.99m, 27.99m, 0m, true },
                    { "Death on the Nile", "A mystery novel by Agatha Christie", 288, new DateTime(1937, 11, 1), 9, 11, 13.99m, 26.99m, 0m, true },

                    // Toni Morrison
                    { "Beloved", "A novel by Toni Morrison", 321, new DateTime(1987, 9, 2), 10, 12, 10.99m, 19.99m, 0m, true },
                    { "Song of Solomon", "A novel by Toni Morrison", 337, new DateTime(1977, 11, 1), 10, 12, 12.99m, 24.99m, 0m, true },
                    { "Sula", "A novel by Toni Morrison", 174, new DateTime(1973, 11, 1), 10, 12, 9.99m, 18.99m, 0m, true },
                    { "Tar Baby", "A novel by Toni Morrison", 318, new DateTime(1981, 1, 1), 10, 12, 11.99m, 21.99m, 0m, true },

                    // Haruki Murakami
                    { "Norwegian Wood", "A novel by Haruki Murakami", 296, new DateTime(1987, 9, 4), 11, 13, 14.99m, 27.99m, 0m, true },
                    { "Kafka on the Shore", "A novel by Haruki Murakami", 467, new DateTime(2002, 9, 12), 11, 13, 13.99m, 26.99m, 0m, true },
                    { "Colorless Tsukuru Tazaki and His Years of Pilgrimage", "A novel by Haruki Murakami", 386, new DateTime(2013, 4, 12), 11, 13, 8.99m, 17.99m, 0m, true },
                    { "1Q84", "A novel by Haruki Murakami", 1157, new DateTime(2009, 5, 29), 11, 13, 10.99m, 19.99m, 0m, true },

                    // Virginia Woolf
                    { "To the Lighthouse", "A novel by Virginia Woolf", 209, new DateTime(1927, 5, 5), 12, 14, 12.99m, 24.99m, 0m, true },
                    { "Mrs Dalloway", "A novel by Virginia Woolf", 194, new DateTime(1925, 5, 14), 12, 14, 9.99m, 18.99m, 0m, true },
                    { "Orlando", "A novel by Virginia Woolf", 354, new DateTime(1928, 10, 11), 12, 14, 11.99m, 21.99m, 0m, true },
                    { "Flush: A Biography", "A novel by Virginia Woolf", 224, new DateTime(1933, 1, 1), 12, 14, 14.99m, 27.99m, 0m, true }
                });

            // Заполнение таблицы ShoppingCarts
            migrationBuilder.InsertData(
                table: "ShoppingCarts",
                columns: new[] { "UserId" },
                values: new object[,]
                {
                    { 1 },
                });


            // Заполнение таблицы M_BookM_Ganre
            migrationBuilder.InsertData(
                table: "M_BookM_Ganre",
                columns: new[] { "BooksId", "GanresId" },
                values: new object[,]
                {
                    // George Orwell
                    { 1, 1 }, // 1984 - dystopian
                    { 1, 6 }, // 1984 - political fiction
                    { 2, 1 }, // Animal Farm - dystopian
                    { 2, 6 }, // Animal Farm - political fiction
                    { 3, 6 }, // Keep the Aspidistra Flying - political fiction
                    { 4, 7 }, // Homage to Catalonia - memoir

                    // Leo Tolstoy
                    { 5, 2 }, // War and Peace - historical
                    { 5, 3 }, // War and Peace - philosophical
                    { 6, 3 }, // Anna Karenina - philosophical
                    { 7, 4 }, // Resurrection - religious
                    { 8, 4 }, // The Death of Ivan Ilyich - religious

                    // Jane Austen
                    { 9, 8 }, // Pride and Prejudice - romantic
                    { 10, 8 }, // Sense and Sensibility - romantic
                    { 11, 9 }, // Emma - comedy of manners
                    { 12, 8 }, // Mansfield Park - romantic

                    // Ernest Hemingway
                    { 13, 10 }, // The Old Man and the Sea - adventure
                    { 14, 10 }, // For Whom the Bell Tolls - adventure
                    { 15, 10 }, // A Farewell to Arms - adventure
                    { 16, 10 }, // The Sun Also Rises - adventure

                    // Fyodor Dostoevsky
                    { 17, 6 }, // Crime and Punishment - psychological
                    { 18, 7 }, // The Brothers Karamazov - philosophical
                    { 19, 6 }, // The Idiot - psychological
                    { 20, 8 }, // The Gambler - satire

                    // Mark Twain
                    { 21, 9 }, // The Adventures of Tom Sawyer - adventure
                    { 21, 10 }, // The Adventures of Tom Sawyer - bildungsroman
                    { 22, 9 }, // Adventures of Huckleberry Finn - adventure
                    { 22, 10 }, // Adventures of Huckleberry Finn - bildungsroman
                    { 23, 10 }, // The Prince and the Pauper - bildungsroman
                    { 24, 10 }, // A Connecticut Yankee in King Arthur's Court - bildungsroman

                    // Charles Dickens
                    { 25, 11 }, // A Tale of Two Cities - historical
                    { 25, 12 }, // A Tale of Two Cities - historical fiction
                    { 26, 10 }, // Great Expectations - bildungsroman
                    { 27, 10 }, // David Copperfield - bildungsroman
                    { 28, 12 }, // Oliver Twist - historical fiction

                    // Gabriel García Márquez
                    { 29, 13 }, // One Hundred Years of Solitude - magical realism
                    { 30, 14 }, // Love in the Time of Cholera - romance
                    { 31, 15 }, // Chronicle of a Death Foretold - mystery
                    { 32, 13 }, // Love and Other Demons - magical realism

                    // Agatha Christie
                    { 33, 2 }, // Murder on the Orient Express - Mystery
                    { 33, 3 }, // Murder on the Orient Express - Detective
                    { 34, 2 }, // The Murder of Roger Ackroyd - Mystery
                    { 34, 3 }, // The Murder of Roger Ackroyd - Detective
                    { 35, 2 }, // And Then There Were None - Mystery
                    { 36, 2 }, // Death on the Nile - Mystery

                    // Toni Morrison
                    { 37, 3 }, // Beloved - Magical Realism
                    { 38, 5 }, // Song of Solomon - Adventure
                    { 39, 5 }, // Sula - Magical Realism
                    { 40, 3 }, // Tar Baby - Magical Realism

                    // Haruki Murakami
                    { 41, 1 }, // Norwegian Wood - Surreal
                    { 42, 2 }, // Kafka on the Shore - Surreal
                    { 43, 6 }, // Colorless Tsukuru Tazaki and His Years of Pilgrimage - Coming-of-Age
                    { 44, 1 }, // 1Q84 - Science Fiction

                    // Virginia Woolf
                    { 45, 14 }, // To the Lighthouse - Modernist
                    { 45, 15 }, // To the Lighthouse - Stream of Consciousness
                    { 46, 14 }, // Mrs Dalloway - Modernist
                    { 46, 15 }, // Mrs Dalloway - Stream of Consciousness
                    { 47, 14 }, // Orlando - Modernist
                    { 48, 14 }, // Flush: A Biography - Modernist

                    // J.R.R. Tolkien
                    { 49, 1 }, // The Hobbit - Science Fiction
                    { 50, 1 }, // The Lord of the Rings - Science Fiction
                    { 51, 1 }, // The Silmarillion - Science Fiction
                    { 52, 1 }, // The Children of Húrin - Science Fiction

                    // Herman Melville
                    { 53, 5 }, // Moby-Dick - Adventure
                    { 54, 8 }, // Bartleby, the Scrivener - Biography
                    { 55, 5 }, // Pierre; or, The Ambiguities - Adventure
                    { 56, 8 }, // Israel Potter: His Fifty Years of Exile - Biography

                    // Franz Kafka
                    { 57, 13 }, // The Metamorphosis - Poetry
                    { 58, 13 }, // The Trial - Poetry
                    { 59, 13 }, // A Hunger Artist - Poetry
                    { 60, 13 }, // The Castle - Poetry
                });

            // Заполнение таблицы M_BookRelationships
            //migrationBuilder.InsertData(
            //    table: "BookRelationships",
            //    columns: new[] { "FirstBookId", "SecondBookId", "RelationshipTypeId" },
            //    values: new object[,]
            //    {
            //        // Продолжение книги "1984"
            //        { 1, 3, 1 }, // 1984 - Keep the Aspidistra Flying: continue

            //        // Продолжение книги "War and Peace"
            //        { 5, 4, 1 }, // War and Peace - Resurrection: continue

            //        // Продолжение книги "Anna Karenina"
            //        { 6, 7, 1 }, // Anna Karenina - The Death of Ivan Ilyich: continue

            //        // Продолжение книги "The Lord of the Rings"
            //        { 27, 28, 1 }, // The Lord of the Rings - The Silmarillion: continue
            //    });

            // Заполнение таблицы M_BookForSaleM_ShoppingCart
            migrationBuilder.InsertData(
                table: "ShoppingCartBooks",
                columns: new[] { "BooksId", "ShoppingCartsId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 }
                });

            // Заполнение таблицы WarehouseItems
            migrationBuilder.InsertData(
                table: "WarehouseItems",
                columns: new[] { "BookForSaleId", "Quantity" },
                values: new object[,]
                {
                    { 1, 100 },
                    { 2, 150 },
                    { 3, 200 },
                    { 4, 250 },
                    { 5, 300 },
                    { 6, 350 },
                    { 7, 400 },
                    { 8, 450 },
                    { 9, 500 },
                    { 10, 550 },
                    { 11, 600 },
                    { 12, 650 },
                    { 13, 700 },
                    { 14, 750 },
                    { 15, 800 },
                    { 16, 850 },
                    { 17, 900 },
                    { 18, 950 },
                    { 19, 1000 },
                    { 20, 1050 },
                    { 21, 1100 },
                    { 22, 1150 },
                    { 23, 1200 },
                    { 24, 1250 },
                    { 25, 1300 },
                    { 26, 1350 },
                    { 27, 1400 },
                    { 28, 1450 },
                    { 29, 1500 },
                    { 30, 1550 },
                    { 31, 1600 },
                    { 32, 1650 },
                    { 33, 1700 },
                    { 34, 1750 },
                    { 35, 1800 },
                    { 36, 1850 },
                    { 37, 1900 },
                    { 38, 1950 },
                    { 39, 2000 },
                    { 40, 2050 },
                    { 41, 2100 },
                    { 42, 2150 },
                    { 43, 2200 },
                    { 44, 2250 },
                    { 45, 2300 },
                    { 46, 2350 },
                    { 47, 2400 },
                    { 48, 2450 },
                    { 49, 2500 },
                    { 50, 2550 },
                    { 51, 2600 },
                    { 52, 2650 },
                    { 53, 2700 },
                    { 54, 2750 },
                    { 55, 2800 },
                    { 56, 2850 },
                    { 57, 2900 },
                    { 58, 2950 },
                    { 59, 3000 },
                    { 60, 3050 },
                });


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookRelationships");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "M_BookM_Ganre");

            migrationBuilder.DropTable(
                name: "ShoppingCartBooks");

            migrationBuilder.DropTable(
                name: "WarehouseItems");

            migrationBuilder.DropTable(
                name: "RelationshipTypes");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "BooksForSale");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "PublishingHouses");

            migrationBuilder.DropSequence(
                name: "M_BookSequence");
        }
    }
}
