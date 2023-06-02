using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace prestaToolsApi.models_DB;

public partial class PrestatoolsContext : DbContext
{
    public PrestatoolsContext()
    {
    }

    public PrestatoolsContext(DbContextOptions<PrestatoolsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoryEntity> Categories { get; set; }

    public virtual DbSet<DetalleVentum> DetalleVenta { get; set; }

    public virtual DbSet<LenderEntity> Lenders { get; set; }

    public virtual DbSet<ToolEntity> Tools { get; set; }

    public virtual DbSet<UserEntity> Users { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
       // => optionsBuilder.UseMySql("server=localhost;port=3306;database=prestatools;uid=root;password=desarrollo2023", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.33-mysql"));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<CategoryEntity>(entity =>
        {
            entity.HasKey(e => e.IdCat).HasName("PRIMARY");

            entity.ToTable("category");

            entity.Property(e => e.IdCat).HasColumnName("id_cat");
            entity.Property(e => e.DescripCat)
                .HasMaxLength(255)
                .HasColumnName("descrip_cat");
            entity.Property(e => e.NombreCat)
                .HasMaxLength(255)
                .HasColumnName("nombre_cat");
        });

        modelBuilder.Entity<DetalleVentum>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("PRIMARY");

            entity.ToTable("detalle_venta");

            entity.HasIndex(e => e.IdVenta, "Detalle_venta_fk0");

            entity.HasIndex(e => e.IdTool, "Detalle_venta_fk1");

            entity.Property(e => e.IdDetalleVenta).HasColumnName("id_detalle_venta");
            entity.Property(e => e.AmountVenta)
                .HasPrecision(10)
                .HasColumnName("amount_venta");
            entity.Property(e => e.DateUp)
                .HasColumnType("timestamp")
                .HasColumnName("date_up");
            entity.Property(e => e.Descuento)
                .HasPrecision(10)
                .HasColumnName("descuento");
            entity.Property(e => e.IdTool).HasColumnName("id_tool");
            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.Price)
                .HasPrecision(10)
                .HasColumnName("price");

            entity.HasOne(d => d.IdToolNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdTool)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Detalle_venta_fk1");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Detalle_venta_fk0");
        });

        modelBuilder.Entity<LenderEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("lender");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.BalanceWallet)
                .HasPrecision(10)
                .HasColumnName("balance_wallet");
            entity.Property(e => e.DIdentidad)
                .HasMaxLength(255)
                .HasColumnName("d_identidad");
            entity.Property(e => e.DateUp)
                .HasColumnType("timestamp")
                .HasColumnName("date_up");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.NumberBank)
                .HasMaxLength(255)
                .HasColumnName("number_bank");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.Telephone)
                .HasMaxLength(255)
                .HasColumnName("telephone");
        });

        modelBuilder.Entity<ToolEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tool");

            entity.HasIndex(e => e.IdCategory, "Tool_fk0");

            entity.HasIndex(e => e.IdUser, "Tool_fk1");

            entity.HasIndex(e => e.IdLenders, "Tool_fk2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BreakDowns)
                .HasMaxLength(255)
                .HasColumnName("break_downs");
            entity.Property(e => e.DateUp)
                .HasColumnType("timestamp")
                .HasColumnName("date_up");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.IdCategory).HasColumnName("id_category");
            entity.Property(e => e.IdLenders).HasColumnName("id_lenders");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Mesuare)
                .HasPrecision(10)
                .HasColumnName("mesuare");
            entity.Property(e => e.Model)
                .HasMaxLength(255)
                .HasColumnName("model");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Nuevo).HasColumnName("nuevo");
            entity.Property(e => e.NumberPiece).HasColumnName("number_piece");
            entity.Property(e => e.Reference)
                .HasMaxLength(255)
                .HasColumnName("reference");
            entity.Property(e => e.TermsUse)
                .HasMaxLength(255)
                .HasColumnName("terms_use");
            entity.Property(e => e.TimeUse)
                .HasPrecision(10)
                .HasColumnName("time_use");
            entity.Property(e => e.UrlImage1)
                .HasMaxLength(255)
                .HasColumnName("url_image_1");
            entity.Property(e => e.UrlImage2)
                .HasMaxLength(255)
                .HasColumnName("url_image_2");
            entity.Property(e => e.UrlImage3)
                .HasMaxLength(255)
                .HasColumnName("url_image_3");
            entity.Property(e => e.ValueCommercial)
                .HasPrecision(10)
                .HasColumnName("value_commercial");
            entity.Property(e => e.ValueRent)
                .HasPrecision(10)
                .HasColumnName("value_rent");
            entity.Property(e => e.Weigt)
                .HasPrecision(10)
                .HasColumnName("weigt");
            entity.Property(e => e.Widgets).HasColumnName("widgets");
            entity.Property(e => e.YearBuy).HasColumnName("year_buy");

            entity.HasOne(d => d.ObjetCategory).WithMany(p => p.Tools)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Tool_fk0");

            entity.HasOne(d => d.IdLendersNavigation).WithMany(p => p.Tools)
                .HasForeignKey(d => d.IdLenders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Tool_fk2");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Tools)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Tool_fk1");
        });

        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.DIdentidad)
                .HasMaxLength(255)
                .HasColumnName("d_identidad");
            entity.Property(e => e.DateUp)
                .HasColumnType("timestamp")
                .HasColumnName("date_up");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.Telephone)
                .HasMaxLength(255)
                .HasColumnName("telephone");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PRIMARY");

            entity.ToTable("venta");

            entity.HasIndex(e => e.IdUser, "Venta_fk0");

            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.DateUp)
                .HasColumnType("timestamp")
                .HasColumnName("date_up");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.NumberComprobante)
                .HasMaxLength(255)
                .HasColumnName("number_comprobante");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.Tax)
                .HasPrecision(10)
                .HasColumnName("tax");
            entity.Property(e => e.Total)
                .HasPrecision(10)
                .HasColumnName("total");
            entity.Property(e => e.TypeComprobante)
                .HasMaxLength(255)
                .HasColumnName("type_comprobante");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Venta_fk0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
