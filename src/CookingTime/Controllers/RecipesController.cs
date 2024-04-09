namespace CookingTime.Controllers;

using Data;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Recipes;
using System.Security.Claims;
using Data.Enums;

public class RecipesController : Controller
{
    private readonly CookingTimeDbContext data;
    private readonly IWebHostEnvironment env;

    public RecipesController(
        CookingTimeDbContext data,
        IWebHostEnvironment env)
    {
        this.data = data;
        this.env = env;
    }

    [HttpGet]
    public IActionResult All()
    {
        var recipes = this.GetRecipes();

        return View(recipes);
    }

    [HttpGet]
    [Authorize]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    public IActionResult Create(CreateRecipeInputModel input)
    {
        using var fileStream = new FileStream(
            Path.Combine(
                this.env.WebRootPath,
                "img/uploads",
                input.Image.FileName),
            FileMode.Create);

        input.Image.CopyTo(fileStream);

        var recipe = new Recipe()
        {
            Title = input.Title,
            ShortDescription = input.ShortDescription,
            Content = input.Content,
            Ingredients = input.Ingredients,
            Portions = input.Portions,
            PreparationTime = input.PreparationTime,
            Type = (RecipeType)input.Type,
            ImageUrl = @"\img\uploads\" + input.Image.FileName,
            UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value
        };

        this.data.Add(recipe);
        this.data.SaveChanges();

        return RedirectToAction("All");
    }

    private IEnumerable<RecipeDetailsOutputModel> GetRecipes()
        => this.data.Recipes
            .Select(r => new RecipeDetailsOutputModel()
            {
                Id = r.Id,
                Title = r.Title,
                ShortDescription = r.ShortDescription,
                Content = r.Content,
                Ingredients = r.Ingredients,
                Portions = r.Portions,
                PreparationTime = r.PreparationTime,
                Type = r.Type,
                CreatedOn = r.CreatedOn,
                ImageUrl = r.ImageUrl,
                UserId = r.UserId
            })
            .ToList();
}