namespace CookingTime.Data.Models;

public class Comment
{
    public int Id { get; set; }

    public string Content { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.Now;

    public int RecipeId { get; set; }

    public Recipe Recipe { get; set; }

    public string UserId { get; set; }

    public User User { get; set; }
}
