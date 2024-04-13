namespace CookingTime.Models.Users;

using System.ComponentModel.DataAnnotations;

public class UserInputModel
{
    [Required(ErrorMessage = "Полето е задължително")]
    [MinLength(2, ErrorMessage = "Въведете поне 2 символа.")]
    [MaxLength(50, ErrorMessage = "Въведете не повече от 50 символа.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Полето е задължително.")]
    [MinLength(2, ErrorMessage = "Въведете поне 2 символа.")]
    [MaxLength(50, ErrorMessage = "Въведете не повече от 50 символа.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Полето е задължително.")]
    [MinLength(2, ErrorMessage = "Въведете поне 2 символа.")]
    [MaxLength(500, ErrorMessage = "Въведете не повече от 500 символа.")]
    public string? Bio { get; set; }

    [Required(ErrorMessage = "Полето е задължително.")]
    [EmailAddress(ErrorMessage = "Въведете валиден имейл адрес.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Полето е задължително.")]
    [StringLength(100, ErrorMessage = "Паролата трябва да бъде между 6 и 100 символа.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,}$",
        ErrorMessage = "Паролата трябва да съдържа поне една главна буква, една малка буква, една цифра и един специален символ. Моля въведете между 6 и 100 символа.")]
    public string Password { get; set; }

    public string ImageUrl { get; set; }

    public IFormFile? Image { get; set; }
}
