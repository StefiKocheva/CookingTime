namespace CookingTime.Models;

using Recipes;

public class IndexOutputModel
{
    public IEnumerable<RecipeDetailsOutputModel> LastThree { get; set; }

    public int TotalSalads { get; set; }

    public int TotalStarters { get; set; }

    public int TotalMainDishes { get; set; }

    public int TotalDesserts { get; set; }
}
