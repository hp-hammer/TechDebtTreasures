namespace UserProfileService.Dtos;

public class UserProfileDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty; // The user email.
    public List<BadgeDto> Badges { get; set; } = new();
    public List<PostDto> RecentPosts { get; set; } = new();
}

public class BadgeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

