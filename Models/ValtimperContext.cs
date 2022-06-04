using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Valtimperium
{
    public partial class ValtimperContext : DbContext
    {
        public ValtimperContext()
        {
        }

        public ValtimperContext(DbContextOptions<ValtimperContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BankCard> BankCards { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Ord> Ords { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Type> Types { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Valtimper;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankCard>(entity =>
            {
                entity.HasKey(e => e.IdCard)
                    .HasName("PK__BankCard__3B7B33C21696A5CA");

                entity.ToTable("BankCard");

                entity.Property(e => e.IdCard).ValueGeneratedNever();

                entity.Property(e => e.Number).IsUnicode(false);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.IdClient)
                    .HasName("PK__Client__C1961B3358CED6D1");

                entity.ToTable("Client");

                entity.Property(e => e.Login).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Phone).IsUnicode(false);

                entity.Property(e => e.Surname).IsUnicode(false);
            });

            modelBuilder.Entity<Ord>(entity =>
            {
                entity.HasKey(e => e.IdOrder)
                    .HasName("PK__Ord__C38F300948F98C65");

                entity.ToTable("Ord");

                entity.Property(e => e.AdressDelivery).IsUnicode(false);

                entity.Property(e => e.DateOrder).HasColumnType("datetime");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Ords)
                    .HasForeignKey(d => d.IdClient)
                    .HasConstraintName("FK__Ord__IdClient__2D27B809");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Ords)
                    .HasForeignKey(d => d.IdProduct)
                    .HasConstraintName("FK__Ord__IdProduct__2E1BDC42");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProduct)
                    .HasName("PK__Product__2E8946D4C852AB0E");

                entity.ToTable("Product");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdType)
                    .HasConstraintName("FK__Product__IdType__2A4B4B5E");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.HasKey(e => e.IdType)
                    .HasName("PK__Type__9A39EABC46D21B9C");

                entity.ToTable("Type");

                entity.Property(e => e.Name).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
