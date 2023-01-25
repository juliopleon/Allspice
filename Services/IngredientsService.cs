namespace Allspice.Services;

public class IngredientsService
{
    private readonly IngredientsRepository _repo;
    private readonly RecipesService _recipesService;

    public IngredientsService(IngredientsRepository repo, RecipesService recipesService)
    {
        _repo = repo;
        _recipesService = recipesService;
    }

    internal Ingredient Create(Ingredient ingredientData)
    {
        Ingredient ingredient = _repo.Create(ingredientData);
        return ingredient;
    }

    internal List<Ingredient> GetIngredientsByRecipe(int recipeId)
    {
        // Recipe recipe = _recipesService.GetOne(recipeId);

        List<Ingredient> ingredients = _repo.GetIngredientsByRecipe(recipeId);
        return ingredients;
    }

    internal string Remove(int id, string userId)
    {
        Ingredient original = _repo.GetOne(id);
        if (original.CreatorId != userId)
        {
            throw new Exception("not your ingredient to delete");
        }
        _repo.remove(id);
        return $"Ingredient at {id} was removed";
    }

}
