namespace CookingTime.Models.Users;

public class UserDetailsOutputModel
{
    public string Id { get; set; }

    public string FirstName { get; set; }
     
    public string LastName { get; set; }

    public string ImageUrl { get; set; }

    public string? Bio { get; set; }

    public string Email { get; set; }

    public IFormFile? Image { get; set; }
}
