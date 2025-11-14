var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Mock endpoint that returns static social posts for any user
app.MapGet("/posts/{userId}", (int userId) =>
{
    var posts = new[]
    {
        new { Id = 1, UserId = userId, Content = "Just completed my first code review!", Timestamp = DateTime.UtcNow.AddDays(-2) },
        new { Id = 2, UserId = userId, Content = "Loving the new features in .NET 8!", Timestamp = DateTime.UtcNow.AddDays(-1) },
        new { Id = 3, UserId = userId, Content = "Working on an exciting new project today.", Timestamp = DateTime.UtcNow }
    };
    
    return Results.Ok(posts);
});

app.Run();
