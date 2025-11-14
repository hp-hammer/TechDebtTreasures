using Microsoft.EntityFrameworkCore;
using UserProfileService.Data;
using UserProfileService.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure SQLite database
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") 
        ?? "Data Source=app.db"));

// Register services
builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<ExternalApiService>();

var app = builder.Build();

// Ensure database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    db.Database.EnsureCreated();
}

// Main endpoint to get user profile
app.MapGet("/profile/{userId}", (int userId, ProfileService profileService) =>
{
    var profile = profileService.GetUserProfile(userId);
    
    if (profile == null)
    {
        return Results.NotFound(new { message = "User not found" });
    }
    
    return Results.Ok(profile);
});

app.Run();
