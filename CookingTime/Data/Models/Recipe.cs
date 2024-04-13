namespace CookingTime.Data.Models;

using Enums;

public class Recipe
{
    public int Id { get; set; }

    public string Title { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.Now;

    public RecipeType Type { get; set; }

    public string ShortDescription { get; set; }

    public string Content { get; set; }

    public string ImageUrl { get; set; }

    public double PreparationTime { get; set; }

    public double Portions { get; set; }

    public string Ingredients { get; set; }

    public string UserId { get; set; }

    public User User { get; set; }

    public List<Comment> Comments { get; set; } = new();

    public List<Favorite> Favorites { get; set; } = new();
}
