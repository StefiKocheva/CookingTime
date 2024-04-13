namespace CookingTime.Models.Recipes;

using Data.Enums;
using System.ComponentModel.DataAnnotations;
using Users;

public class RecipeDetailsOutputModel
{
    public int Id { get; set; }

    public string Title { get; set; }

    public DateTime CreatedOn { get; set; }

    public RecipeType Type { get; set; }

    public int? TypeInt { get; set; }

    public string ShortDescription { get; set; }

    public string Content { get; set; }

    public string ImageUrl { get; set; }

    public IFormFile? Image { get; set; }

    public double PreparationTime { get; set; }

    public double Portions { get; set; }

    public string Ingredients { get; set; }

    public string UserId { get; set; }

    public UserDetailsOutputModel User { get; set; }

    public int LikesCount { get; set; }

    public bool IsFavorite { get; set; }

    [Required(ErrorMessage = "Полето е задължително.")]
    [MinLength(2, ErrorMessage = "Въведете поне 2 символа.")]
    [MaxLength(200, ErrorMessage = "Въведете не повече от 200 символа.")]
    public string? CommentContent { get; set; }

    public List<CommentOutputModel> Comments { get; set; }
}
