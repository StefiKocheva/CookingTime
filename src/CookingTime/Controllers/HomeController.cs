namespace CookingTime.Controllers;

using Models.Recipes;
using Data;
using Data.Enums;
using Microsoft.AspNetCore.Mvc;
using Models;

public class HomeController : Controller
{
    private readonly CookingTimeDbContext data;

    public HomeController(CookingTimeDbContext data)
    {
        this.data = data;
    }

    public IActionResult Index()
    {
        var model = new IndexOutputModel()
        {
            TotalSalads = this.data.Recipes.Count(r => r.Type == RecipeType.Salad),
            TotalStarters = this.data.Recipes.Count(r => r.Type == RecipeType.Starter),
            TotalMainDishes = this.data.Recipes.Count(r => r.Type == RecipeType.MainDish),
            TotalDesserts = this.data.Recipes.Count(r => r.Type == RecipeType.Dessert),
            LastThree = this.data.Recipes
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
                    UserId = r.UserId,
                    LikesCount = this.data.Favorites.Count(f => f.RecipeId == r.Id)
                })
                .OrderByDescending(r => r.CreatedOn)
                .Take(3)
                .ToList()
        };

        return View(model);
    }
}
