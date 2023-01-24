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
}
