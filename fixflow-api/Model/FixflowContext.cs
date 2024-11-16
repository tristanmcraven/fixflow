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

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketKit> TicketKits { get; set; }

    public virtual DbSet<TicketMalfunction> TicketMalfunctions { get; set; }

    public virtual DbSet<TicketRepair> TicketRepairs { get; set; }

    public virtual DbSet<TicketStatus> TicketStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=1234;database=fixflow", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.39-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<DeviceBrand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("device_brand");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Name, "name_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<DeviceModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("device_model");

            entity.HasIndex(e => e.DeviceBrandId, "fk_devicemodel_device_brand_id_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DeviceBrandId).HasColumnName("device_brand_id");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .HasColumnName("name");

            entity.HasOne(d => d.DeviceBrand).WithMany(p => p.DeviceModels)
                .HasForeignKey(d => d.DeviceBrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_devicemodel_device_brand_id");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("status");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Name, "name_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ticket");

            entity.HasIndex(e => e.DeviceBrandId, "fk_ticket_device_brand_id_idx");

            entity.HasIndex(e => e.DeviceModelId, "fk_ticket_device_model_id_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientFullname)
                .HasMaxLength(150)
                .HasColumnName("client_fullname");
            entity.Property(e => e.ClientPhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("client_phone_number");
            entity.Property(e => e.Description)
                .HasMaxLength(5000)
                .HasColumnName("description");
            entity.Property(e => e.DeviceBrandId).HasColumnName("device_brand_id");
            entity.Property(e => e.DeviceModelId).HasColumnName("device_model_id");
            entity.Property(e => e.Note)
                .HasMaxLength(5000)
                .HasColumnName("note");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("timestamp");

            entity.HasOne(d => d.DeviceBrand).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.DeviceBrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ticket_device_brand_id");

            entity.HasOne(d => d.DeviceModel).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.DeviceModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ticket_device_model_id");
        });

        modelBuilder.Entity<TicketKit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ticket_kit");

            entity.HasIndex(e => e.TicketId, "fk_ticketkit_ticket_id_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.TicketId).HasColumnName("ticket_id");

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketKits)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ticketkit_ticket_id");
        });

        modelBuilder.Entity<TicketMalfunction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ticket_malfunctions");

            entity.HasIndex(e => e.TicketId, "fk_ticketmalfunctions_ticket_id_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.TicketId).HasColumnName("ticket_id");

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketMalfunctions)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ticketmalfunctions_ticket_id");
        });

        modelBuilder.Entity<TicketRepair>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ticket_repairs");

            entity.HasIndex(e => e.TicketId, "fk_ticketrepairs_ticket_id_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Repair)
                .HasMaxLength(150)
                .HasColumnName("repair");
            entity.Property(e => e.TicketId).HasColumnName("ticket_id");

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketRepairs)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ticketrepairs_ticket_id");
        });

        modelBuilder.Entity<TicketStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ticket_status");

            entity.HasIndex(e => e.StatusId, "fk_ticketstatus_status_id_idx");

            entity.HasIndex(e => e.TicketId, "fk_ticketstatus_ticket_id_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.TicketId).HasColumnName("ticket_id");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("timestamp");

            entity.HasOne(d => d.Status).WithMany(p => p.TicketStatuses)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ticketstatus_status_id");

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketStatuses)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ticketstatus_ticket_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
