namespace CookingTime.Areas.Identity.Pages.Account;

using System.ComponentModel.DataAnnotations;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class RegisterModel : PageModel
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public RegisterModel(
        UserManager<User> userManager,
        SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
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
        [EmailAddress(ErrorMessage = "Въведете валиден имейл адрес.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Полето е задължително.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролите не съвпадат.")]
        public string ConfirmPassword { get; set; }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = new User
        {
            FirstName = Input.FirstName,
            LastName = Input.LastName,
            UserName = Input.Email,
            Email = Input.Email
        };

        var result = await _userManager.CreateAsync(user, Input.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToPage("/");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return Page();
    }
}