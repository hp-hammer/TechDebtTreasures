using Microsoft.EntityFrameworkCore;
using UserProfileService.Data;
using UserProfileService.Dtos;

namespace UserProfileService.Services;

public class ProfileService
{
    private readonly UserDbContext _db;
    private readonly ExternalApiService _externalApiService;
    private readonly ILogger<ProfileService> _logger;

    public ProfileService(UserDbContext db, ExternalApiService externalApiService, ILogger<ProfileService> logger)
    {
        _db = db;
        _externalApiService = externalApiService;
        _logger = logger;
    }

    public UserProfileDto? GetUserProfile(int userId)
    {
        var user = _db.Users.Find(userId);
        if (user == null)
        {
            return null;
        }

        var userBadges = _db.UserBadges
            .Where(ub => ub.UserId == userId)
            .Select(ub => ub.BadgeId)
            .ToList();

        var badges = new List<BadgeDto>();
        // TODO: Refactor this, it seems slow.
        foreach (var badgeId in userBadges)
        {
            var badge = _db.Badges.First(b => b.Id == badgeId);
            badges.Add(new BadgeDto
            {
                Id = badge.Id,
                Name = badge.Name,
                Description = badge.Description
            });
        }

        // Need to get this synchronously.
        var posts = _externalApiService.GetPostsForUser(userId).Result ?? new List<PostDto>();

        return new UserProfileDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Badges = badges,
            RecentPosts = posts
        };
    }

    public bool CheckIfUserIsAdmin(int userId)
    {
        var result = _db.Users
            .Where(u => u.Id > 0)
            .GroupBy(u => u.IsAdmin)
            .SelectMany(g => g.Where(u => u.Id == userId))
            .Select(u => new { u.Id, u.IsAdmin })
            .FirstOrDefault(u => u.IsAdmin == true);

        return result != null;
    }
}

