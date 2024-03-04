using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DB_Context;

public partial class AuthorAndBookContext : DbContext
{
    public AuthorAndBookContext()
    {

    }

    public AuthorAndBookContext(DbContextOptions<AuthorAndBookContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder
        .UseLazyLoadingProxies()
        .UseSqlServer("Server=localhost\\MSSQLSERVER01;Database=AuthorAndBook;Integrated Security=SSPI;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Authors__3214EC07476331F5");

            entity.HasIndex(e => e.FullName, "UQ__Authors__89C60F110AC7A07F").IsUnique();

            entity.Property(e => e.FullName).HasMaxLength(30);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Books__3214EC074CE6DF3D");

            entity.Property(e => e.Name).HasMaxLength(60);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK__Books__AuthorId__3A81B327")
                .OnDelete(DeleteBehavior.Cascade);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
