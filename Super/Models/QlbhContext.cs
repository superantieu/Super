using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Super.Models;

public partial class QlbhContext : DbContext
{
    public QlbhContext()
    {
    }

    public QlbhContext(DbContextOptions<QlbhContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Balance> Balances { get; set; }

    public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }

    public virtual DbSet<Hang> Hangs { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<Kmdb> Kmdbs { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<NhanHieu> NhanHieus { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=DB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietHoaDon>(entity =>
        {
            entity.Property(e => e.MaHd).ValueGeneratedOnAdd();
            entity.Property(e => e.DonGiaBan).HasDefaultValueSql("((0))");
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.SoLuongMua).HasDefaultValueSql("((0))");
            entity.Property(e => e.ThanhTien).HasDefaultValueSql("((0))");
            entity.Property(e => e.TongTien).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.MaHangNavigation).WithMany(p => p.ChiTietHoaDons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietHoaDon_Hang");

            entity.HasOne(d => d.MaHdNavigation).WithMany(p => p.ChiTietHoaDons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietHoaDon_HoaDon");
        });

        modelBuilder.Entity<Hang>(entity =>
        {
            entity.Property(e => e.Filter).HasComputedColumnSql("(lower([TenHang]+[MaNhanHieu]))", true);
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.MaNhanHieuNavigation).WithMany(p => p.Hangs).HasConstraintName("FK_Hang_NhanHieu");

            entity.HasOne(d => d.UrlNavigation).WithMany(p => p.Hangs).HasConstraintName("FK_Hang_Balance");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.NgayLap).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.HoaDons).HasConstraintName("FK_HoaDon_KhachHang");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<NhanHieu>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.MaCungCapNavigation).WithMany(p => p.NhanHieus).HasConstraintName("FK_NhanHieu_NhaCungCap");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Email).IsFixedLength();
            entity.Property(e => e.Password).IsFixedLength();
            entity.Property(e => e.RoleId).HasDefaultValueSql("((2))");
            entity.Property(e => e.UserName).IsFixedLength();

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.Users).HasConstraintName("FK_Users_KhachHang");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
