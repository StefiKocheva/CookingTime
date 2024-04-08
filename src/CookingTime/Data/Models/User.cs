namespace CookingTime.Data.Models;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string? Bio { get; set; }

    public string ImageUrl { get; set; } = "https://res.cloudinary.com/dduhwzpfw/image/upload/v1672849735/avatar.png";

    public List<Recipe> Recipes { get; set; } = new();

    public List<Comment> Comments { get; set; } = new();

    public List<Favorite> Favorites { get; set; } = new();
}
