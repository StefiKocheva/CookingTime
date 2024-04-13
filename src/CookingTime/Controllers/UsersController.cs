namespace CookingTime.Controllers;

using System.Security.Claims;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Users;

public class UsersController : Controller
{
    private readonly CookingTimeDbContext data;
    private readonly UserManager<User> userManager;
    private readonly IWebHostEnvironment env;

    public UsersController(CookingTimeDbContext data, UserManager<User> userManager, IWebHostEnvironment env)
    {
        this.data = data;
        this.userManager = userManager;
        this.env = env;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Details()
    {
        var user = this.data.Users
            .Where(u => u.Id == this.User.FindFirst(ClaimTypes.NameIdentifier).Value)
            .Select(u => new UserDetailsOutputModel()
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Bio = u.Bio,
                Email = u.Email,
                ImageUrl = u.ImageUrl
            })
            .Single();

        return View(user);
    }

    [HttpGet]
    [Authorize(Roles = "Administrator")]
    public IActionResult All()
    {
        var users = this.data.Users
            .Select(u => new UserDetailsOutputModel()
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Bio = u.Bio,
                Email = u.Email,
                ImageUrl = u.ImageUrl
            })
            .ToList();

        return View(users);
    }

    [HttpGet]
    [Authorize]
    public IActionResult Edit(string id)
    {
        var user = this.data.Users
            .Where(u => u.Id == id)
            .Select(u => new UserDetailsOutputModel()
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Bio = u.Bio,
                Email = u.Email,
                ImageUrl = u.ImageUrl
            })
            .Single();

        return View(user);
    }

    [HttpPost]
    [Authorize]
    public IActionResult Update(string id, UserInputModel input)
    {
        if (!this.User.IsInRole("Administrator"))
        {
            id = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        var user = this.data.Users
            .FirstOrDefault(u => u.Id == id);

        user.FirstName = input.FirstName;
        user.LastName = input.LastName;
        user.Bio = input.Bio;

        if (input.Image != null)
        {
            using var fileStream = new FileStream(
                Path.Combine(
                    this.env.WebRootPath,
                    "img/uploads/profiles",
                    input.Image.FileName),
                FileMode.Create);

            input.Image.CopyTo(fileStream);

            user.ImageUrl = @"\img\uploads\profiles\" + input.Image.FileName;
        }

        user.Email = input.Email;
        user.NormalizedEmail = input.Email.ToUpper();
        user.UserName = input.Email;
        user.UserName = input.Email.ToUpper();

        this.data.SaveChanges();

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public IActionResult Delete(string id)
    {
        foreach (var comment in this.data.Comments.Where(c => c.UserId == id))
        {
            this.data.Comments.Remove(comment);
        }

        foreach (var favorite in this.data.Favorites.Where(f => f.UserId == id))
        {
            this.data.Favorites.Remove(favorite);
        }

        foreach (var recipe in this.data.Recipes.Where(r => r.UserId == id))
        {
            foreach (var comment in this.data.Comments.Where(c => c.RecipeId == recipe.Id))
            {
                this.data.Comments.Remove(comment);
            }

            this.data.Recipes.Remove(recipe);
        }

        var user = this.data.Users
            .FirstOrDefault(u => u.Id == id);

        this.data.Users.Remove(user);
        this.data.SaveChanges();

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Authorize(Roles = "Administrator")]
    public IActionResult Add()
        => View();

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Create(UserInputModel input)
    {
        var user = new User
        {
            UserName = input.Email,
            Email = input.Email,
            FirstName = input.FirstName,
            LastName = input.LastName
        };

        await userManager.CreateAsync(user, input.Password);

        return RedirectToAction("All");
    }
}