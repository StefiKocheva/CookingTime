namespace CookingTime.Models.Recipes;

using Users;

public class CommentOutputModel
{
    public int Id { get; set; }

    public string Content { get; set; }

    public DateTime CreatedOn { get; set; }

    public int RecipeId { get; set; }

    public string UserId { get; set; }

    public UserDetailsOutputModel User { get; set; }
}
