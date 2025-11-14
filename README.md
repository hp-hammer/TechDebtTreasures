## Project Concept
A .NET 8 Minimal API microservice called UserProfileService.

Its sole purpose is to aggregate a user's profile for a front-end application. To do this, it needs to:

Fetch core user data (name, email) from a local database.

Fetch the user's "badges" or "awards" from another table in the same database.

Fetch the user's recent "social posts" from a (mocked) external API.

Combine all this data into a single UserProfileDto and return it.

This simple workflow provides ample opportunity to implement every fault on your list.

## Technology Stack
Framework: .NET 8 (or 9) Minimal API. This makes it modern and makes the choice of Newtonsoft.Json more obviously a fault.

Database: EF Core 8 with SQLite. This is file-based, so the candidate doesn't need to set up a real database. The DB file can be included in the repo.

External API (Mock): A second, very simple .NET Web API project within the same solution (MockSocialApi). This mock API just has one endpoint (GET /posts/{userId}) that returns a hard-coded JSON string. This makes it easy to control the HttpClient interaction.
