using Newtonsoft.Json;
using UserProfileService.Dtos;

namespace UserProfileService.Services;

public class ExternalApiService
{
    private readonly ILogger<ExternalApiService> _logger;
    private readonly string _mockApiBaseUrl;

    public ExternalApiService(ILogger<ExternalApiService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _mockApiBaseUrl = configuration["MockApiBaseUrl"] ?? "http://localhost:5001";
    }

    public async Task<List<PostDto>?> GetPostsForUser(int userId)
    {
        try
        {
            using (var client = new HttpClient())
            {
                var url = $"{_mockApiBaseUrl}/posts/{userId}";
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Failed to fetch posts.");
                    return null;
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                var posts = JsonConvert.DeserializeObject<List<PostDto>>(jsonString);
                return posts;
            }
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError("Failed to fetch posts.");
            return null;
        }
    }
}

