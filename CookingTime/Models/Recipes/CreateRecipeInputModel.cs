namespace CookingTime.Models.Recipes;

using System.ComponentModel.DataAnnotations;

public class CreateRecipeInputModel
{
    [Required(ErrorMessage = "Полето е задължително.")]
    [MinLength(2, ErrorMessage = "Въведете поне 2 символа.")]
    [MaxLength(50, ErrorMessage = "Въведете не повече от 50 символа.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Полето е задължително.")]
    [AllowedValues(1, 2, 3, 4, ErrorMessage = "Въведете число между 1 и 4.")]
    public int Type { get; set; }

    [Required(ErrorMessage = "Полето е задължително.")]
    [MinLength(2, ErrorMessage = "Въведете поне 2 символа.")]
    [MaxLength(50, ErrorMessage = "Въведете не повече от 50 символа.")]
    public string ShortDescription { get; set; }

    [Required(ErrorMessage = "Полето е задължително.")]
    [MinLength(50, ErrorMessage = "Въведете поне 50 символа.")]
    public string Content { get; set; }

    [Required(ErrorMessage = "Полето е задължително.")]
    public IFormFile Image { get; set; }

    [Required(ErrorMessage = "Полето е задължително.")]
    [Range(0, Double.MaxValue, ErrorMessage = "Въведете положително време за приготвяне.")]
    public double PreparationTime { get; set; }

    [Required(ErrorMessage = "Полето е задължително.")]
    [Range(0, Double.MaxValue, ErrorMessage = "Въведете положителен брой порции.")]
    public double Portions { get; set; }

    [Required(ErrorMessage = "Полето е задължително.")]
    [MinLength(2, ErrorMessage = "Въведете поне 2 символа.")]
    [MaxLength(200, ErrorMessage = "Въведете не повече от 200 символа.")]
    public string Ingredients { get; set; }
}
