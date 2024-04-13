namespace CookingTime.Controllers;

using Data;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Recipes;
using System.Security.Claims;
using Data.Enums;
using Models.Users;
using System.Web;

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
        var model = new RecipesListingOutputModel
        {
            ListName = "Всички рецепти",
            Recipes = this.GetRecipes()
        };

        return View(model);
    }

    [HttpGet]
    [Authorize]
    public IActionResult Favorites()
    {
        var favorites = this.data.Favorites
            .Where(f => f.UserId == this.User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var model = new RecipesListingOutputModel
        {
            ListName = "Харесани рецепти",
            Recipes = new List<RecipeDetailsOutputModel>()
        };

        foreach (var favorite in favorites)
        {
            model.Recipes.Add(this.data.Recipes
                .Where(r => r.Id == favorite.RecipeId)
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
                .FirstOrDefault());
        }

        return View("Listing", model);
    }

    [HttpPost]
    [Authorize]
    public IActionResult AddToFavorites(int id)
    {
        var favorite = new Favorite()
        {
            UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value,
            RecipeId = id
        };

        this.data.Add(favorite);
        this.data.SaveChanges();

        return RedirectToAction("Details", new { Id = id });
    }

    [HttpPost]
    [Authorize]
    public IActionResult RemoveFromFavorites(int id)
    {
        var favorite = this.data.Favorites
            .FirstOrDefault(f => f.UserId == this.User.FindFirst(ClaimTypes.NameIdentifier).Value && f.RecipeId == id);

        this.data.Remove(favorite);
        this.data.SaveChanges();

        return RedirectToAction("Details", new { Id = id });
    }

    [HttpGet]
    public IActionResult Salads()
    {
        var model = new RecipesListingOutputModel
        {
            ListName = "Салати",
            Recipes = this
                .GetRecipes()
                .Where(r => r.Type == RecipeType.Salad)
                .ToList()
        };

        return View("All", model);
    }

    public IActionResult Starters()
    {
        var model = new RecipesListingOutputModel
        {
            ListName = "Предястия",
            Recipes = this
                .GetRecipes()
                .Where(r => r.Type == RecipeType.Starter)
                .ToList()
        };

        return View("All", model);
    }

    public IActionResult MainDishes()
    {
        var model = new RecipesListingOutputModel
        {
            ListName = "Основни ястия",
            Recipes = this
                .GetRecipes()
                .Where(r => r.Type == RecipeType.MainDish)
                .ToList()
        };

        return View("All", model);
    }

    public IActionResult Desserts()
    {
        var model = new RecipesListingOutputModel
        {
            ListName = "Десерти",
            Recipes = this
                .GetRecipes()
                .Where(r => r.Type == RecipeType.Dessert)
                .ToList()
        };

        return View("All", model);
    }

    [HttpGet]
    [Authorize]
    public IActionResult Mine()
    {
        var model = new RecipesListingOutputModel
        {
            ListName = "Мои рецепти",
            Recipes = this
                .GetRecipes()
                .Where(r => r.UserId == this.User.FindFirst(ClaimTypes.NameIdentifier).Value)
                .ToList()
        };

        return View("Listing", model);
    }

    [HttpGet]
    public IActionResult Search(string query)
    {
        query = HttpUtility.UrlDecode(query, System.Text.Encoding.UTF8);

        var model = new RecipesListingOutputModel
        {
            ListName = $"Резултати от търсене на \"{query}\"",
            Recipes = this
                .GetRecipes()
                .Where(r => r.Title.ToLower().Contains(query.ToLower().Trim()))
                .ToList()
        };

        return View("Listing", model);
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var recipe = this.data.Recipes
            .Where(r => r.Id == id)
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
            .FirstOrDefault();

        recipe.LikesCount = this.data.Favorites.Count(f => f.RecipeId == recipe.Id);

        recipe.User = this.data.Users.Where(u => u.Id == recipe.UserId).Select(u => new UserDetailsOutputModel()
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Bio = u.Bio,
            Email = u.Email,
            ImageUrl = u.ImageUrl
        }).FirstOrDefault();

        recipe.Comments = this.data.Comments
            .Where(c => c.RecipeId == id)
            .Select(c => new CommentOutputModel()
            {
                Id = c.Id,
                User = this.data.Users
                    .Where(u => u.Id == c.UserId)
                    .Select(u => new UserDetailsOutputModel()
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Bio = u.Bio,
                        Email = u.Email,
                        ImageUrl = u.ImageUrl
                    })
                    .Single(),
                RecipeId = c.RecipeId,
                CreatedOn = c.CreatedOn,
                Content = c.Content
            })
            .OrderByDescending(c => c.CreatedOn)
            .ToList();

        if (this.User.Identity?.IsAuthenticated ?? false)
        {
            recipe.IsFavorite = this.data.Favorites.Any(f => f.RecipeId == id && f.UserId == this.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        return View(recipe);
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
                "img/uploads/recipes",
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
            ImageUrl = @"\img\uploads\recipes\" + input.Image.FileName,
            UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value
        };

        this.data.Add(recipe);
        this.data.SaveChanges();

        return RedirectToAction("All");
    }

    [HttpGet]
    [Authorize]
    public IActionResult Edit(int id)
    {
        var recipe = this.data.Recipes
            .Where(r => r.Id == id)
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
                TypeInt = (int)r.Type,
                ImageUrl = r.ImageUrl,
                UserId = r.UserId
            })
            .FirstOrDefault();

        return View(recipe);
    }

    [HttpPost]
    [Authorize]
    public IActionResult Update(int id, CreateRecipeInputModel input)
    {
        var recipe = this.data.Recipes
            .FirstOrDefault(r => r.Id == id);

        if (recipe.UserId != this.User.FindFirst(ClaimTypes.NameIdentifier).Value && !this.User.IsInRole("Administrator"))
        {
            return BadRequest();
        }

        recipe.Title = input.Title;
        recipe.ShortDescription = input.ShortDescription;
        recipe.Content = input.Content;
        recipe.Ingredients = input.Ingredients;
        recipe.Portions = input.Portions;
        recipe.PreparationTime = input.PreparationTime;
        recipe.Type = (RecipeType)input.Type;

        if (input.Image != null)
        {
            using var fileStream = new FileStream(
                Path.Combine(
                    this.env.WebRootPath,
                    "img/uploads/recipes",
                    input.Image.FileName),
                FileMode.Create);

            input.Image.CopyTo(fileStream);

            recipe.ImageUrl = @"\img\uploads\recipes\" + input.Image.FileName;
        }
        this.data.SaveChanges();

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [Authorize]
    public IActionResult Comment(RecipeDetailsOutputModel input)
    {
        var comment = new Comment()
        {
            Content = input.CommentContent,
            RecipeId = input.Id,
            UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value
        };

        this.data.Add(comment);
        this.data.SaveChanges();

        return RedirectToAction("Details", new { input.Id });
    }

    private List<RecipeDetailsOutputModel> GetRecipes()
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
                UserId = r.UserId,
                User = this.data.Users
                    .Where(u => u.Id == r.UserId)
                    .Select(u => new UserDetailsOutputModel()
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Bio = u.Bio,
                        Email = u.Email,
                        ImageUrl = u.ImageUrl
                    })
                    .Single(),
                LikesCount = this.data.Favorites.Count(f => f.RecipeId == r.Id)
            })
            .ToList();

    [HttpPost]
    [Authorize]
    public IActionResult Delete(int id)
    {
        var recipe = this.data.Recipes
            .FirstOrDefault(r => r.Id == id);

        if (recipe.UserId != this.User.FindFirst(ClaimTypes.NameIdentifier).Value && !this.User.IsInRole("Administrator"))
        {
            return BadRequest();
        }

        foreach (var comment in this.data.Comments.Where(c => c.RecipeId == recipe.Id))
        {
            this.data.Comments.Remove(comment);
        }

        foreach (var favorite in this.data.Favorites.Where(f => f.RecipeId == recipe.Id))
        {
            this.data.Favorites.Remove(favorite);
        }

        this.data.Recipes.Remove(recipe);
        this.data.SaveChanges();

        return RedirectToAction("Index", "Home");
    }
}