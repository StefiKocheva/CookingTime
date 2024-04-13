namespace CookingTime.Models.Recipes;

public class RecipesListingOutputModel
{
    public string ListName { get; set; }

    public List<RecipeDetailsOutputModel> Recipes { get; set; }
}
