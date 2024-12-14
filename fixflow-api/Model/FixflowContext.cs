using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace fixflow_api.Model;

public partial class FixflowContext : DbContext
{
    public FixflowContext()
    {
    }

    public FixflowContext(DbContextOptions<FixflowContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DeviceBrand> DeviceBrands { get; set; }

    public virtual DbSet<DeviceModel> DeviceModels { get; set; }

    public virtual DbSet<DeviceType> DeviceTypes { get; set; }

    public virtual DbSet<Repair> Repairs { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketKit> TicketKits { get; set; }

    public virtual DbSet<TicketMalfunction> TicketMalfunctions { get; set; }

    public virtual DbSet<TicketRepair> TicketRepairs { get; set; }

    public virtual DbSet<TicketStatus> TicketStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=localhost;user=root;password=1234;database=fixflow", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.40-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<DeviceBrand>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PRIMARY");

            entity.ToTable("device_brand");

            entity.HasIndex(e => e.Guid, "guid_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Name, "name_UNIQUE").IsUnique();

            entity.Property(e => e.Guid).HasColumnName("guid");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<DeviceModel>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PRIMARY");

            entity.ToTable("device_model");

            entity.HasIndex(e => e.DeviceBrandGuid, "fk_dm_device_brand_guid_idx");

            entity.HasIndex(e => e.Guid, "guid_UNIQUE").IsUnique();

            entity.Property(e => e.Guid).HasColumnName("guid");
            entity.Property(e => e.DeviceBrandGuid).HasColumnName("device_brand_guid");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.DeviceBrand).WithMany(p => p.DeviceModels)
                .HasForeignKey(d => d.DeviceBrandGuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_dm_device_brand_guid");
        });

        modelBuilder.Entity<DeviceType>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PRIMARY");

            entity.ToTable("device_type");

            entity.HasIndex(e => e.Guid, "guid_UNIQUE").IsUnique();

            entity.Property(e => e.Guid).HasColumnName("guid");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Repair>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PRIMARY");

            entity.ToTable("repair");

            entity.HasIndex(e => e.Guid, "guid_UNIQUE").IsUnique();

            entity.Property(e => e.Guid).HasColumnName("guid");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PRIMARY");

            entity.ToTable("status");

            entity.HasIndex(e => e.Guid, "guid_UNIQUE").IsUnique();

            entity.Property(e => e.Guid).HasColumnName("guid");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PRIMARY");

            entity.ToTable("ticket");

            entity.HasIndex(e => e.DeviceBrandGuid, "fk_ticket_device_brand_guid_idx");

            entity.HasIndex(e => e.DeviceModelGuid, "fk_ticket_device_model_guid_idx");

            entity.HasIndex(e => e.DeviceTypeGuid, "fk_ticket_device_type_guid_idx");

            entity.HasIndex(e => e.Guid, "guid_UNIQUE").IsUnique();

            entity.Property(e => e.Guid).HasColumnName("guid");
            entity.Property(e => e.ClientFullname)
                .HasMaxLength(150)
                .HasColumnName("client_fullname");
            entity.Property(e => e.ClientPhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("client_phone_number");
            entity.Property(e => e.Description)
                .HasMaxLength(5000)
                .HasColumnName("description");
            entity.Property(e => e.DeviceBrandGuid).HasColumnName("device_brand_guid");
            entity.Property(e => e.DeviceModelGuid).HasColumnName("device_model_guid");
            entity.Property(e => e.DeviceTypeGuid).HasColumnName("device_type_guid");
            entity.Property(e => e.Note)
                .HasMaxLength(5000)
                .HasColumnName("note");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("timestamp");

            entity.HasOne(d => d.DeviceBrand).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.DeviceBrandGuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ticket_device_brand_guid");

            entity.HasOne(d => d.DeviceModel).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.DeviceModelGuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ticket_device_model_guid");

            entity.HasOne(d => d.DeviceType).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.DeviceTypeGuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ticket_device_type_guid");
        });

        modelBuilder.Entity<TicketKit>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PRIMARY");

            entity.ToTable("ticket_kit");

            entity.HasIndex(e => e.TicketGuid, "fk_tk_ticket_guid_idx");

            entity.HasIndex(e => e.Guid, "guid_UNIQUE").IsUnique();

            entity.Property(e => e.Guid).HasColumnName("guid");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.TicketGuid).HasColumnName("ticket_guid");

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketKits)
                .HasForeignKey(d => d.TicketGuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tk_ticket_guid");
        });

        modelBuilder.Entity<TicketMalfunction>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PRIMARY");

            entity.ToTable("ticket_malfunctions");

            entity.HasIndex(e => e.TicketGuid, "fk_tm_ticket_guid_idx");

            entity.HasIndex(e => e.Guid, "guid_UNIQUE").IsUnique();

            entity.Property(e => e.Guid).HasColumnName("guid");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.TicketGuid).HasColumnName("ticket_guid");

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketMalfunctions)
                .HasForeignKey(d => d.TicketGuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tm_ticket_guid");
        });

        modelBuilder.Entity<TicketRepair>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PRIMARY");

            entity.ToTable("ticket_repairs");

            entity.HasIndex(e => e.RepairGuid, "fk_tr_repair_guid_idx");

            entity.HasIndex(e => e.TicketGuid, "fk_tr_ticket_guid_idx");

            entity.HasIndex(e => e.Guid, "guid_UNIQUE").IsUnique();

            entity.Property(e => e.Guid).HasColumnName("guid");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.RepairGuid).HasColumnName("repair_guid");
            entity.Property(e => e.TicketGuid).HasColumnName("ticket_guid");

            entity.HasOne(d => d.Repair).WithMany(p => p.TicketRepairs)
                .HasForeignKey(d => d.RepairGuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tr_repair_guid");

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketRepairs)
                .HasForeignKey(d => d.TicketGuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tr_ticket_guid");
        });

        modelBuilder.Entity<TicketStatus>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PRIMARY");

            entity.ToTable("ticket_status");

            entity.HasIndex(e => e.StatusGuid, "fk_ts_status_guid_idx");

            entity.HasIndex(e => e.TicketGuid, "fk_ts_ticket_guid_idx");

            entity.HasIndex(e => e.Guid, "guid_UNIQUE").IsUnique();

            entity.Property(e => e.Guid).HasColumnName("guid");
            entity.Property(e => e.StatusGuid).HasColumnName("status_guid");
            entity.Property(e => e.TicketGuid).HasColumnName("ticket_guid");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("timestamp");

            entity.HasOne(d => d.Status).WithMany(p => p.TicketStatuses)
                .HasForeignKey(d => d.StatusGuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ts_status_guid");

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketStatuses)
                .HasForeignKey(d => d.TicketGuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ts_ticket_guid");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
