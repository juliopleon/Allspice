
namespace Allspice.Services;

public class RecipesService
{
    private readonly RecipesRepository _repo;

    public RecipesService(RecipesRepository repo)
    {
        _repo = repo;
    }

    internal List<Recipe> Get(string userId)
    {
        List<Recipe> recipes = _repo.Get();

        List<Recipe> filtered = recipes.FindAll(r => r.Archived == false || r.CreatorId == userId);
        return filtered;
    }

    internal Recipe GetOne(int id, string userId)
    {
        Recipe recipe = _repo.GetOne(id);
        if (recipe == null)
        {
            throw new Exception("No Recipe at that id");
        }
        if (recipe.Archived == true && recipe.CreatorId != userId)
        {
            throw new Exception("not your recipe.");
        }
        return recipe;
    }
}
