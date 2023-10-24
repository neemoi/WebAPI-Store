﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAPIKurs;

public partial class WebsellContext : IdentityDbContext<User>
{
    public WebsellContext() { }

    public WebsellContext(DbContextOptions<WebsellContext> options)
        : base(options) { }


    public virtual DbSet<Delivery> Deliveries { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderitem> Orderitems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("host=localhost;port=3306;database=websell;username=admin;password=admin", ServerVersion.Parse("8.0.33-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(e => new { e.UserId, e.LoginProvider });

        modelBuilder.Entity<IdentityUserRole<string>>().HasKey(e => new { e.UserId, e.RoleId });

        modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();

        modelBuilder.Entity<Delivery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("deliveries");

            entity.HasIndex(e => e.OrderId, "OrderId");

            entity.Property(e => e.Price).HasPrecision(10, 2);
            entity.Property(e => e.Type).HasColumnType("enum('pickup','courier','mail')");

            entity.HasOne(d => d.Order).WithMany(p => p.Deliveries)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("deliveries_ibfk_1");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("orders");

            entity.HasIndex(e => e.UserId, "UserId");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Status).HasColumnType("enum('new','processed','delivered')");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_ibfk_1");
        });

        modelBuilder.Entity<Orderitem>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("orderitems");

            entity.HasIndex(e => e.ProductId, "ProductId");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderitems_ibfk_1");

            entity.HasOne(d => d.Product).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderitems_ibfk_2");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("payments");

            entity.HasIndex(e => e.OrderId, "OrderId");

            entity.Property(e => e.Amount).HasPrecision(10, 2);
            entity.Property(e => e.Type).HasColumnType("enum('cash','card','e-money')");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payments_ibfk_1");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("products");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Price).HasPrecision(10, 2);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.Name).HasMaxLength(15);
            entity.Property(e => e.Password).HasMaxLength(15);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}