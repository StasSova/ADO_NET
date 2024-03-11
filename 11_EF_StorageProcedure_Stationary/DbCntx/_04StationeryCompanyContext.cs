using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DbCntx;

public partial class _04StationeryCompanyContext : DbContext
{
    public _04StationeryCompanyContext()
    {
    }

    public _04StationeryCompanyContext(DbContextOptions<_04StationeryCompanyContext> options)
        : base(options)
    {

    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CompanyArchive> CompanyArchives { get; set; }

    public virtual DbSet<HistoryOfSell> HistoryOfSells { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<ItemArchive> ItemArchives { get; set; }

    public virtual DbSet<Manager> Managers { get; set; }

    public virtual DbSet<ManagerArchive> ManagerArchives { get; set; }

    public virtual DbSet<TypeOfItem> TypeOfItems { get; set; }

    public virtual DbSet<TypeOfItemArchive> TypeOfItemArchives { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
        .UseLazyLoadingProxies()
        .UseSqlServer("Server=localhost\\MSSQLSERVER01;Database=04_StationeryCompany;Integrated Security=SSPI;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Company__3214EC0764D2E735");

            entity.ToTable("Company", tb => tb.HasTrigger("Company_DeleteTrigger"));

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<CompanyArchive>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CompanyA__3214EC07DE86F6BB");

            entity.ToTable("CompanyArchive");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<HistoryOfSell>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HistoryO__3214EC076F87F284");

            entity.Property(e => e.Date).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ManagerId).HasColumnName("ManagerID");

            entity.HasOne(d => d.BuyersCompanyNavigation).WithMany(p => p.HistoryOfSells)
                .HasForeignKey(d => d.BuyersCompany)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__HistoryOf__Buyer__5070F446");

            entity.HasOne(d => d.Item).WithMany(p => p.HistoryOfSells)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__HistoryOf__ItemI__4E88ABD4");

            entity.HasOne(d => d.Manager).WithMany(p => p.HistoryOfSells)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__HistoryOf__Manag__4F7CD00D");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Item__3214EC07700AD2D6");

            entity.ToTable("Item", tb =>
                {
                    tb.HasTrigger("Item_DeleteTrigger");
                    tb.HasTrigger("UpdateTypeItemCount");
                    tb.HasTrigger("UpdateTypeItemCount_Delete");
                    tb.HasTrigger("UpdateTypeItemCount_Update");
                });

            entity.Property(e => e.CostPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TypeId).HasColumnName("Type_ID");

            entity.HasOne(d => d.Type).WithMany(p => p.Items)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Item__Type_ID__46E78A0C");
        });

        modelBuilder.Entity<ItemArchive>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ItemArch__3214EC07818A170E");

            entity.ToTable("ItemArchive");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CostPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TypeId).HasColumnName("Type_ID");
        });

        modelBuilder.Entity<Manager>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Manager__3214EC07FEA9D02E");

            entity.ToTable("Manager", tb => tb.HasTrigger("Manager_DeleteTrigger"));

            entity.Property(e => e.Fname)
                .HasMaxLength(100)
                .HasColumnName("FName");
            entity.Property(e => e.Lname)
                .HasMaxLength(100)
                .HasColumnName("LName");
        });

        modelBuilder.Entity<ManagerArchive>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ManagerA__3214EC073D7487EB");

            entity.ToTable("ManagerArchive");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Fname)
                .HasMaxLength(25)
                .HasColumnName("FName");
            entity.Property(e => e.Lname)
                .HasMaxLength(25)
                .HasColumnName("LName");
        });

        modelBuilder.Entity<TypeOfItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TypeOfIt__3214EC0708D63633");

            entity.ToTable("TypeOfItem", tb => tb.HasTrigger("TypeOfItem_DeleteTrigger"));

            entity.HasIndex(e => e.Type, "UQ__TypeOfIt__F9B8A48B3C78004D").IsUnique();

            entity.Property(e => e.Count).HasDefaultValue(0);
            entity.Property(e => e.Type).HasMaxLength(100);
        });

        modelBuilder.Entity<TypeOfItemArchive>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TypeOfIt__3214EC070524FCC5");

            entity.ToTable("TypeOfItemArchive");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Type).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
