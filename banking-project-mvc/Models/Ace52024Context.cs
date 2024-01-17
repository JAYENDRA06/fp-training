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

    public virtual DbSet<Jayendra> Jayendras { get; set; }

    public virtual DbSet<SbaccountJay> SbaccountJays { get; set; }

    public virtual DbSet<SbtransactionJay> SbtransactionJays { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Jayendra>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__Jayendra__AA2FFBE5513A081B");

            entity.ToTable("Jayendra");

            entity.Property(e => e.PersonId).ValueGeneratedNever();
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
