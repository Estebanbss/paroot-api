using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using paroot_api.Data.Models;

namespace paroot_api.Data;

public partial class ParootDbContext : DbContext
{
    public ParootDbContext()
    {
    }

    public ParootDbContext(DbContextOptions<ParootDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Url> Urls { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Url>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__urls__3213E83FC053E648");

            entity.ToTable("urls");

            entity.HasIndex(e => e.ShortUrl, "UQ__urls__44C120A573AB6A1B").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Clicks)
                .HasDefaultValue(0)
                .HasColumnName("clicks");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.LastClickedAt)
                .HasColumnType("datetime")
                .HasColumnName("last_clicked_at");
            entity.Property(e => e.LastClickedCountry)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("last_clicked_country");
            entity.Property(e => e.OriginalUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("original_url");
            entity.Property(e => e.ShortUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("short_url");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
