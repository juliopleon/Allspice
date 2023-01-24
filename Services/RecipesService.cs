
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

    internal Recipe Get(int id)
    {
        Recipe recipe = _repo.Get(id);
        if (recipe == null)
        {
            throw new Exception("no recipe by that id");

        }
        return recipe;
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

    internal Recipe Create(Recipe recipeData)
    {
        Recipe recipe = _repo.Create(recipeData);
        return recipe;
    }

    internal Recipe Update(Recipe recipeUpdate, int id)
    {
        Recipe original = Get(id);
        original.Title = recipeUpdate.Title ?? original.Title;
        original.Instructions = recipeUpdate.Instructions ?? original.Instructions;
        original.Img = recipeUpdate.Img ?? original.Img;
        original.Category = recipeUpdate.Category ?? original.Category;

        bool edited = _repo.Update(original);
        if (edited == false)
        {
            throw new Exception("Recipe was not updated");
        }
        return original;
    }

    internal string Remove(int id, string userId)
    {
        Recipe original = GetOne(id, userId);
        if (original.CreatorId != userId)
        {
            throw new Exception("Not your Recipe");
        }

        _repo.Remove(id);
        return $"{original.Title} has been removed";
    }
}
