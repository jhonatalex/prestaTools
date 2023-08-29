using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace prestaToolsApi.ModelsEntity;

public partial class PrestatoolsContext : DbContext
{
    public PrestatoolsContext()
    {
    }

    public PrestatoolsContext(DbContextOptions<PrestatoolsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<DetalleVentum> DetalleVenta { get; set; }

    public virtual DbSet<Lender> Lenders { get; set; }

    public virtual DbSet<Tool> Tools { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCat).HasName("PK_CATEGORY");

            entity.ToTable("Category");

            entity.Property(e => e.IdCat)
                //.ValueGeneratedNever()
                .HasColumnName("id_cat");
            entity.Property(e => e.DescripCat)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descrip_cat");
            entity.Property(e => e.TitleCat)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title_cat");
            entity.Property(e => e.UrlImagen)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("url_imagen");
            entity.Property(e => e.UrlImagenBanner)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("url_imagen_banner");
        });

        modelBuilder.Entity<DetalleVentum>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("PK_DETALLE_VENTA");
            entity.ToTable("Detalle_venta");
            entity.Property(e => e.IdDetalleVenta)
                //.ValueGeneratedNever()
                .HasColumnName("id_detalle_venta");//1
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("amount");//2
            entity.Property(e => e.Date)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("date");//3
            entity.Property(e => e.Descuento)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("descuento");//4
            entity.Property(e => e.StartDate)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("start_date");//5
            entity.Property(e => e.EndDate)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("end_date");//6
            entity.Property(e => e.BuyOrder)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("buy_order");
            entity.Property(e => e.SessionId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("session_id");
            entity.Property(e => e.PaymentTypeCode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("payment_type_code");
            entity.Property(e => e.InstallmentsAmount)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("installments_amount");
            entity.Property(e => e.InstallmentsNumber).HasColumnName("installments_number");
            entity.Property(e => e.Token)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("token");
            entity.Property(e => e.RentalDays).HasColumnName("rental_days");
            entity.Property(e => e.IdTool).HasColumnName("id_tool");
            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("price");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("total");
            entity.HasOne(d => d.IdToolNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdTool)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Detalle_venta_fk1");
            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Detalle_venta_fk0");
        });

        modelBuilder.Entity<Lender>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK_LENDER");

            entity.ToTable("Lender");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.BalanceWallet)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("balance_wallet");
            entity.Property(e => e.DIdentidad)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("d_identidad");
            entity.Property(e => e.DateUp)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("date_up");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.NumberBank)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("number_bank");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Telephone)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("telephone");
            entity.Property(e => e.commune)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("commune");
            entity.Property(e => e.region)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("region");
            entity.Property(e => e.Rate).HasColumnName("rate");
        });

        modelBuilder.Entity<Tool>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TOOL");

            entity.ToTable("Tool");

            entity.Property(e => e.Id)
                //.ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.BreakDowns)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("break_downs");
            entity.Property(e => e.DateUp)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("date_up");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.IdCategory).HasColumnName("id_category");
            entity.Property(e => e.IdLenders)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("id_lenders");
            entity.Property(e => e.Mesuare)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("mesuare");
            entity.Property(e => e.Model)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("model");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.NewItem).HasColumnName("newitem");
            entity.Property(e => e.NumberPiece).HasColumnName("number_piece");
            entity.Property(e => e.Rate).HasColumnName("rate");
            entity.Property(e => e.Reference)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("reference");
            entity.Property(e => e.Brand)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("brand");
            entity.Property(e => e.State)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("state");
            entity.Property(e => e.TermsUse)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("terms_use");
            entity.Property(e => e.TimeUse)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("time_use");
            entity.Property(e => e.UrlImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("url_image");
            entity.Property(e => e.UrlImage2)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("url_image_2");
            entity.Property(e => e.UrlImage3)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("url_image_3");
            entity.Property(e => e.ValueCommercial)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("value_commercial");
            entity.Property(e => e.ValueRent)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("value_rent");
            entity.Property(e => e.Weigt)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("weigt");
            entity.Property(e => e.Widgets).HasColumnName("widgets");
            entity.Property(e => e.YearBuy).HasColumnName("year_buy");

            entity.HasOne(d => d.objetoCategoria).WithMany(p => p.Tools)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Tool_fk0");

            entity.HasOne(d => d.objetoLender).WithMany(p => p.Tools)
                .HasForeignKey(d => d.IdLenders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Tool_fk1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK_USER");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__AB6E61644F51E8F1").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.DIdentidad)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("d_identidad");
            entity.Property(e => e.Date)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("date");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Telephone)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("telephone");
            entity.Property(e => e.TypeUser)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("type_user");
            entity.Property(e => e.commune)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("commune");
            entity.Property(e => e.region)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("region");
            entity.Property(e => e.Rate).HasColumnName("rate");
            entity.Property(e => e.Verify).HasColumnName("verify");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK_VENTA");

            entity.Property(e => e.IdVenta)
                //.ValueGeneratedNever()
                .HasColumnName("id_venta");
            entity.Property(e => e.Date)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("date");
            entity.Property(e => e.IdUser)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("id_user");
            entity.Property(e => e.NumberComprobante)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("number_comprobante");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.TypeComprobante)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("type_comprobante");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Venta_fk0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public override int SaveChanges()
    {
        ChangeTracker.DetectChanges();

        var conflictingEntries = ChangeTracker.Entries()
      .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
      .GroupBy(e => e.Metadata.FindPrimaryKey().Properties.Select(p => e.Property(p.Name).CurrentValue).ToArray())
      .FirstOrDefault(g => g.Count() > 1);

        if (conflictingEntries != null)
        {
            var conflictingKeyValues = conflictingEntries.Key;
            throw new InvalidOperationException($"Multiple entities of type '{conflictingEntries.First().Metadata.Name}' have the same key value '{string.Join(", ", conflictingKeyValues)}'. Ensure that only one entity instance with a given key value is attached.");
        }

        return base.SaveChanges();
    }
}
