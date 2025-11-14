using Microsoft.EntityFrameworkCore;

namespace UserProfileService.Data;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Badge> Badges { get; set; }
    public DbSet<UserBadge> UserBadges { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure many-to-many relationship
        modelBuilder.Entity<UserBadge>()
            .HasKey(ub => new { ub.UserId, ub.BadgeId });

        modelBuilder.Entity<UserBadge>()
            .HasOne(ub => ub.User)
            .WithMany(u => u.UserBadges)
            .HasForeignKey(ub => ub.UserId);

        modelBuilder.Entity<UserBadge>()
            .HasOne(ub => ub.Badge)
            .WithMany(b => b.UserBadges)
            .HasForeignKey(ub => ub.BadgeId);

        // Seed data
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com", IsAdmin = false },
            new User { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com", IsAdmin = true },
            new User { Id = 3, Name = "Bob Johnson", Email = "bob.johnson@example.com", IsAdmin = false }
        );

        modelBuilder.Entity<Badge>().HasData(
            new Badge { Id = 1, Name = "Early Adopter", Description = "Joined in the first month" },
            new Badge { Id = 2, Name = "Code Master", Description = "Completed 100 code reviews" },
            new Badge { Id = 3, Name = "Team Player", Description = "Helped 50 team members" },
            new Badge { Id = 4, Name = "Bug Hunter", Description = "Found 25 critical bugs" }
        );

        modelBuilder.Entity<UserBadge>().HasData(
            new UserBadge { UserId = 1, BadgeId = 1 },
            new UserBadge { UserId = 1, BadgeId = 2 },
            new UserBadge { UserId = 2, BadgeId = 1 },
            new UserBadge { UserId = 2, BadgeId = 3 },
            new UserBadge { UserId = 2, BadgeId = 4 },
            new UserBadge { UserId = 3, BadgeId = 1 }
        );
    }
}

