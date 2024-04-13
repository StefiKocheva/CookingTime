namespace CookingTime.Data.Models;

public class Favorite
{
    public int RecipeId { get; set; }

    public Recipe Recipe { get; set; }

    public string UserId { get; set; }

    public User User { get; set; }
}
