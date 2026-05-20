using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMangas.Data.EF;

public partial class BibliotecaContext : DbContext
{
    public BibliotecaContext(DbContextOptions<BibliotecaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autores> Autores { get; set; }

    public virtual DbSet<Editoriales> Editoriales { get; set; }

    public virtual DbSet<Obras> Obras { get; set; }

    public virtual DbSet<Tomos> Tomos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Autores>(entity =>
        {
            entity.HasKey(e => e.AutorId).HasName("PRIMARY");

            entity.Property(e => e.Nombre).HasMaxLength(80);
        });

        modelBuilder.Entity<Editoriales>(entity =>
        {
            entity.HasKey(e => e.EditorialId).HasName("PRIMARY");

            entity.Property(e => e.Nombre).HasMaxLength(80);
            entity.Property(e => e.Pais).HasMaxLength(80);
        });

        modelBuilder.Entity<Obras>(entity =>
        {
            entity.HasKey(e => e.ObraId).HasName("PRIMARY");

            entity.HasIndex(e => e.AutorId, "AutorId");

            entity.HasIndex(e => e.EditorialId, "EditorialId");

            entity.Property(e => e.Titulo).HasMaxLength(80);

            entity.HasOne(d => d.Autor).WithMany(p => p.Obras)
                .HasForeignKey(d => d.AutorId)
                .HasConstraintName("Obras_ibfk_1");

            entity.HasOne(d => d.Editorial).WithMany(p => p.Obras)
                .HasForeignKey(d => d.EditorialId)
                .HasConstraintName("Obras_ibfk_2");
        });

        modelBuilder.Entity<Tomos>(entity =>
        {
            entity.HasKey(e => e.TomoId).HasName("PRIMARY");

            entity.HasIndex(e => e.ObraId, "ObraId");

            entity.HasOne(d => d.Obra).WithMany(p => p.Tomos)
                .HasForeignKey(d => d.ObraId)
                .HasConstraintName("Tomos_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
