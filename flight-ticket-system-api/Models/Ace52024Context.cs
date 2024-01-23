using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace firstapi.Models;

public partial class Ace52024Context : DbContext
{
    public Ace52024Context()
    {
    }

    public Ace52024Context(DbContextOptions<Ace52024Context> options)
        : base(options)
    {
    }

    public virtual DbSet<AirlinesJay> AirlinesJays { get; set; }

    public virtual DbSet<AirportsJay> AirportsJays { get; set; }

    public virtual DbSet<BookingsJay> BookingsJays { get; set; }

    public virtual DbSet<FlightsJay> FlightsJays { get; set; }

    public virtual DbSet<PassengersJay> PassengersJays { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AirlinesJay>(entity =>
        {
            entity.HasKey(e => e.AirlineCode).HasName("PK__Airlines__79E77E126CE69B65");

            entity.ToTable("AirlinesJay");

            entity.Property(e => e.AirlineCode)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.AirlineName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AirportsJay>(entity =>
        {
            entity.HasKey(e => e.AirportCode).HasName("PK__Airports__4B67735214FCB92A");

            entity.ToTable("AirportsJay");

            entity.Property(e => e.AirportCode)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.AirportName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("city");
        });

        modelBuilder.Entity<BookingsJay>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951AED1C753C1D");

            entity.ToTable("BookingsJay");

            entity.Property(e => e.BookingDate).HasColumnType("datetime");
            entity.Property(e => e.FlightNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TotalCost).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.FlightNumberNavigation).WithMany(p => p.BookingsJays)
                .HasForeignKey(d => d.FlightNumber)
                .HasConstraintName("FK__BookingsJ__Fligh__76EBA2E9");

            entity.HasOne(d => d.Passenger).WithMany(p => p.BookingsJays)
                .HasForeignKey(d => d.PassengerId)
                .HasConstraintName("FK__BookingsJ__Passe__77DFC722");
        });

        modelBuilder.Entity<FlightsJay>(entity =>
        {
            entity.HasKey(e => e.FlightNumber).HasName("PK__FlightsJ__2EAE6F5165814928");

            entity.ToTable("FlightsJay");

            entity.Property(e => e.FlightNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.AirlineCode)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.ArrivalCode)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.ArrivalDateTime).HasColumnType("datetime");
            entity.Property(e => e.DepartureAirportCode)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.DepartureDateTime).HasColumnType("datetime");
            entity.Property(e => e.FlightName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TicketPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.AirlineCodeNavigation).WithMany(p => p.FlightsJays)
                .HasForeignKey(d => d.AirlineCode)
                .HasConstraintName("FK__FlightsJa__Airli__740F363E");

            entity.HasOne(d => d.ArrivalCodeNavigation).WithMany(p => p.FlightsJayArrivalCodeNavigations)
                .HasForeignKey(d => d.ArrivalCode)
                .HasConstraintName("FK__FlightsJa__Arriv__731B1205");

            entity.HasOne(d => d.DepartureAirportCodeNavigation).WithMany(p => p.FlightsJayDepartureAirportCodeNavigations)
                .HasForeignKey(d => d.DepartureAirportCode)
                .HasConstraintName("FK__FlightsJa__Depar__7226EDCC");
        });

        modelBuilder.Entity<PassengersJay>(entity =>
        {
            entity.HasKey(e => e.PassengerId).HasName("PK__Passenge__88915FB0A3D079B7");

            entity.ToTable("PassengersJay");

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
