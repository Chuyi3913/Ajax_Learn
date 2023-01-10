using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Core_Api_Learn.Models
{
    public partial class DemoContext : DbContext
    {
        public DemoContext()
        {
        }

        public DemoContext(DbContextOptions<DemoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Member> Members { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.City)
                    .HasMaxLength(10)
                    .HasColumnName("city");

                entity.Property(e => e.Road)
                    .HasMaxLength(200)
                    .HasColumnName("road");

                entity.Property(e => e.SiteId)
                    .HasMaxLength(50)
                    .HasColumnName("site_id");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.FileName).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
