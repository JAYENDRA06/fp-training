using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace first_mvc_application.Models;

public partial class Ace52024Context : DbContext
{
    public Ace52024Context()
    {
    }

    public Ace52024Context(DbContextOptions<Ace52024Context> options)
        : base(options)
    {
    }

    public virtual DbSet<SbaccountJay> SbaccountJays { get; set; }

    public virtual DbSet<SbtransactionJay> SbtransactionJays { get; set; }

    public virtual DbSet<UsertblJay> UsertblJays { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SbaccountJay>(entity =>
        {
            entity.HasKey(e => e.AccountNumber).HasName("PK__SBAccoun__BE2ACD6E03996350");

            entity.ToTable("SBAccountJay");

            entity.Property(e => e.CurrentBalance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CustomerAddress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SbtransactionJay>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__SBTransa__55433A6B33440E61");

            entity.ToTable("SBTransactionJay");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrancationType)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");

            entity.HasOne(d => d.AccountNumberNavigation).WithMany(p => p.SbtransactionJays)
                .HasForeignKey(d => d.AccountNumber)
                .HasConstraintName("fk");
        });

        modelBuilder.Entity<UsertblJay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UsertblJ__3213E83FD58D334A");

            entity.ToTable("UsertblJay");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
