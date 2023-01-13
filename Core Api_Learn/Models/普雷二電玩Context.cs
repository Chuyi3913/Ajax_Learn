using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Core_Api_Learn.Models
{
    public partial class 普雷二電玩Context : DbContext
    {
        public 普雷二電玩Context()
        {
        }

        public 普雷二電玩Context(DbContextOptions<普雷二電玩Context> options)
            : base(options)
        {
        }

        public virtual DbSet<顧客列表> 顧客列表s { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<顧客列表>(entity =>
            {
                entity.HasKey(e => e.顧客id);

                entity.ToTable("顧客列表");

                entity.HasIndex(e => e.會員帳號, "IX_顧客列表")
                    .IsUnique();

                entity.Property(e => e.顧客id).HasColumnName("顧客ID");

                entity.Property(e => e.信用評等)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.信用額度)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.地址).HasMaxLength(50);

                entity.Property(e => e.會員密碼).HasMaxLength(50);

                entity.Property(e => e.會員帳號).HasMaxLength(50);

                entity.Property(e => e.會員類型).HasMaxLength(50);

                entity.Property(e => e.機構代碼)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.統一編號).HasMaxLength(10);

                entity.Property(e => e.聯絡人).HasMaxLength(30);

                entity.Property(e => e.聯絡電話).HasMaxLength(20);

                entity.Property(e => e.認證).HasDefaultValueSql("((0))");

                entity.Property(e => e.銀行帳號)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.顧客名稱).HasMaxLength(30);

                entity.Property(e => e.驗證碼).HasMaxLength(6);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
