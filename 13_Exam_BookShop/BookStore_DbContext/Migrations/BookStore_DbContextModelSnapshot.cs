﻿// <auto-generated />
using System;
using BookStore_DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookStore_DbContext.Migrations
{
    [DbContext(typeof(BookStore_DbContext))]
    partial class BookStore_DbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookStore_DbContext.Models.M_Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("BookStore_DbContext.Models.M_Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AuthorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfPress")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfPage")
                        .HasColumnType("int");

                    b.Property<int?>("PublishingHouseId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("DateOfPress");

                    b.HasIndex("NumberOfPage");

                    b.HasIndex("PublishingHouseId");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BookStore_DbContext.Models.M_BookForSale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<decimal>("CostPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("Discount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(3, 2)")
                        .HasDefaultValue(0m);

                    b.Property<bool>("IsOnSale")
                        .HasColumnType("bit");

                    b.Property<decimal>("SellingPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("BookId")
                        .IsUnique();

                    b.ToTable("BooksForSale");
                });

            modelBuilder.Entity("BookStore_DbContext.Models.M_BookRelationship", b =>
                {
                    b.Property<int>("FirstBookId")
                        .HasColumnType("int");

                    b.Property<int>("SecondBookId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("RelationshipTypeId")
                        .HasColumnType("int");

                    b.HasKey("FirstBookId", "SecondBookId");

                    b.HasIndex("RelationshipTypeId");

                    b.HasIndex("SecondBookId");

                    b.ToTable("BookRelationships");
                });

            modelBuilder.Entity("BookStore_DbContext.Models.M_Ganre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Genre")
                        .IsUnique();

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("BookStore_DbContext.Models.M_PublishingHouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PublishingHouses");
                });

            modelBuilder.Entity("BookStore_DbContext.Models.M_RelationshipType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Relationship")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RelationshipTypes");
                });

            modelBuilder.Entity("BookStore_DbContext.Models.M_ShoppingCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("BookStore_DbContext.Models.M_User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BookStore_DbContext.Models.M_WarehouseItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookForSaleId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookForSaleId")
                        .IsUnique();

                    b.ToTable("WarehouseItems");
                });

            modelBuilder.Entity("M_BookForSaleM_ShoppingCart", b =>
                {
                    b.Property<int>("BooksId")
                        .HasColumnType("int");

                    b.Property<int>("ShoppingCartsId")
                        .HasColumnType("int");

                    b.HasKey("BooksId", "ShoppingCartsId");

                    b.HasIndex("ShoppingCartsId");

                    b.ToTable("ShoppingCartBooks", (string)null);
                });

            modelBuilder.Entity("M_BookM_Ganre", b =>
                {
                    b.Property<int>("BooksId")
                        .HasColumnType("int");

                    b.Property<int>("GanresId")
                        .HasColumnType("int");

                    b.HasKey("BooksId", "GanresId");

                    b.HasIndex("GanresId");

                    b.ToTable("M_BookM_Ganre");
                });

            modelBuilder.Entity("BookStore_DbContext.Models.M_Book", b =>
                {
                    b.HasOne("BookStore_DbContext.Models.M_Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId");

                    b.HasOne("BookStore_DbContext.Models.M_PublishingHouse", "PublishingHouse")
                        .WithMany("Books")
                        .HasForeignKey("PublishingHouseId");

                    b.Navigation("Author");

                    b.Navigation("PublishingHouse");
                });

            modelBuilder.Entity("BookStore_DbContext.Models.M_BookForSale", b =>
                {
                    b.HasOne("BookStore_DbContext.Models.M_Book", "Book")
                        .WithOne("BookForSale")
                        .HasForeignKey("BookStore_DbContext.Models.M_BookForSale", "BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("BookStore_DbContext.Models.M_BookRelationship", b =>
                {
                    b.HasOne("BookStore_DbContext.Models.M_Book", "FirstBook")
                        .WithMany("RelatedBooks")
                        .HasForeignKey("FirstBookId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BookStore_DbContext.Models.M_RelationshipType", "RelationshipType")
                        .WithMany("BookRelationships")
                        .HasForeignKey("RelationshipTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookStore_DbContext.Models.M_Book", "SecondBook")
                        .WithMany()
                        .HasForeignKey("SecondBookId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FirstBook");

                    b.Navigation("RelationshipType");

                    b.Navigation("SecondBook");
                });

            modelBuilder.Entity("BookStore_DbContext.Models.M_ShoppingCart", b =>
                {
                    b.HasOne("BookStore_DbContext.Models.M_User", "User")
                        .WithMany("ShoppingCarts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookStore_DbContext.Models.M_WarehouseItem", b =>
                {
                    b.HasOne("BookStore_DbContext.Models.M_BookForSale", "BookForSale")
                        .WithOne("WarehouseItem")
                        .HasForeignKey("BookStore_DbContext.Models.M_WarehouseItem", "BookForSaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookForSale");
                });

            modelBuilder.Entity("M_BookForSaleM_ShoppingCart", b =>
                {
                    b.HasOne("BookStore_DbContext.Models.M_BookForSale", null)
                        .WithMany()
                        .HasForeignKey("BooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookStore_DbContext.Models.M_ShoppingCart", null)
                        .WithMany()
                        .HasForeignKey("ShoppingCartsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("M_BookM_Ganre", b =>
                {
                    b.HasOne("BookStore_DbContext.Models.M_Book", null)
                        .WithMany()
                        .HasForeignKey("BooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookStore_DbContext.Models.M_Ganre", null)
                        .WithMany()
                        .HasForeignKey("GanresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookStore_DbContext.Models.M_Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("BookStore_DbContext.Models.M_Book", b =>
                {
                    b.Navigation("BookForSale")
                        .IsRequired();

                    b.Navigation("RelatedBooks");
                });

            modelBuilder.Entity("BookStore_DbContext.Models.M_BookForSale", b =>
                {
                    b.Navigation("WarehouseItem")
                        .IsRequired();
                });

            modelBuilder.Entity("BookStore_DbContext.Models.M_PublishingHouse", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("BookStore_DbContext.Models.M_RelationshipType", b =>
                {
                    b.Navigation("BookRelationships");
                });

            modelBuilder.Entity("BookStore_DbContext.Models.M_User", b =>
                {
                    b.Navigation("ShoppingCarts");
                });
#pragma warning restore 612, 618
        }
    }
}
