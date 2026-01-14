using CafePOS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CafePOS.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base (options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserRole>()
            .HasKey(x => new { x.UserId, x.RoleId });

        modelBuilder.Entity<UserRole>()
            .HasOne(x => x.User)
            .WithMany(u => u.UserRole)
            .HasForeignKey(x => x.UserId);

        modelBuilder.Entity<UserRole>()
            .HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId);

        modelBuilder.Entity<RefreshToken>()
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);
    }
}